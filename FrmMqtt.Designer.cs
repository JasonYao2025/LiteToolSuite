namespace LiteToolSuite{
    partial class FrmMqtt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMqtt));
            this.btnConnectMqttServer = new System.Windows.Forms.Button();
            this.rtbSubContent = new System.Windows.Forms.RichTextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.combVehicleList = new System.Windows.Forms.ComboBox();
            this.tbDeviceId = new System.Windows.Forms.TextBox();
            this.labelVehicleName = new System.Windows.Forms.Label();
            this.labelAIDevice = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCopyDeviceId = new System.Windows.Forms.Button();
            this.tbDeviceModel = new System.Windows.Forms.TextBox();
            this.labelAIModel = new System.Windows.Forms.Label();
            this.btPub = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.combPubTopic = new System.Windows.Forms.ComboBox();
            this.btnSub = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbTopic = new System.Windows.Forms.CheckedListBox();
            this.btCancelSub = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectMqttServer
            // 
            resources.ApplyResources(this.btnConnectMqttServer, "btnConnectMqttServer");
            this.btnConnectMqttServer.Name = "btnConnectMqttServer";
            this.btnConnectMqttServer.UseVisualStyleBackColor = true;
            this.btnConnectMqttServer.Click += new System.EventHandler(this.btnConnectMqttServer_Click);
            // 
            // rtbSubContent
            // 
            resources.ApplyResources(this.rtbSubContent, "rtbSubContent");
            this.rtbSubContent.Name = "rtbSubContent";
            this.rtbSubContent.ReadOnly = true;
            // 
            // btnDisconnect
            // 
            resources.ApplyResources(this.btnDisconnect, "btnDisconnect");
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // combVehicleList
            // 
            resources.ApplyResources(this.combVehicleList, "combVehicleList");
            this.combVehicleList.FormattingEnabled = true;
            this.combVehicleList.Name = "combVehicleList";
            this.combVehicleList.SelectedIndexChanged += new System.EventHandler(this.combVehicleList_SelectedIndexChanged);
            // 
            // tbDeviceId
            // 
            this.tbDeviceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbDeviceId, "tbDeviceId");
            this.tbDeviceId.Name = "tbDeviceId";
            this.tbDeviceId.ReadOnly = true;
            // 
            // labelVehicleName
            // 
            resources.ApplyResources(this.labelVehicleName, "labelVehicleName");
            this.labelVehicleName.Name = "labelVehicleName";
            // 
            // labelAIDevice
            // 
            resources.ApplyResources(this.labelAIDevice, "labelAIDevice");
            this.labelAIDevice.Name = "labelAIDevice";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.rtbSubContent);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCopyDeviceId);
            this.groupBox3.Controls.Add(this.tbDeviceModel);
            this.groupBox3.Controls.Add(this.tbDeviceId);
            this.groupBox3.Controls.Add(this.labelAIDevice);
            this.groupBox3.Controls.Add(this.labelAIModel);
            this.groupBox3.Controls.Add(this.labelVehicleName);
            this.groupBox3.Controls.Add(this.combVehicleList);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnCopyDeviceId
            // 
            resources.ApplyResources(this.btnCopyDeviceId, "btnCopyDeviceId");
            this.btnCopyDeviceId.Name = "btnCopyDeviceId";
            this.btnCopyDeviceId.UseVisualStyleBackColor = true;
            this.btnCopyDeviceId.Click += new System.EventHandler(this.btnCopyDeviceId_Click);
            // 
            // tbDeviceModel
            // 
            this.tbDeviceModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbDeviceModel, "tbDeviceModel");
            this.tbDeviceModel.Name = "tbDeviceModel";
            this.tbDeviceModel.ReadOnly = true;
            // 
            // labelAIModel
            // 
            resources.ApplyResources(this.labelAIModel, "labelAIModel");
            this.labelAIModel.Name = "labelAIModel";
            // 
            // btPub
            // 
            resources.ApplyResources(this.btPub, "btPub");
            this.btPub.Name = "btPub";
            this.btPub.UseVisualStyleBackColor = true;
            this.btPub.Click += new System.EventHandler(this.btPub_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.combPubTopic);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btPub);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // combPubTopic
            // 
            resources.ApplyResources(this.combPubTopic, "combPubTopic");
            this.combPubTopic.FormattingEnabled = true;
            this.combPubTopic.Name = "combPubTopic";
            // 
            // btnSub
            // 
            resources.ApplyResources(this.btnSub, "btnSub");
            this.btnSub.Name = "btnSub";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbTopic);
            this.groupBox2.Controls.Add(this.btCancelSub);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnSub);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // clbTopic
            // 
            resources.ApplyResources(this.clbTopic, "clbTopic");
            this.clbTopic.FormattingEnabled = true;
            this.clbTopic.Name = "clbTopic";
            // 
            // btCancelSub
            // 
            resources.ApplyResources(this.btCancelSub, "btCancelSub");
            this.btCancelSub.Name = "btCancelSub";
            this.btCancelSub.UseVisualStyleBackColor = true;
            this.btCancelSub.Click += new System.EventHandler(this.btCancelSub_Click);
            // 
            // FrmMqtt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnConnectMqttServer);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMqtt";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConnectMqttServer;
        private System.Windows.Forms.RichTextBox rtbSubContent;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox combVehicleList;
        private System.Windows.Forms.TextBox tbDeviceId;
        private System.Windows.Forms.Label labelVehicleName;
        private System.Windows.Forms.Label labelAIDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbDeviceModel;
        private System.Windows.Forms.Label labelAIModel;
        private System.Windows.Forms.Button btnCopyDeviceId;
        private System.Windows.Forms.Button btPub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combPubTopic;
        private System.Windows.Forms.Button btCancelSub;
        private System.Windows.Forms.CheckedListBox clbTopic;
        private System.Windows.Forms.Button btnReset;
    }
}