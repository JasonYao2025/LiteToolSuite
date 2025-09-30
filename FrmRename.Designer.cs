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
            this.btSelectFolder.Location = new System.Drawing.Point(83, 66);
            this.btSelectFolder.Name = "btSelectFolder";
            this.btSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btSelectFolder.TabIndex = 1;
            this.btSelectFolder.Text = "选择目录";
            this.btSelectFolder.UseVisualStyleBackColor = true;
            this.btSelectFolder.Click += new System.EventHandler(this.btSelectFolder_Click);
            // 
            // tbSourcePath
            // 
            this.tbSourcePath.Location = new System.Drawing.Point(185, 66);
            this.tbSourcePath.Name = "tbSourcePath";
            this.tbSourcePath.Size = new System.Drawing.Size(558, 21);
            this.tbSourcePath.TabIndex = 2;
            // 
            // rtbChangeHistory
            // 
            this.rtbChangeHistory.Location = new System.Drawing.Point(83, 237);
            this.rtbChangeHistory.Name = "rtbChangeHistory";
            this.rtbChangeHistory.Size = new System.Drawing.Size(660, 330);
            this.rtbChangeHistory.TabIndex = 3;
            this.rtbChangeHistory.Text = "";
            // 
            // btChangeName
            // 
            this.btChangeName.Location = new System.Drawing.Point(267, 174);
            this.btChangeName.Name = "btChangeName";
            this.btChangeName.Size = new System.Drawing.Size(75, 23);
            this.btChangeName.TabIndex = 4;
            this.btChangeName.Text = "开始改名";
            this.btChangeName.UseVisualStyleBackColor = true;
            this.btChangeName.Click += new System.EventHandler(this.btChangeName_Click);
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(448, 174);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 23);
            this.btReset.TabIndex = 5;
            this.btReset.Text = "重置";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btFileName
            // 
            this.btFileName.Location = new System.Drawing.Point(668, 120);
            this.btFileName.Name = "btFileName";
            this.btFileName.Size = new System.Drawing.Size(75, 23);
            this.btFileName.TabIndex = 7;
            this.btFileName.Text = "确定";
            this.btFileName.UseVisualStyleBackColor = true;
            this.btFileName.Click += new System.EventHandler(this.btFileName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "文件名格式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "第一码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "第二码：";
            // 
            // tbFirstCode
            // 
            this.tbFirstCode.Location = new System.Drawing.Point(242, 120);
            this.tbFirstCode.Name = "tbFirstCode";
            this.tbFirstCode.Size = new System.Drawing.Size(100, 21);
            this.tbFirstCode.TabIndex = 12;
            this.tbFirstCode.Text = "天车";
            // 
            // tbSecondCode
            // 
            this.tbSecondCode.Location = new System.Drawing.Point(423, 120);
            this.tbSecondCode.Name = "tbSecondCode";
            this.tbSecondCode.Size = new System.Drawing.Size(100, 21);
            this.tbSecondCode.TabIndex = 13;
            this.tbSecondCode.Text = "Cam";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 581);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QA小工具集主界面";
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

