namespace LiteToolSuite
{
    partial class FrmVehicle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVehicle));
            this.combVehicleList = new System.Windows.Forms.ComboBox();
            this.tbDeviceId = new System.Windows.Forms.TextBox();
            this.labelVehicleName = new System.Windows.Forms.Label();
            this.labelAIDevice = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.btnResetRichtextbox = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnVehicleConfig = new System.Windows.Forms.Button();
            this.btnCopyDeviceId = new System.Windows.Forms.Button();
            this.tbDeviceModel = new System.Windows.Forms.TextBox();
            this.labelAIModel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbVehicleName = new System.Windows.Forms.TextBox();
            this.tbAIDeviceModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.combAIDevice = new System.Windows.Forms.ComboBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // combVehicleList
            // 
            this.combVehicleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.groupBox1.Controls.Add(this.btSearch);
            this.groupBox1.Controls.Add(this.tbSearchText);
            this.groupBox1.Controls.Add(this.btnResetRichtextbox);
            this.groupBox1.Controls.Add(this.treeView1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tbSearchText
            // 
            resources.ApplyResources(this.tbSearchText, "tbSearchText");
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // btnResetRichtextbox
            // 
            resources.ApplyResources(this.btnResetRichtextbox, "btnResetRichtextbox");
            this.btnResetRichtextbox.Name = "btnResetRichtextbox";
            this.btnResetRichtextbox.UseVisualStyleBackColor = true;
            this.btnResetRichtextbox.Click += new System.EventHandler(this.btnResetRichtextbox_Click);
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.Name = "treeView1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnVehicleConfig);
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
            // btnVehicleConfig
            // 
            resources.ApplyResources(this.btnVehicleConfig, "btnVehicleConfig");
            this.btnVehicleConfig.Name = "btnVehicleConfig";
            this.btnVehicleConfig.UseVisualStyleBackColor = true;
            this.btnVehicleConfig.Click += new System.EventHandler(this.btnVehicleConfig_Click);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbVehicleName);
            this.groupBox4.Controls.Add(this.tbAIDeviceModel);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.combAIDevice);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // tbVehicleName
            // 
            this.tbVehicleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbVehicleName, "tbVehicleName");
            this.tbVehicleName.Name = "tbVehicleName";
            this.tbVehicleName.ReadOnly = true;
            // 
            // tbAIDeviceModel
            // 
            this.tbAIDeviceModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbAIDeviceModel, "tbAIDeviceModel");
            this.tbAIDeviceModel.Name = "tbAIDeviceModel";
            this.tbAIDeviceModel.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // combAIDevice
            // 
            this.combAIDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combAIDevice, "combAIDevice");
            this.combAIDevice.FormattingEnabled = true;
            this.combAIDevice.Name = "combAIDevice";
            this.combAIDevice.SelectedIndexChanged += new System.EventHandler(this.combAIDevice_SelectedIndexChanged);
            // 
            // btSearch
            // 
            resources.ApplyResources(this.btSearch, "btSearch");
            this.btSearch.Name = "btSearch";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // FrmVehicle
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVehicle";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox combVehicleList;
        private System.Windows.Forms.TextBox tbDeviceId;
        private System.Windows.Forms.Label labelVehicleName;
        private System.Windows.Forms.Label labelAIDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbDeviceModel;
        private System.Windows.Forms.Label labelAIModel;
        private System.Windows.Forms.Button btnCopyDeviceId;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbVehicleName;
        private System.Windows.Forms.TextBox tbAIDeviceModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combAIDevice;
        private System.Windows.Forms.Button btnVehicleConfig;
        private System.Windows.Forms.Button btnResetRichtextbox;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.Button btSearch;
    }
}