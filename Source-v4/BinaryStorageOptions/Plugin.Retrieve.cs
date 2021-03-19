﻿using BinaryStorageOptions.Providers;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryStorageOptions
{
	public partial class Plugin
	{
		public void Retrieve(IServiceProvider serviceProvider)
		{
			IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
			ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

			if (context.MessageName != MessageName.Retrieve && context.MessageName != MessageName.RetrieveMultiple)
				//Invalid event attached
				return;

			IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
			IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

			List<Entity> entities = new List<Entity>();

			//This case handles where IOrganizationService is called with "LoadProperty".  For this, you need to attach this plugin to the "Retrieve" method of the parent Entity.
			if (context.OutputParameters.ContainsKey(CrmConstants.BusinessEntityKey) &&
					context.OutputParameters[CrmConstants.BusinessEntityKey] is Entity &&
					((Entity)context.OutputParameters[CrmConstants.BusinessEntityKey]).RelatedEntities != null &&
					((Entity)context.OutputParameters[CrmConstants.BusinessEntityKey]).RelatedEntities.Values is ICollection<EntityCollection> &&
					((Entity)context.OutputParameters[CrmConstants.BusinessEntityKey]).RelatedEntities.Values.Count > 0)
			{
				foreach (EntityCollection entityCollection in ((Entity)context.OutputParameters[CrmConstants.BusinessEntityKey]).RelatedEntities.Values.Where(ec => ec.EntityName == CrmConstants.AnnotationEntityName || ec.EntityName == CrmConstants.AttachmentEntityName))
				{
					entities.AddRange(entityCollection.Entities);
				}
			}
			else if (context.MessageName == MessageName.Retrieve && context.OutputParameters.Contains(CrmConstants.BusinessEntityKey) && context.OutputParameters[CrmConstants.BusinessEntityKey] is Entity)
			{
				//Handles retrieve message
				Entity entity = (Entity)context.OutputParameters[CrmConstants.BusinessEntityKey];
				if (entity.LogicalName != CrmConstants.AnnotationEntityName && entity.LogicalName != CrmConstants.AttachmentEntityName)
					return;
				entities.Add(entity);
			}
			else if (context.MessageName == MessageName.RetrieveMultiple && context.OutputParameters.Contains(CrmConstants.BusinessEntityCollectionKey) && context.OutputParameters[CrmConstants.BusinessEntityCollectionKey] is EntityCollection)
			{
				EntityCollection entityCollection = (EntityCollection)context.OutputParameters[CrmConstants.BusinessEntityCollectionKey];
				if (entityCollection.TotalRecordCount > 0 && entityCollection.EntityName != CrmConstants.AnnotationEntityName && entityCollection.EntityName != CrmConstants.AttachmentEntityName)
					return;
				entities.AddRange(entityCollection.Entities);
			}

			if (entities.Count > 0)
			{
				Configuration.IConfigurationProvider configurationProvider = Configuration.Factory.GetConfigurationProvider(service, entities[0].LogicalName, unsecurePluginStepConfiguration, securePluginStepConfiguration);
				if (configurationProvider.StorageProviderType == Providers.StorageProviderType.CrmDefault)
				{
					//In this case, not doing anything.
					return;
				}

				Providers.IBinaryStorageProvider storageProvider = Providers.Factory.GetStorageProvider(configurationProvider);
				entities.ForEach(e => 
					HandleEntity(service, tracingService, context.UserId, storageProvider, e, GenericConstants.Constants[e.LogicalName][GenericConstants.DocumentBodyAttributeKey], GenericConstants.Constants[e.LogicalName][GenericConstants.FileNameAttributeKey])
				);
			}
		}

		private void HandleEntity(IOrganizationService service, ITracingService tracingService, Guid userId, Providers.IBinaryStorageProvider storageProvider, Entity entity, string documentBodyAttributeKey, string fileNameAttributeKey)
		{
			try
			{
				if (!(entity.Attributes.ContainsKey(documentBodyAttributeKey) || entity.Attributes.ContainsKey(CrmConstants.FileSizeKey)))
				{
					//If none of these columns are queried, we don't have to do anything, as it will just return whatever is in base tables.
					return;
				}

				// if the query did not include the filename, try to retrieve that
				if (!entity.Attributes.ContainsKey(fileNameAttributeKey))
				{
					var filenameResult = service.Retrieve(entity.LogicalName, entity.Id, new ColumnSet(fileNameAttributeKey));
					if (filenameResult.Attributes.ContainsKey(fileNameAttributeKey))
					{
						entity.Attributes.Add(fileNameAttributeKey, filenameResult[fileNameAttributeKey]);
					}
				}

				try
				{
					if (entity.Attributes.ContainsKey(CrmConstants.FileSizeKey) &&
							((int)entity.Attributes[CrmConstants.FileSizeKey] == GenericConstants.EmptyBodyContentDataLength || (int)entity.Attributes[CrmConstants.FileSizeKey] == GenericConstants.AltEmptyBodyContentDataLength) &&
							entity.Attributes.ContainsKey(fileNameAttributeKey))
					{
						entity.Attributes[CrmConstants.FileSizeKey] = storageProvider.GetFileSize(entity.Id, (string)entity.Attributes[fileNameAttributeKey]);
					}
				}
				catch (Exception ex)
				{
					tracingService.Trace("Error getting file size: {0}", ex.ToString());
					// if the filesize lookup fails, just return 1
					entity.Attributes[CrmConstants.FileSizeKey] = 1;
				}

				try
				{
					if (entity.Attributes.ContainsKey(documentBodyAttributeKey) && entity.Attributes.ContainsKey(fileNameAttributeKey))
					{
						string body = (string)entity.Attributes[documentBodyAttributeKey];

						if (body == GenericConstants.EmptyBodyContent)
						{
							tracingService.Trace("Found Binary Attachment with filename {0}", (string)entity[fileNameAttributeKey]);
							//If the body is requested, go fetch it and populate it where CRM wants it
							byte[] data = storageProvider.Read(entity.Id, (string)entity[fileNameAttributeKey]);
							entity.Attributes[documentBodyAttributeKey] = Convert.ToBase64String(data);
						}
                        else if (body == GenericConstants.EmptyBodyContentBase64)
                        {
							tracingService.Trace("Found Base64 Attachment with filename {0}", (string)entity[fileNameAttributeKey]);
							string data = storageProvider.ReadString(entity.Id, (string)entity[fileNameAttributeKey]).Trim();
							entity.Attributes[documentBodyAttributeKey] = data.Replace("\u0000", string.Empty);
						}
                    }
				}
				catch (Exception ex)
				{
					tracingService.Trace("Error getting document body: {0}", ex.ToString());
					entity.Attributes[documentBodyAttributeKey] = string.Empty;
				}
			}
			catch (Exception ex)
			{
				throw new InvalidPluginExecutionException(OperationStatus.Failed, ex.ToString());
			}
		}
	}
}
