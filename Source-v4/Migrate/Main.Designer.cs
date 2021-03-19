namespace Migrate
{
	partial class Main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrganizationServiceUrl = new System.Windows.Forms.TextBox();
            this.rdoADAuth = new System.Windows.Forms.RadioButton();
            this.rdoIFDAuth = new System.Windows.Forms.RadioButton();
            this.authGroup = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.migrateGroup = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPluginStepsManage = new System.Windows.Forms.CheckBox();
            this.udWaitDelay = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udThreadCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoAzureSasToken = new System.Windows.Forms.RadioButton();
            this.rdoAzureAccessKey = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblEncryption = new System.Windows.Forms.Label();
            this.lblCompression = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAES256Salt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAES256Key = new System.Windows.Forms.TextBox();
            this.lblAccountKey = new System.Windows.Forms.Label();
            this.txtAzureAccountKey = new System.Windows.Forms.TextBox();
            this.txtAzureAccount = new System.Windows.Forms.TextBox();
            this.lblAzureAccount = new System.Windows.Forms.Label();
            this.directionGroup = new System.Windows.Forms.GroupBox();
            this.chkMoveAnnotations = new System.Windows.Forms.CheckBox();
            this.chkMigrateAttachments = new System.Windows.Forms.CheckBox();
            this.chkMigrateAnnotations = new System.Windows.Forms.CheckBox();
            this.rdoOutbound = new System.Windows.Forms.RadioButton();
            this.rdoInbound = new System.Windows.Forms.RadioButton();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoOAuth = new System.Windows.Forms.RadioButton();
            this.rdoClientSecret = new System.Windows.Forms.RadioButton();
            this.authGroup.SuspendLayout();
            this.migrateGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWaitDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udThreadCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.directionGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "OrganizationServiceUrl :";
            // 
            // txtOrganizationServiceUrl
            // 
            this.txtOrganizationServiceUrl.Location = new System.Drawing.Point(255, 17);
            this.txtOrganizationServiceUrl.Margin = new System.Windows.Forms.Padding(6);
            this.txtOrganizationServiceUrl.Name = "txtOrganizationServiceUrl";
            this.txtOrganizationServiceUrl.Size = new System.Drawing.Size(1185, 29);
            this.txtOrganizationServiceUrl.TabIndex = 1;
            this.txtOrganizationServiceUrl.Text = "https://{server}/{org}/XRMServices/2011/Organization.svc";
            // 
            // rdoADAuth
            // 
            this.rdoADAuth.AutoSize = true;
            this.rdoADAuth.Checked = true;
            this.rdoADAuth.Location = new System.Drawing.Point(29, 35);
            this.rdoADAuth.Margin = new System.Windows.Forms.Padding(6);
            this.rdoADAuth.Name = "rdoADAuth";
            this.rdoADAuth.Size = new System.Drawing.Size(194, 29);
            this.rdoADAuth.TabIndex = 2;
            this.rdoADAuth.TabStop = true;
            this.rdoADAuth.Text = "AD Authentication";
            this.rdoADAuth.UseVisualStyleBackColor = true;
            // 
            // rdoIFDAuth
            // 
            this.rdoIFDAuth.AutoSize = true;
            this.rdoIFDAuth.Location = new System.Drawing.Point(235, 35);
            this.rdoIFDAuth.Margin = new System.Windows.Forms.Padding(6);
            this.rdoIFDAuth.Name = "rdoIFDAuth";
            this.rdoIFDAuth.Size = new System.Drawing.Size(260, 29);
            this.rdoIFDAuth.TabIndex = 3;
            this.rdoIFDAuth.Text = "IFD/Online Authentication";
            this.rdoIFDAuth.UseVisualStyleBackColor = true;
            this.rdoIFDAuth.CheckedChanged += new System.EventHandler(this.rdoIFDAuth_CheckedChanged);
            // 
            // authGroup
            // 
            this.authGroup.Controls.Add(this.rdoClientSecret);
            this.authGroup.Controls.Add(this.rdoOAuth);
            this.authGroup.Controls.Add(this.txtPassword);
            this.authGroup.Controls.Add(this.label3);
            this.authGroup.Controls.Add(this.txtUsername);
            this.authGroup.Controls.Add(this.label2);
            this.authGroup.Controls.Add(this.btnConnect);
            this.authGroup.Controls.Add(this.rdoADAuth);
            this.authGroup.Controls.Add(this.rdoIFDAuth);
            this.authGroup.Location = new System.Drawing.Point(28, 65);
            this.authGroup.Margin = new System.Windows.Forms.Padding(6);
            this.authGroup.Name = "authGroup";
            this.authGroup.Padding = new System.Windows.Forms.Padding(6);
            this.authGroup.Size = new System.Drawing.Size(1415, 203);
            this.authGroup.TabIndex = 4;
            this.authGroup.TabStop = false;
            this.authGroup.Text = "Authentication";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(405, 133);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(402, 29);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password :";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(405, 85);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(402, 29);
            this.txtUsername.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Username :";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(1036, 26);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(171, 50);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // migrateGroup
            // 
            this.migrateGroup.Controls.Add(this.groupBox2);
            this.migrateGroup.Controls.Add(this.groupBox1);
            this.migrateGroup.Controls.Add(this.directionGroup);
            this.migrateGroup.Location = new System.Drawing.Point(28, 279);
            this.migrateGroup.Margin = new System.Windows.Forms.Padding(6);
            this.migrateGroup.Name = "migrateGroup";
            this.migrateGroup.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.migrateGroup.Size = new System.Drawing.Size(1415, 503);
            this.migrateGroup.TabIndex = 7;
            this.migrateGroup.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPluginStepsManage);
            this.groupBox2.Controls.Add(this.udWaitDelay);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.udThreadCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(649, 321);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(743, 168);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // chkPluginStepsManage
            // 
            this.chkPluginStepsManage.AutoSize = true;
            this.chkPluginStepsManage.Checked = true;
            this.chkPluginStepsManage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPluginStepsManage.Location = new System.Drawing.Point(39, 37);
            this.chkPluginStepsManage.Margin = new System.Windows.Forms.Padding(6);
            this.chkPluginStepsManage.Name = "chkPluginStepsManage";
            this.chkPluginStepsManage.Size = new System.Drawing.Size(545, 29);
            this.chkPluginStepsManage.TabIndex = 19;
            this.chkPluginStepsManage.Text = "Automatically Enable/Disable BinaryStorageOptions plugin";
            this.chkPluginStepsManage.UseVisualStyleBackColor = true;
            // 
            // udWaitDelay
            // 
            this.udWaitDelay.Location = new System.Drawing.Point(447, 92);
            this.udWaitDelay.Margin = new System.Windows.Forms.Padding(6);
            this.udWaitDelay.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.udWaitDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWaitDelay.Name = "udWaitDelay";
            this.udWaitDelay.Size = new System.Drawing.Size(86, 29);
            this.udWaitDelay.TabIndex = 18;
            this.udWaitDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "Wait Delay (s) :";
            // 
            // udThreadCount
            // 
            this.udThreadCount.Location = new System.Drawing.Point(189, 89);
            this.udThreadCount.Margin = new System.Windows.Forms.Padding(6);
            this.udThreadCount.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.udThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udThreadCount.Name = "udThreadCount";
            this.udThreadCount.Size = new System.Drawing.Size(86, 29);
            this.udThreadCount.TabIndex = 16;
            this.udThreadCount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "ThreadCount :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoAzureSasToken);
            this.groupBox1.Controls.Add(this.rdoAzureAccessKey);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblEncryption);
            this.groupBox1.Controls.Add(this.lblCompression);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtAES256Salt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtAES256Key);
            this.groupBox1.Controls.Add(this.lblAccountKey);
            this.groupBox1.Controls.Add(this.txtAzureAccountKey);
            this.groupBox1.Controls.Add(this.txtAzureAccount);
            this.groupBox1.Controls.Add(this.lblAzureAccount);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(17, 30);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(1375, 274);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // rdoAzureSasToken
            // 
            this.rdoAzureSasToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoAzureSasToken.AutoSize = true;
            this.rdoAzureSasToken.Location = new System.Drawing.Point(425, 32);
            this.rdoAzureSasToken.Margin = new System.Windows.Forms.Padding(6);
            this.rdoAzureSasToken.Name = "rdoAzureSasToken";
            this.rdoAzureSasToken.Size = new System.Drawing.Size(140, 29);
            this.rdoAzureSasToken.TabIndex = 42;
            this.rdoAzureSasToken.Text = "SAS Token";
            this.rdoAzureSasToken.UseVisualStyleBackColor = true;
            this.rdoAzureSasToken.CheckedChanged += new System.EventHandler(this.rdoAzureSasToken_CheckedChanged);
            // 
            // rdoAzureAccessKey
            // 
            this.rdoAzureAccessKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoAzureAccessKey.AutoSize = true;
            this.rdoAzureAccessKey.Checked = true;
            this.rdoAzureAccessKey.Location = new System.Drawing.Point(204, 32);
            this.rdoAzureAccessKey.Margin = new System.Windows.Forms.Padding(6);
            this.rdoAzureAccessKey.Name = "rdoAzureAccessKey";
            this.rdoAzureAccessKey.Size = new System.Drawing.Size(142, 29);
            this.rdoAzureAccessKey.TabIndex = 41;
            this.rdoAzureAccessKey.TabStop = true;
            this.rdoAzureAccessKey.Text = "Access Key";
            this.rdoAzureAccessKey.UseVisualStyleBackColor = true;
            this.rdoAzureAccessKey.CheckedChanged += new System.EventHandler(this.rdoAzureAccessKey_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 36);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 25);
            this.label11.TabIndex = 40;
            this.label11.Text = "Auth Type :";
            // 
            // lblEncryption
            // 
            this.lblEncryption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEncryption.AutoSize = true;
            this.lblEncryption.Location = new System.Drawing.Point(603, 84);
            this.lblEncryption.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEncryption.Name = "lblEncryption";
            this.lblEncryption.Size = new System.Drawing.Size(115, 25);
            this.lblEncryption.TabIndex = 39;
            this.lblEncryption.Text = "Encryption :";
            // 
            // lblCompression
            // 
            this.lblCompression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCompression.AutoSize = true;
            this.lblCompression.Location = new System.Drawing.Point(924, 84);
            this.lblCompression.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCompression.Name = "lblCompression";
            this.lblCompression.Size = new System.Drawing.Size(139, 25);
            this.lblCompression.TabIndex = 38;
            this.lblCompression.Text = "Compression :";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 228);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 25);
            this.label10.TabIndex = 37;
            this.label10.Text = "AES256 Salt :";
            // 
            // txtAES256Salt
            // 
            this.txtAES256Salt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAES256Salt.Enabled = false;
            this.txtAES256Salt.Location = new System.Drawing.Point(204, 223);
            this.txtAES256Salt.Margin = new System.Windows.Forms.Padding(6);
            this.txtAES256Salt.Name = "txtAES256Salt";
            this.txtAES256Salt.Size = new System.Drawing.Size(1146, 29);
            this.txtAES256Salt.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 25);
            this.label9.TabIndex = 35;
            this.label9.Text = "AES256 Key :";
            // 
            // txtAES256Key
            // 
            this.txtAES256Key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAES256Key.Enabled = false;
            this.txtAES256Key.Location = new System.Drawing.Point(204, 175);
            this.txtAES256Key.Margin = new System.Windows.Forms.Padding(6);
            this.txtAES256Key.Name = "txtAES256Key";
            this.txtAES256Key.Size = new System.Drawing.Size(1146, 29);
            this.txtAES256Key.TabIndex = 34;
            // 
            // lblAccountKey
            // 
            this.lblAccountKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAccountKey.AutoSize = true;
            this.lblAccountKey.Location = new System.Drawing.Point(11, 132);
            this.lblAccountKey.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAccountKey.Name = "lblAccountKey";
            this.lblAccountKey.Size = new System.Drawing.Size(185, 25);
            this.lblAccountKey.TabIndex = 33;
            this.lblAccountKey.Text = "Azure Access Key :";
            // 
            // txtAzureAccountKey
            // 
            this.txtAzureAccountKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAzureAccountKey.Enabled = false;
            this.txtAzureAccountKey.Location = new System.Drawing.Point(204, 127);
            this.txtAzureAccountKey.Margin = new System.Windows.Forms.Padding(6);
            this.txtAzureAccountKey.Name = "txtAzureAccountKey";
            this.txtAzureAccountKey.Size = new System.Drawing.Size(1146, 29);
            this.txtAzureAccountKey.TabIndex = 32;
            // 
            // txtAzureAccount
            // 
            this.txtAzureAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAzureAccount.Enabled = false;
            this.txtAzureAccount.Location = new System.Drawing.Point(204, 79);
            this.txtAzureAccount.Margin = new System.Windows.Forms.Padding(6);
            this.txtAzureAccount.Name = "txtAzureAccount";
            this.txtAzureAccount.Size = new System.Drawing.Size(371, 29);
            this.txtAzureAccount.TabIndex = 30;
            // 
            // lblAzureAccount
            // 
            this.lblAzureAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAzureAccount.AutoSize = true;
            this.lblAzureAccount.Location = new System.Drawing.Point(11, 84);
            this.lblAzureAccount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAzureAccount.Name = "lblAzureAccount";
            this.lblAzureAccount.Size = new System.Drawing.Size(152, 25);
            this.lblAzureAccount.TabIndex = 31;
            this.lblAzureAccount.Text = "Azure Account :";
            // 
            // directionGroup
            // 
            this.directionGroup.Controls.Add(this.chkMoveAnnotations);
            this.directionGroup.Controls.Add(this.chkMigrateAttachments);
            this.directionGroup.Controls.Add(this.chkMigrateAnnotations);
            this.directionGroup.Controls.Add(this.rdoOutbound);
            this.directionGroup.Controls.Add(this.rdoInbound);
            this.directionGroup.Location = new System.Drawing.Point(17, 321);
            this.directionGroup.Margin = new System.Windows.Forms.Padding(6);
            this.directionGroup.Name = "directionGroup";
            this.directionGroup.Padding = new System.Windows.Forms.Padding(6);
            this.directionGroup.Size = new System.Drawing.Size(601, 168);
            this.directionGroup.TabIndex = 7;
            this.directionGroup.TabStop = false;
            this.directionGroup.Text = "Migration";
            // 
            // chkMoveAnnotations
            // 
            this.chkMoveAnnotations.AutoSize = true;
            this.chkMoveAnnotations.Location = new System.Drawing.Point(29, 126);
            this.chkMoveAnnotations.Margin = new System.Windows.Forms.Padding(6);
            this.chkMoveAnnotations.Name = "chkMoveAnnotations";
            this.chkMoveAnnotations.Size = new System.Drawing.Size(272, 29);
            this.chkMoveAnnotations.TabIndex = 24;
            this.chkMoveAnnotations.Text = "Move Annotations External";
            this.chkMoveAnnotations.UseVisualStyleBackColor = true;
            this.chkMoveAnnotations.CheckedChanged += new System.EventHandler(this.chkMoveAnnotations_CheckedChanged);
            // 
            // chkMigrateAttachments
            // 
            this.chkMigrateAttachments.AutoSize = true;
            this.chkMigrateAttachments.Checked = true;
            this.chkMigrateAttachments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMigrateAttachments.Location = new System.Drawing.Point(275, 89);
            this.chkMigrateAttachments.Margin = new System.Windows.Forms.Padding(6);
            this.chkMigrateAttachments.Name = "chkMigrateAttachments";
            this.chkMigrateAttachments.Size = new System.Drawing.Size(270, 29);
            this.chkMigrateAttachments.TabIndex = 16;
            this.chkMigrateAttachments.Text = "Migrate Email Attachments";
            this.chkMigrateAttachments.UseVisualStyleBackColor = true;
            // 
            // chkMigrateAnnotations
            // 
            this.chkMigrateAnnotations.AutoSize = true;
            this.chkMigrateAnnotations.Checked = true;
            this.chkMigrateAnnotations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMigrateAnnotations.Location = new System.Drawing.Point(29, 89);
            this.chkMigrateAnnotations.Margin = new System.Windows.Forms.Padding(6);
            this.chkMigrateAnnotations.Name = "chkMigrateAnnotations";
            this.chkMigrateAnnotations.Size = new System.Drawing.Size(212, 29);
            this.chkMigrateAnnotations.TabIndex = 15;
            this.chkMigrateAnnotations.Text = "Migrate Annotations";
            this.chkMigrateAnnotations.UseVisualStyleBackColor = true;
            // 
            // rdoOutbound
            // 
            this.rdoOutbound.AutoSize = true;
            this.rdoOutbound.Checked = true;
            this.rdoOutbound.Location = new System.Drawing.Point(29, 35);
            this.rdoOutbound.Margin = new System.Windows.Forms.Padding(6);
            this.rdoOutbound.Name = "rdoOutbound";
            this.rdoOutbound.Size = new System.Drawing.Size(182, 29);
            this.rdoOutbound.TabIndex = 2;
            this.rdoOutbound.TabStop = true;
            this.rdoOutbound.Text = "CRM -> External";
            this.rdoOutbound.UseVisualStyleBackColor = true;
            // 
            // rdoInbound
            // 
            this.rdoInbound.AutoSize = true;
            this.rdoInbound.Location = new System.Drawing.Point(277, 35);
            this.rdoInbound.Margin = new System.Windows.Forms.Padding(6);
            this.rdoInbound.Name = "rdoInbound";
            this.rdoInbound.Size = new System.Drawing.Size(182, 29);
            this.rdoInbound.TabIndex = 3;
            this.rdoInbound.Text = "External -> CRM";
            this.rdoInbound.UseVisualStyleBackColor = true;
            this.rdoInbound.CheckedChanged += new System.EventHandler(this.rdoInbound_CheckedChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(1303, 37);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(90, 42);
            this.lblProgress.TabIndex = 18;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Location = new System.Drawing.Point(18, 85);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1373, 68);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgbProgress
            // 
            this.pgbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbProgress.Location = new System.Drawing.Point(18, 37);
            this.pgbProgress.Margin = new System.Windows.Forms.Padding(6);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(1274, 42);
            this.pgbProgress.Step = 1;
            this.pgbProgress.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Errors :";
            // 
            // btnMigrate
            // 
            this.btnMigrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMigrate.Location = new System.Drawing.Point(1108, 490);
            this.btnMigrate.Margin = new System.Windows.Forms.Padding(6);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(284, 100);
            this.btnMigrate.TabIndex = 9;
            this.btnMigrate.Text = "Migrate";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(18, 209);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(6);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(1372, 266);
            this.txtOutput.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.pgbProgress);
            this.groupBox3.Controls.Add(this.txtOutput);
            this.groupBox3.Controls.Add(this.lblProgress);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnMigrate);
            this.groupBox3.Controls.Add(this.lblMessage);
            this.groupBox3.Location = new System.Drawing.Point(27, 791);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1416, 609);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // rdoOAuth
            // 
            this.rdoOAuth.AutoSize = true;
            this.rdoOAuth.Location = new System.Drawing.Point(507, 35);
            this.rdoOAuth.Margin = new System.Windows.Forms.Padding(6);
            this.rdoOAuth.Name = "rdoOAuth";
            this.rdoOAuth.Size = new System.Drawing.Size(94, 29);
            this.rdoOAuth.TabIndex = 11;
            this.rdoOAuth.Text = "OAuth";
            this.rdoOAuth.UseVisualStyleBackColor = true;
            this.rdoOAuth.CheckedChanged += new System.EventHandler(this.rdoOAuth_CheckedChanged);
            // 
            // rdoClientSecret
            // 
            this.rdoClientSecret.AutoSize = true;
            this.rdoClientSecret.Location = new System.Drawing.Point(613, 35);
            this.rdoClientSecret.Margin = new System.Windows.Forms.Padding(6);
            this.rdoClientSecret.Name = "rdoClientSecret";
            this.rdoClientSecret.Size = new System.Drawing.Size(149, 29);
            this.rdoClientSecret.TabIndex = 12;
            this.rdoClientSecret.Text = "Client Secret";
            this.rdoClientSecret.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 1422);
            this.Controls.Add(this.migrateGroup);
            this.Controls.Add(this.authGroup);
            this.Controls.Add(this.txtOrganizationServiceUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1491, 1486);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Crm Binary Storage Migration";
            this.Load += new System.EventHandler(this.Main_Load);
            this.authGroup.ResumeLayout(false);
            this.authGroup.PerformLayout();
            this.migrateGroup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWaitDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udThreadCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.directionGroup.ResumeLayout(false);
            this.directionGroup.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOrganizationServiceUrl;
		private System.Windows.Forms.RadioButton rdoADAuth;
		private System.Windows.Forms.RadioButton rdoIFDAuth;
		private System.Windows.Forms.GroupBox authGroup;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.GroupBox migrateGroup;
		private System.Windows.Forms.Button btnMigrate;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.GroupBox directionGroup;
		private System.Windows.Forms.RadioButton rdoOutbound;
		private System.Windows.Forms.RadioButton rdoInbound;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.ProgressBar pgbProgress;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkMigrateAttachments;
		private System.Windows.Forms.CheckBox chkMigrateAnnotations;
		private System.Windows.Forms.CheckBox chkMoveAnnotations;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblCompression;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtAES256Salt;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtAES256Key;
		private System.Windows.Forms.Label lblAccountKey;
		private System.Windows.Forms.TextBox txtAzureAccountKey;
		private System.Windows.Forms.TextBox txtAzureAccount;
		private System.Windows.Forms.Label lblAzureAccount;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkPluginStepsManage;
		private System.Windows.Forms.NumericUpDown udWaitDelay;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown udThreadCount;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblEncryption;
        private System.Windows.Forms.RadioButton rdoAzureSasToken;
        private System.Windows.Forms.RadioButton rdoAzureAccessKey;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoOAuth;
        private System.Windows.Forms.RadioButton rdoClientSecret;
    }
}

