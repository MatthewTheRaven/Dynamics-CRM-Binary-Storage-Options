﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinaryStorageOptions.Configuration;
using BinaryStorageOptions.Providers;

namespace Migrate
{
	public partial class Main : Form
	{
		private AuthenticationCredentials authCredentials;
		private OrganizationServiceProxy proxy;
		private IServiceManagement<IOrganizationService> serviceManagement;
		private ConcurrentQueue<Guid> annotationsToMigrate;
		private ConcurrentQueue<Guid> attachmentsToMigrate;
		private bool shouldCancel;
		private bool isBusy;

		private enum DynamicsAuthTypeEnum
		{
			AD,
			IFD,
			OAuth,
			Certificate,
			ClientSecret,
			O365
		}

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			try
			{
				authGroup.Enabled = false;
				txtOrganizationServiceUrl.Enabled = false;
				serviceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(txtOrganizationServiceUrl.Text));
				authCredentials = new AuthenticationCredentials();
				proxy = GetServiceProxy(serviceManagement, authCredentials);

				BinaryStorageOptions.Configuration.IConfigurationProvider annotationConfigProvider =
					BinaryStorageOptions.Configuration.Factory.GetConfigurationProvider(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName, GetUnsecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName), GetSecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName));
				if (annotationConfigProvider.StorageProviderType == BinaryStorageOptions.Providers.StorageProviderType.CrmDefault)
				{
					MessageBox.Show("The provider is set to 'CrmDefault'.  This means no migration will happen.", "Default Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				BinaryStorageOptions.Configuration.IConfigurationProvider attachmentConfigProvider =
					BinaryStorageOptions.Configuration.Factory.GetConfigurationProvider(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName, GetUnsecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName), GetSecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName));
				if (attachmentConfigProvider.StorageProviderType == BinaryStorageOptions.Providers.StorageProviderType.CrmDefault)
				{
					MessageBox.Show("The provider is set to 'CrmDefault'.  This means no migration will happen.", "Default Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				ShowLoadedSettings(annotationConfigProvider, attachmentConfigProvider);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something very bad happened : " + ex.ToString(), "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
				authGroup.Enabled = true;
				txtOrganizationServiceUrl.Enabled = true;
			}
		}

		private void ShowLoadedSettings(IConfigurationProvider annotationConfigProvider, IConfigurationProvider attachmentConfigProvider)
		{
			switch (annotationConfigProvider.StorageProviderType)
			{
				case StorageProviderType.AzureBlob:
					AzureBlobStorageConfiguration blobConfig = (AzureBlobStorageConfiguration)annotationConfigProvider.Configuration;
					txtAzureAccount.Text = blobConfig.StorageAccount;
					txtAzureAccountKey.Text = blobConfig.StorageKey;
					rdoAzureSasToken.Checked = blobConfig.IsSasToken;
					rdoAzureAccessKey.Checked = !blobConfig.IsSasToken;
					break;
				case StorageProviderType.AzureFile:
					AzureFileStorageConfiguration fileConfig = (AzureFileStorageConfiguration)annotationConfigProvider.Configuration;
					txtAzureAccount.Text = fileConfig.StorageAccount;
					txtAzureAccountKey.Text = fileConfig.StorageKey;
					rdoAzureSasToken.Checked = fileConfig.IsSasToken;
					rdoAzureAccessKey.Checked = !fileConfig.IsSasToken;
					break;
			}
			IEncryptionProvider encryptionProvider = BinaryStorageOptions.Providers.Factory.GetEncryptionProvider(annotationConfigProvider.EncryptionProviderType, annotationConfigProvider);
			lblEncryption.Text = string.Format("Encryption : {0}", annotationConfigProvider.EncryptionProviderType);
			txtAES256Key.Text = encryptionProvider.Key;
			txtAES256Salt.Text = encryptionProvider.KeySalt;
			lblCompression.Text = string.Format("Compression : {0}", annotationConfigProvider.CompressionProviderType);
			if (annotationConfigProvider.CompressionProviderType == CompressionProviderType.Zip)
			{
				ZipCompressionProvider compressionProvider = (ZipCompressionProvider)BinaryStorageOptions.Providers.Factory.GetCompressionProvider(annotationConfigProvider.CompressionProviderType, annotationConfigProvider);
				lblCompression.Text += " (" + compressionProvider.CompressionLevel.ToString() + ")";
			}

			migrateGroup.Text = string.Format("Connected. External Storage Provider : {0}, {1}", GetExternalPath(annotationConfigProvider), GetExternalPath(attachmentConfigProvider));
			migrateGroup.Enabled = true;
		}

		private string GetExternalPath(IConfigurationProvider configurationProvider)
		{
			string description = string.Format("{0} - ", configurationProvider.StorageProviderType);
			switch (configurationProvider.StorageProviderType)
			{
				case StorageProviderType.AzureBlob:
					AzureBlobStorageConfiguration blobConfig = (AzureBlobStorageConfiguration)configurationProvider.Configuration;
					description += (blobConfig.Container + "/" + blobConfig.Folder).TrimEnd('/');
					break;
				case StorageProviderType.AzureFile:
					AzureFileStorageConfiguration fileConfig = (AzureFileStorageConfiguration)configurationProvider.Configuration;
					description += (fileConfig.Share + "/" + fileConfig.Folder).TrimEnd('/');
					break;
			}
			return description;
		}

		private void rdoIFDAuth_CheckedChanged(object sender, EventArgs e)
		{
			if (rdoIFDAuth.Checked)
			{
				txtUsername.Enabled = true;
				txtPassword.Enabled = true;
			}
			else
			{
				txtUsername.Enabled = false;
				txtPassword.Enabled = false;
			}
		}

		private void rdoOAuth_CheckedChanged(object sender, EventArgs e)
		{
			if (rdoOAuth.Checked)
			{
				txtUsername.Enabled = true;
				txtPassword.Enabled = true;
			}
			else
			{
				txtUsername.Enabled = false;
				txtPassword.Enabled = false;
			}
		}

		private void btnMigrate_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isBusy)
				{
					if (!chkPluginStepsManage.Checked)
					{
						if (MessageBox.Show("You have unticked the 'Automatically Enable/Disable BinaryStorageOptions plugin' checkbox. You MUST disable the Retrieve/RetrieveMultiple messages on the plugin manually before running this.  If you don't bad things will happen! Are you sure you wish to proceed at this time?", "Plugin Steps", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
						{
							return;
						}
					}

					isBusy = true;
					migrateGroup.Controls.OfType<Control>().ToList().ForEach(c => c.Enabled = false);
					btnMigrate.Text = "Cancel";
					btnMigrate.Enabled = true;
					shouldCancel = false;
					if (rdoInbound.Checked)
					{
						Task.Run(() => Migrate(false, (int)udThreadCount.Value, (int)udWaitDelay.Value * 1000, chkMigrateAnnotations.Checked, chkMigrateAttachments.Checked, chkMoveAnnotations.Checked));
					}
					else
					{
						Task.Run(() => Migrate(true, (int)udThreadCount.Value, (int)udWaitDelay.Value * 1000, chkMigrateAnnotations.Checked, chkMigrateAttachments.Checked, chkMoveAnnotations.Checked));
					}
				}
				else
				{
					shouldCancel = true;
					btnMigrate.Text = "Stopping...";
					btnMigrate.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something very bad happened : " + ex.ToString(), "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnMigrate.Text = "Migrate";
				migrateGroup.Controls.OfType<Control>().ToList().ForEach(c => c.Enabled = true);
				isBusy = false;
				shouldCancel = false;
			}
		}

		private void Migrate(bool external, int threadCount, int waitTime, bool migrateAnnotations, bool migrateEmailAttachments, bool moveAnnotations)
		{
			try
			{
				if (chkPluginStepsManage.Checked)
				{
					SetPluginStepsStatus(proxy, true);
				}

				FillMigrationQueues(external, migrateAnnotations, migrateEmailAttachments);

				int totalCount = annotationsToMigrate.Count + attachmentsToMigrate.Count;

				if (totalCount > 0)
				{
					pgbProgress.InvokeEx(pb => { pb.Maximum = totalCount; pb.Value = 0; });
					lblProgress.InvokeEx(pl => pl.Text = Math.Round(((double)pgbProgress.Value / (double)totalCount) * 100, 1) + "%");
				}
				else
				{
					pgbProgress.InvokeEx(pb => pb.Value = pb.Maximum);
					lblProgress.InvokeEx(pl => pl.Text = "100%");
				}
				txtOutput.InvokeEx(tb => tb.Clear());

				while ((migrateAnnotations && annotationsToMigrate.Count > 0) || (migrateEmailAttachments && attachmentsToMigrate.Count > 0))
				{
					Notify("Starting Migration...");
					if (migrateAnnotations)
					{
						Parallel.For(0, annotationsToMigrate.Count, new ParallelOptions { MaxDegreeOfParallelism = threadCount },
							(i, state) =>
							{
								if (shouldCancel) state.Stop();
								Guid id;
								while (!annotationsToMigrate.TryDequeue(out id))
								{
									int sleepValue = new Random().Next(1000);
									Notify(string.Format("Waiting for {0} milliseconds...", sleepValue));
									System.Threading.Thread.Sleep(sleepValue);
								}
								if (!MigrateEntity(id,
									BinaryStorageOptions.CrmConstants.AnnotationEntityName,
									BinaryStorageOptions.GenericConstants.Constants[BinaryStorageOptions.CrmConstants.AnnotationEntityName][BinaryStorageOptions.GenericConstants.DocumentBodyAttributeKey],
									BinaryStorageOptions.GenericConstants.Constants[BinaryStorageOptions.CrmConstants.AnnotationEntityName][BinaryStorageOptions.GenericConstants.FileNameAttributeKey], external,
									moveAnnotations))
								{
									NotifyError(string.Format("Migration of '{0}' with id '{1}' FAILED.", BinaryStorageOptions.CrmConstants.AnnotationEntityName, id));
								}
								pgbProgress.InvokeEx(pb => pgbProgress.Increment(1));
								lblProgress.InvokeEx(pl => pl.Text = Math.Round(((double)pgbProgress.Value / (double)totalCount) * 100, 1) + "%");
								//Notify(string.Format("Waiting for {0} milliseconds...", waitTime));
								System.Threading.Thread.Sleep(waitTime);
							});
					}
					if (migrateEmailAttachments)
					{
						if (shouldCancel) break;
						Parallel.For(0, attachmentsToMigrate.Count, new ParallelOptions { MaxDegreeOfParallelism = threadCount },
							(i, state) =>
							{
								if (shouldCancel) state.Stop();
								Notify(string.Format("Waiting for {0} seconds...", (int)udWaitDelay.Value));
								System.Threading.Thread.Sleep((int)udWaitDelay.Value);
								Guid id;
								while (!attachmentsToMigrate.TryDequeue(out id))
								{
									int sleepValue = new Random().Next(1000);
									Notify(string.Format("Waiting for {0} milliseconds...", sleepValue));
									System.Threading.Thread.Sleep(sleepValue);
								}
								if (!MigrateEntity(id, BinaryStorageOptions.CrmConstants.AttachmentEntityName,
									BinaryStorageOptions.GenericConstants.Constants[BinaryStorageOptions.CrmConstants.AttachmentEntityName][BinaryStorageOptions.GenericConstants.DocumentBodyAttributeKey],
									BinaryStorageOptions.GenericConstants.Constants[BinaryStorageOptions.CrmConstants.AttachmentEntityName][BinaryStorageOptions.GenericConstants.FileNameAttributeKey], external,
									moveAnnotations))
								{
									NotifyError(string.Format("Migration of '{0}' with id '{1}' FAILED.", BinaryStorageOptions.CrmConstants.AnnotationEntityName, id));
								}
								pgbProgress.InvokeEx(pb => pb.Increment(1));
								lblProgress.InvokeEx(pl => pl.Text = Math.Round(((double)pgbProgress.Value / (double)totalCount) * 100, 1) + "%");
								//Notify(string.Format("Waiting for {0} milliseconds...", waitTime));
								System.Threading.Thread.Sleep(waitTime);
							});
					}
					if (shouldCancel) break;
				}
			}
			finally
			{
				if (chkPluginStepsManage.Checked)
				{
					SetPluginStepsStatus(proxy, false);
				}
				isBusy = false;
				shouldCancel = false;
				btnMigrate.InvokeEx(b => b.Text = "Migrate");
				migrateGroup.InvokeEx(g => g.Controls.OfType<Control>().ToList().ForEach(c => c.Enabled = true));
				Notify("All Done.");
			}
		}

		private bool MigrateEntity(Guid id, string entityName, string documentBodyKey, string filenameKey, bool moveExternal, bool moveAnnotations)
		{
			OrganizationServiceProxy localProxy = null;
			bool success = false;
			try
			{
				localProxy = GetServiceProxy(serviceManagement, authCredentials);

				BinaryStorageOptions.Configuration.IConfigurationProvider configProvider = BinaryStorageOptions.Configuration.Factory.GetConfigurationProvider(proxy, entityName, GetUnsecurePluginConfiguration(localProxy, entityName), GetSecurePluginConfiguration(localProxy, entityName));
				BinaryStorageOptions.Providers.IBinaryStorageProvider storageProvider = BinaryStorageOptions.Providers.Factory.GetStorageProvider(configProvider);
				Entity entity = localProxy.Retrieve(entityName, id, new ColumnSet(true));
				if (moveExternal)
				{
					success = MigrateEntityToExternal(localProxy, storageProvider, entity, entityName, documentBodyKey, filenameKey, moveAnnotations);
				}
				else
				{
					success = MigrateEntityToLocal(localProxy, storageProvider, entity, entityName, documentBodyKey, filenameKey);
				}
			}
			catch (Exception ex)
			{
				NotifyError(ex.ToString());
			}
			finally
			{
				if (localProxy != null)
				{
					localProxy.Dispose();
					localProxy = null;
				}
			}
			return success;
		}

		private bool MigrateEntityToExternal(OrganizationServiceProxy localProxy, BinaryStorageOptions.Providers.IBinaryStorageProvider storageProvider, Entity entity, string entityName, string documentBodyKey, string filenameKey, bool moveAnnotations)
		{
			if (!entity.Attributes.ContainsKey(documentBodyKey) || (string)entity.Attributes[documentBodyKey] == BinaryStorageOptions.GenericConstants.EmptyBodyContent)
				return true;

			string fileName = (string)entity.Attributes[filenameKey];
			Notify(string.Format("Migrating '{0}' with filename '{1}' CRM -> External using {2}.", entityName, entity.Id.ToString() + "_" + fileName, storageProvider.GetType().Name));
			if (storageProvider.Create(entity.Id, fileName, Convert.FromBase64String((string)entity.Attributes[documentBodyKey])))
			{
				Notify(string.Format("Created '{0}' with filename '{1}'", entityName, entity.Id.ToString() + "_" + fileName));
				if (entityName == BinaryStorageOptions.CrmConstants.AttachmentEntityName || moveAnnotations)
				{
					Notify(string.Format("Removing '{0}' with filename '{1}' from CRM.", entityName, entity.Id.ToString() + "_" + fileName));
					entity.Attributes[BinaryStorageOptions.GenericConstants.Constants[entity.LogicalName][BinaryStorageOptions.GenericConstants.DocumentBodyAttributeKey]] = BinaryStorageOptions.GenericConstants.EmptyBodyContent;
					localProxy.Update(entity);
				}
				Notify(string.Format("Migration of '{0}' with filename '{1}' DONE.", entityName, entity.Id.ToString() + "_" + fileName));
				return true;
			}
			return false;
		}

		private bool MigrateEntityToLocal(OrganizationServiceProxy localProxy, BinaryStorageOptions.Providers.IBinaryStorageProvider storageProvider, Entity entity, string entityName, string documentBodyKey, string filenameKey)
		{
			if (!entity.Attributes.ContainsKey(documentBodyKey))
				return true;

			string filename = (string)entity.Attributes[filenameKey];
			Notify(string.Format("Migrating '{0}' with filename '{1}' External -> CRM using {2}.", entityName, entity.Id.ToString() + "_" + filename, storageProvider.GetType().Name));
			if ((string)entity.Attributes[documentBodyKey] == BinaryStorageOptions.GenericConstants.EmptyBodyContent)
			{
				byte[] data = storageProvider.Read(entity.Id, filename);
				entity.Attributes[documentBodyKey] = Convert.ToBase64String(data);
				localProxy.Update(entity);
			}
			storageProvider.Delete(entity.Id, filename);
			Notify(string.Format("Migration of '{0}' with id '{1}' DONE.", entityName, entity.Id.ToString() + "_" + filename));
			return true;
		}


		private DynamicsAuthTypeEnum DynamicsAuthType
		{
			get
			{
				DynamicsAuthTypeEnum value = DynamicsAuthTypeEnum.AD;

				if (IsRadioButtonSelected(rdoADAuth)) { return DynamicsAuthTypeEnum.AD; }
				if (IsRadioButtonSelected(rdoIFDAuth)) { return DynamicsAuthTypeEnum.IFD; }
				if (IsRadioButtonSelected(rdoOAuth)) { return DynamicsAuthTypeEnum.OAuth; }
				if (IsRadioButtonSelected(rdoClientSecret)) { return DynamicsAuthTypeEnum.ClientSecret; }

				return value;
			}
		}

		private bool IsRadioButtonSelected(RadioButton radioButtonControl)
		{
			bool result = false;
			if (radioButtonControl.InvokeRequired)
			{
				radioButtonControl.BeginInvoke(new MethodInvoker(delegate { result = radioButtonControl.Checked; }));
			}
			else
			{
				result = radioButtonControl.Checked;
			}
			return result;
		}

		private void FillMigrationQueues(bool moveToExternal, bool migrateAnnotations, bool migrateEmailAttachments)
		{
			OrganizationServiceProxy localProxy = null;
			localProxy = GetServiceProxy(serviceManagement, authCredentials);

			annotationsToMigrate = new ConcurrentQueue<Guid>();
			attachmentsToMigrate = new ConcurrentQueue<Guid>();

			var annotationIdList = new List<Guid>();
			var attachmentsIdList = new List<Guid>();

			string pagingCookie = null;
			QueryExpression queryExpression;
			int pageSize = 5000;
			try
			{
				BinaryStorageOptions.Configuration.IConfigurationProvider configProvider;
				BinaryStorageOptions.Providers.IBinaryStorageProvider storageProvider;
				if (migrateAnnotations)
				{
					Notify(string.Format("Getting list of '{0}' to migrate...", BinaryStorageOptions.CrmConstants.AnnotationEntityName));
					configProvider = BinaryStorageOptions.Configuration.Factory.GetConfigurationProvider(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName, GetUnsecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName), GetSecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AnnotationEntityName));
					storageProvider = BinaryStorageOptions.Providers.Factory.GetStorageProvider(configProvider);
					var externalAnnotations = new Dictionary<Guid, string>(storageProvider.GetFileNames().ToDictionary(fn => new Guid(fn.Substring(0, 36))));
					annotationIdList = new List<Guid>(externalAnnotations.Keys);
				}
				if (migrateEmailAttachments)
				{
					Notify(string.Format("Getting list of '{0}' to migrate...", BinaryStorageOptions.CrmConstants.AttachmentEntityName));
					configProvider = BinaryStorageOptions.Configuration.Factory.GetConfigurationProvider(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName, GetUnsecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName), GetSecurePluginConfiguration(proxy, BinaryStorageOptions.CrmConstants.AttachmentEntityName));
					storageProvider = BinaryStorageOptions.Providers.Factory.GetStorageProvider(configProvider);
					var externalAttachments = new Dictionary<Guid, string>(storageProvider.GetFileNames().ToDictionary(fn => new Guid(fn.Substring(0, 36))));
					attachmentsIdList = new List<Guid>(externalAttachments.Keys);
				}

				if (moveToExternal)
				{
					bool moreRecords = false;
					int pageNum = 1;
					EntityCollection results;
					if (migrateAnnotations)
					{
						Notify(string.Format("Getting list of '{0}' to migrate...", BinaryStorageOptions.CrmConstants.AnnotationEntityName));

						queryExpression = new QueryExpression(BinaryStorageOptions.CrmConstants.AnnotationEntityName);
						queryExpression.Criteria = new Microsoft.Xrm.Sdk.Query.FilterExpression();
						queryExpression.Criteria.AddCondition(BinaryStorageOptions.CrmConstants.IsDocumentKey, ConditionOperator.Equal, true);
						queryExpression.ColumnSet = new ColumnSet(false);
						queryExpression.PageInfo = new PagingInfo
						{
							Count = pageSize,
							PageNumber = pageNum,
							PagingCookie = pagingCookie,
						};
						results = localProxy.RetrieveMultiple(queryExpression);
						pagingCookie = results.PagingCookie;
						moreRecords = results.MoreRecords;
						while (moreRecords || results.Entities.Count > 0)
						{
							results.Entities.Select(e => e.Id).ToList().ForEach(id =>
							{
								if (!annotationIdList.Contains(id))
									annotationsToMigrate.Enqueue(id);
							});
							Notify(string.Format("{0} found : {1}", BinaryStorageOptions.CrmConstants.AnnotationEntityName, annotationsToMigrate.Count));
							queryExpression.PageInfo.PageNumber = ++pageNum;
							queryExpression.PageInfo.PagingCookie = pagingCookie;
							results = localProxy.RetrieveMultiple(queryExpression);
							pagingCookie = results.PagingCookie;
							moreRecords = results.MoreRecords;
						}
					}

					if (migrateEmailAttachments)
					{
						Notify(string.Format("Getting list of '{0}' to migrate...", BinaryStorageOptions.CrmConstants.AttachmentEntityName));

						moreRecords = false;
						pageNum = 1;
						pagingCookie = null;
						queryExpression = new QueryExpression(BinaryStorageOptions.CrmConstants.AttachmentEntityName);
						queryExpression.ColumnSet = new ColumnSet(false);
						queryExpression.PageInfo = new PagingInfo
						{
							Count = pageSize,
							PageNumber = pageNum,
							PagingCookie = pagingCookie,
						};
						results = localProxy.RetrieveMultiple(queryExpression);
						pagingCookie = results.PagingCookie;
						moreRecords = results.MoreRecords;
						while (moreRecords || results.Entities.Count > 0)
						{
							results.Entities.Select(e => e.Id).ToList().ForEach(id =>
							{
								if (!attachmentsIdList.Contains(id))
									attachmentsToMigrate.Enqueue(id);
							});
							Notify(string.Format("{0} found : {1}", BinaryStorageOptions.CrmConstants.AttachmentEntityName, attachmentsToMigrate.Count));
							queryExpression.PageInfo.PageNumber = ++pageNum;
							queryExpression.PageInfo.PagingCookie = pagingCookie;
							results = localProxy.RetrieveMultiple(queryExpression);
							pagingCookie = results.PagingCookie;
							moreRecords = results.MoreRecords;
						}
					}
				}
				else
				{
					attachmentsToMigrate = new ConcurrentQueue<Guid>(attachmentsIdList);
					annotationsToMigrate = new ConcurrentQueue<Guid>(annotationIdList);
				}
			}
			finally
			{
				if (localProxy != null)
				{
					localProxy.Dispose();
					localProxy = null;
				}
			}
		}

		private void Notify(string message)
		{
			lblMessage.InvokeEx(l => l.Text = message);
		}

		private void NotifyError(string message, bool newline = true)
		{
			txtOutput.InvokeEx(tb =>
			{
				tb.Suspend();
				tb.Text += message + (newline ? "\r\n" : "");
				tb.Select(tb.TextLength, 0);
				tb.ScrollToCaret();
				tb.Resume();
			});
		}

		public void SetPluginStepsStatus(IOrganizationService proxy, bool disable)
		{
			int pluginStateCode = disable ? 1 : 0;
			int pluginStatusCode = disable ? 2 : 1;
			using (OrganizationServiceContext context = new OrganizationServiceContext(proxy))
			{
				var steps = context.CreateQuery("sdkmessageprocessingstep")
										.Where(s => s.GetAttributeValue<string>("name").StartsWith("BinaryStorageOptions."))
										.ToList();
				foreach (var step in steps)
				{
					Notify(string.Format("{0} plugin step '{1}'...", (disable ? "Disabling" : "Enabling"), step.Attributes["name"]));
					var response = proxy.Execute(new Microsoft.Crm.Sdk.Messages.SetStateRequest
					{
						EntityMoniker = new EntityReference("sdkmessageprocessingstep", step.Id),
						State = new OptionSetValue(pluginStateCode),
						Status = new OptionSetValue(pluginStatusCode)
					});
				}
			}
			return;
		}

		private string GetUnsecurePluginConfiguration(IOrganizationService proxy, string entityName)
		{
			using (OrganizationServiceContext context = new OrganizationServiceContext(proxy))
			{
				string stepName = string.Format("BinaryStorageOptions.Plugin: Create of {0}", entityName);
				var step = context.CreateQuery("sdkmessageprocessingstep")
										.Where(s => s.GetAttributeValue<string>("name") == stepName)
										.FirstOrDefault();
				if (step != null)
				{
					return (string)step.Attributes["configuration"];
				}
			}
			return null;
		}

		private string GetSecurePluginConfiguration(IOrganizationService proxy, string entityName)
		{
			using (OrganizationServiceContext context = new OrganizationServiceContext(proxy))
			{
				string stepName = string.Format("BinaryStorageOptions.Plugin: Create of {0}", entityName);
				var step = context.CreateQuery("sdkmessageprocessingstep")
										.Where(s => s.GetAttributeValue<string>("name") == stepName)
										.FirstOrDefault();
				if (step != null)
				{
					EntityReference secureConfigReference = step.Attributes["sdkmessageprocessingstepsecureconfigid"] as EntityReference;
					if (secureConfigReference != null)
					{
						var secureConfig = proxy.Retrieve("sdkmessageprocessingstepsecureconfig", secureConfigReference.Id, new ColumnSet("secureconfig"));
						if (secureConfig != null)
						{
							return (string)secureConfig.Attributes["secureconfig"];
						}
					}
				}
			}
			return null;
		}

		private bool moveAnnotationsWarningShown = false;
		private void chkMoveAnnotations_CheckedChanged(object sender, EventArgs e)
		{
			if (chkMoveAnnotations.Checked && !moveAnnotationsWarningShown && rdoOutbound.Checked)
			{
				if (MessageBox.Show("If you use this function, the filesizes on the annotation attachments will all be set to 3, which will cause queries against the filesize to behave oddly. Are you sure you want to do this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				{
					moveAnnotationsWarningShown = true;
					chkMoveAnnotations.Checked = false;
				}
			}
		}

		private void rdoInbound_CheckedChanged(object sender, EventArgs e)
		{
			chkMoveAnnotations.Enabled = !rdoInbound.Checked;
			if (!chkMoveAnnotations.Enabled)
				chkMoveAnnotations.Checked = false;
		}

		private void rdoAzureSasToken_CheckedChanged(object sender, EventArgs e)
        {
			lblAccountKey.Text = "Azure SAS Token :";
        }

        private void rdoAzureAccessKey_CheckedChanged(object sender, EventArgs e)
        {
			lblAccountKey.Text = "Azure Access Key :";
        }

		private OrganizationServiceProxy GetServiceProxy(IServiceManagement<IOrganizationService> orgService, AuthenticationCredentials authCredentials)
		{
			OrganizationServiceProxy orgProxy = null;

			switch (DynamicsAuthType)
			{
				case DynamicsAuthTypeEnum.AD:
					authCredentials = GetCredentials(orgService, AuthenticationProviderType.ActiveDirectory);
					//authCredentials.ClientCredentials.Windows.ClientCredential = System.Net.CredentialCache.DefaultNetworkCredentials;
					orgProxy = new OrganizationServiceProxy(orgService, authCredentials.ClientCredentials);
					break;

				case DynamicsAuthTypeEnum.IFD:
					authCredentials = GetCredentials(orgService, AuthenticationProviderType.Federation);
					//authCredentials.ClientCredentials.UserName.UserName = txtUsername.Text;
					//authCredentials.ClientCredentials.UserName.Password = txtPassword.Text;
					//authCredentials = serviceManagement.Authenticate(authCredentials);
					orgProxy = new OrganizationServiceProxy(orgService, authCredentials.SecurityTokenResponse);
					break;

				case DynamicsAuthTypeEnum.OAuth:
					authCredentials = GetCredentials(orgService, AuthenticationProviderType.OnlineFederation);
					orgProxy = new OrganizationServiceProxy(orgService, authCredentials.SecurityTokenResponse);
					break;

				case DynamicsAuthTypeEnum.ClientSecret:
					break;
			}

			return orgProxy;
		}

		/// <summary>
		/// Obtain the AuthenticationCredentials based on AuthenticationProviderType.
		/// </summary>
		/// <param name="service">A service management object.</param>
		/// <param name="endpointType">An AuthenticationProviderType of the CRM environment.</param>
		/// <returns>Get filled credentials.</returns>
		private AuthenticationCredentials GetCredentials<TService>(IServiceManagement<TService> service, AuthenticationProviderType endpointType)
		{
			AuthenticationCredentials authCredentials = new AuthenticationCredentials();

			switch (endpointType)
			{
				case AuthenticationProviderType.ActiveDirectory:
					authCredentials.ClientCredentials.Windows.ClientCredential =
						new System.Net.NetworkCredential(txtUsername.Text,
							txtPassword.Text);
					break;
				case AuthenticationProviderType.LiveId:
					authCredentials.ClientCredentials.UserName.UserName = txtUsername.Text;
					authCredentials.ClientCredentials.UserName.Password = txtPassword.Text;
					authCredentials.SupportingCredentials = new AuthenticationCredentials();
					authCredentials.SupportingCredentials.ClientCredentials =
						Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
					break;
				default: // For Federated and OnlineFederated environments.                    
					authCredentials.ClientCredentials.UserName.UserName = txtUsername.Text;
					authCredentials.ClientCredentials.UserName.Password = txtPassword.Text;
					// For OnlineFederated single-sign on, you could just use current UserPrincipalName instead of passing user name and password.
					// authCredentials.UserPrincipalName = UserPrincipal.Current.UserPrincipalName;  // Windows Kerberos

					// The service is configured for User Id authentication, but the user might provide Microsoft
					// account credentials. If so, the supporting credentials must contain the device credentials.
					if (endpointType == AuthenticationProviderType.OnlineFederation)
					{
						IdentityProvider provider = service.GetIdentityProvider(authCredentials.ClientCredentials.UserName.UserName);
						if (provider != null && provider.IdentityProviderType == IdentityProviderType.LiveId)
						{
							authCredentials.SupportingCredentials = new AuthenticationCredentials();
							authCredentials.SupportingCredentials.ClientCredentials =
								Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
						}
					}

					break;
			}

			return authCredentials;
		}
    }
}
