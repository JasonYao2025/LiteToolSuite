namespace LiteToolSuite
{
    partial class FrmFunctionPortal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFunctionPortal));
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.combLang = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelDeveloper
            // 
            resources.ApplyResources(this.labelDeveloper, "labelDeveloper");
            this.labelDeveloper.Name = "labelDeveloper";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // combLang
            // 
            this.combLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combLang.FormattingEnabled = true;
            resources.ApplyResources(this.combLang, "combLang");
            this.combLang.Name = "combLang";
            this.combLang.SelectedIndexChanged += new System.EventHandler(this.combLang_SelectedIndexChanged);
            // 
            // FrmFunctionPortal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combLang);
            this.Controls.Add(this.labelDeveloper);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFunctionPortal";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combLang;
    }
}