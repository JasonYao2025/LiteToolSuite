namespace LiteToolSuite
{
    partial class FrmInputContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInputContent));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbLowerChar = new System.Windows.Forms.CheckBox();
            this.nudContentLength = new System.Windows.Forms.NumericUpDown();
            this.cbOtherChar = new System.Windows.Forms.CheckBox();
            this.cbSpecialChar = new System.Windows.Forms.CheckBox();
            this.cbNumber = new System.Windows.Forms.CheckBox();
            this.cbUpperChar = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbInputContent = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCpToClipboard = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnHTML = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContentLength)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbLowerChar);
            this.groupBox1.Controls.Add(this.nudContentLength);
            this.groupBox1.Controls.Add(this.cbOtherChar);
            this.groupBox1.Controls.Add(this.cbSpecialChar);
            this.groupBox1.Controls.Add(this.cbNumber);
            this.groupBox1.Controls.Add(this.cbUpperChar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbLowerChar
            // 
            resources.ApplyResources(this.cbLowerChar, "cbLowerChar");
            this.cbLowerChar.Name = "cbLowerChar";
            this.cbLowerChar.UseVisualStyleBackColor = true;
            // 
            // nudContentLength
            // 
            resources.ApplyResources(this.nudContentLength, "nudContentLength");
            this.nudContentLength.Name = "nudContentLength";
            this.nudContentLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // cbOtherChar
            // 
            resources.ApplyResources(this.cbOtherChar, "cbOtherChar");
            this.cbOtherChar.Name = "cbOtherChar";
            this.cbOtherChar.UseVisualStyleBackColor = true;
            // 
            // cbSpecialChar
            // 
            resources.ApplyResources(this.cbSpecialChar, "cbSpecialChar");
            this.cbSpecialChar.Name = "cbSpecialChar";
            this.cbSpecialChar.UseVisualStyleBackColor = true;
            // 
            // cbNumber
            // 
            resources.ApplyResources(this.cbNumber, "cbNumber");
            this.cbNumber.Name = "cbNumber";
            this.cbNumber.UseVisualStyleBackColor = true;
            // 
            // cbUpperChar
            // 
            resources.ApplyResources(this.cbUpperChar, "cbUpperChar");
            this.cbUpperChar.Name = "cbUpperChar";
            this.cbUpperChar.UseVisualStyleBackColor = true;
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
            // rtbInputContent
            // 
            resources.ApplyResources(this.rtbInputContent, "rtbInputContent");
            this.rtbInputContent.Name = "rtbInputContent";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnCpToClipboard);
            this.groupBox2.Controls.Add(this.rtbInputContent);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCpToClipboard
            // 
            resources.ApplyResources(this.btnCpToClipboard, "btnCpToClipboard");
            this.btnCpToClipboard.Name = "btnCpToClipboard";
            this.btnCpToClipboard.UseVisualStyleBackColor = true;
            this.btnCpToClipboard.Click += new System.EventHandler(this.btnCpToClipboard_Click);
            // 
            // btnGenerate
            // 
            resources.ApplyResources(this.btnGenerate, "btnGenerate");
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnHTML
            // 
            resources.ApplyResources(this.btnHTML, "btnHTML");
            this.btnHTML.Name = "btnHTML";
            this.btnHTML.UseVisualStyleBackColor = true;
            this.btnHTML.Click += new System.EventHandler(this.btnHTML_Click);
            // 
            // FrmInputContent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnHTML);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInputContent";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContentLength)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbInputContent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCpToClipboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbOtherChar;
        private System.Windows.Forms.CheckBox cbSpecialChar;
        private System.Windows.Forms.CheckBox cbNumber;
        private System.Windows.Forms.CheckBox cbUpperChar;
        private System.Windows.Forms.NumericUpDown nudContentLength;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckBox cbLowerChar;
        private System.Windows.Forms.Button btnHTML;
        private System.Windows.Forms.Button btnClear;
    }
}