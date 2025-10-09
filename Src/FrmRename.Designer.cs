namespace LiteToolSuite
{
    partial class FrmRename
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRename));
            this.btSelectFolder = new System.Windows.Forms.Button();
            this.tbSourcePath = new System.Windows.Forms.TextBox();
            this.rtbChangeHistory = new System.Windows.Forms.RichTextBox();
            this.btChangeName = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.btFileName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFirstCode = new System.Windows.Forms.TextBox();
            this.tbSecondCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btSelectFolder
            // 
            resources.ApplyResources(this.btSelectFolder, "btSelectFolder");
            this.btSelectFolder.Name = "btSelectFolder";
            this.btSelectFolder.UseVisualStyleBackColor = true;
            this.btSelectFolder.Click += new System.EventHandler(this.btSelectFolder_Click);
            // 
            // tbSourcePath
            // 
            resources.ApplyResources(this.tbSourcePath, "tbSourcePath");
            this.tbSourcePath.Name = "tbSourcePath";
            // 
            // rtbChangeHistory
            // 
            resources.ApplyResources(this.rtbChangeHistory, "rtbChangeHistory");
            this.rtbChangeHistory.Name = "rtbChangeHistory";
            // 
            // btChangeName
            // 
            resources.ApplyResources(this.btChangeName, "btChangeName");
            this.btChangeName.Name = "btChangeName";
            this.btChangeName.UseVisualStyleBackColor = true;
            this.btChangeName.Click += new System.EventHandler(this.btChangeName_Click);
            // 
            // btReset
            // 
            resources.ApplyResources(this.btReset, "btReset");
            this.btReset.Name = "btReset";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btFileName
            // 
            resources.ApplyResources(this.btFileName, "btFileName");
            this.btFileName.Name = "btFileName";
            this.btFileName.UseVisualStyleBackColor = true;
            this.btFileName.Click += new System.EventHandler(this.btFileName_Click);
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tbFirstCode
            // 
            resources.ApplyResources(this.tbFirstCode, "tbFirstCode");
            this.tbFirstCode.Name = "tbFirstCode";
            // 
            // tbSecondCode
            // 
            resources.ApplyResources(this.tbSecondCode, "tbSecondCode");
            this.tbSecondCode.Name = "tbSecondCode";
            // 
            // FrmRename
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbSecondCode);
            this.Controls.Add(this.tbFirstCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btFileName);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btChangeName);
            this.Controls.Add(this.rtbChangeHistory);
            this.Controls.Add(this.tbSourcePath);
            this.Controls.Add(this.btSelectFolder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btSelectFolder;
        private System.Windows.Forms.TextBox tbSourcePath;
        private System.Windows.Forms.RichTextBox rtbChangeHistory;
        private System.Windows.Forms.Button btChangeName;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFirstCode;
        private System.Windows.Forms.TextBox tbSecondCode;
    }
}

