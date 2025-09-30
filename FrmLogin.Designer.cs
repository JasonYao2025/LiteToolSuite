namespace LiteToolSuite{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMobile = new System.Windows.Forms.TextBox();
            this.tbPasswd = new System.Windows.Forms.TextBox();
            this.combSite = new System.Windows.Forms.ComboBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.combLang = new System.Windows.Forms.ComboBox();
            this.btAddSite = new System.Windows.Forms.Button();
            this.btRemoveSite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
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
            // tbMobile
            // 
            resources.ApplyResources(this.tbMobile, "tbMobile");
            this.tbMobile.Name = "tbMobile";
            // 
            // tbPasswd
            // 
            resources.ApplyResources(this.tbPasswd, "tbPasswd");
            this.tbPasswd.Name = "tbPasswd";
            // 
            // combSite
            // 
            this.combSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSite.FormattingEnabled = true;
            resources.ApplyResources(this.combSite, "combSite");
            this.combSite.Name = "combSite";
            this.combSite.SelectedIndexChanged += new System.EventHandler(this.combSite_SelectedIndexChanged);
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.Name = "labelTitle";
            // 
            // combLang
            // 
            this.combLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combLang.FormattingEnabled = true;
            resources.ApplyResources(this.combLang, "combLang");
            this.combLang.Name = "combLang";
            this.combLang.SelectedIndexChanged += new System.EventHandler(this.combLang_SelectedIndexChanged);
            // 
            // btAddSite
            // 
            resources.ApplyResources(this.btAddSite, "btAddSite");
            this.btAddSite.Name = "btAddSite";
            this.btAddSite.UseVisualStyleBackColor = true;
            this.btAddSite.Click += new System.EventHandler(this.btAddSite_Click);
            // 
            // btRemoveSite
            // 
            resources.ApplyResources(this.btRemoveSite, "btRemoveSite");
            this.btRemoveSite.Name = "btRemoveSite";
            this.btRemoveSite.UseVisualStyleBackColor = true;
            this.btRemoveSite.Click += new System.EventHandler(this.btRemoveSite_Click);
            // 
            // FrmLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btRemoveSite);
            this.Controls.Add(this.btAddSite);
            this.Controls.Add(this.combLang);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.combSite);
            this.Controls.Add(this.tbPasswd);
            this.Controls.Add(this.tbMobile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMobile;
        private System.Windows.Forms.TextBox tbPasswd;
        private System.Windows.Forms.ComboBox combSite;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ComboBox combLang;
        private System.Windows.Forms.Button btAddSite;
        private System.Windows.Forms.Button btRemoveSite;
    }
}