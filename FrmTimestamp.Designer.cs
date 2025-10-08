namespace LiteToolSuite
{
    partial class FrmTimestamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimestamp));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbCurrentTimestamp = new System.Windows.Forms.TextBox();
            this.combTimeUnit2 = new System.Windows.Forms.ComboBox();
            this.btnTimeToTimestamp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.btnTimestampToTime = new System.Windows.Forms.Button();
            this.combTimeUnit = new System.Windows.Forms.ComboBox();
            this.tbTimestamp = new System.Windows.Forms.TextBox();
            this.tbTimestampConverted = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbCurrentTimestamp);
            this.groupBox1.Controls.Add(this.combTimeUnit2);
            this.groupBox1.Controls.Add(this.btnTimeToTimestamp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.tbTime);
            this.groupBox1.Controls.Add(this.btnTimestampToTime);
            this.groupBox1.Controls.Add(this.combTimeUnit);
            this.groupBox1.Controls.Add(this.tbTimestamp);
            this.groupBox1.Controls.Add(this.tbTimestampConverted);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tbCurrentTimestamp
            // 
            this.tbCurrentTimestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbCurrentTimestamp, "tbCurrentTimestamp");
            this.tbCurrentTimestamp.Name = "tbCurrentTimestamp";
            this.tbCurrentTimestamp.ReadOnly = true;
            // 
            // combTimeUnit2
            // 
            resources.ApplyResources(this.combTimeUnit2, "combTimeUnit2");
            this.combTimeUnit2.FormattingEnabled = true;
            this.combTimeUnit2.Items.AddRange(new object[] {
            resources.GetString("combTimeUnit2.Items")});
            this.combTimeUnit2.Name = "combTimeUnit2";
            // 
            // btnTimeToTimestamp
            // 
            resources.ApplyResources(this.btnTimeToTimestamp, "btnTimeToTimestamp");
            this.btnTimeToTimestamp.Name = "btnTimeToTimestamp";
            this.btnTimeToTimestamp.UseVisualStyleBackColor = true;
            this.btnTimeToTimestamp.Click += new System.EventHandler(this.btnTimeToTimestamp_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtpTime
            // 
            resources.ApplyResources(this.dtpTime, "dtpTime");
            this.dtpTime.Name = "dtpTime";
            // 
            // tbTime
            // 
            this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbTime, "tbTime");
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            // 
            // btnTimestampToTime
            // 
            resources.ApplyResources(this.btnTimestampToTime, "btnTimestampToTime");
            this.btnTimestampToTime.Name = "btnTimestampToTime";
            this.btnTimestampToTime.UseVisualStyleBackColor = true;
            this.btnTimestampToTime.Click += new System.EventHandler(this.btnTimestampToTime_Click);
            // 
            // combTimeUnit
            // 
            this.combTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.combTimeUnit, "combTimeUnit");
            this.combTimeUnit.FormattingEnabled = true;
            this.combTimeUnit.Name = "combTimeUnit";
            // 
            // tbTimestamp
            // 
            resources.ApplyResources(this.tbTimestamp, "tbTimestamp");
            this.tbTimestamp.Name = "tbTimestamp";
            // 
            // tbTimestampConverted
            // 
            resources.ApplyResources(this.tbTimestampConverted, "tbTimestampConverted");
            this.tbTimestampConverted.Name = "tbTimestampConverted";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
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
            // FrmTimestamp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTimestamp";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTimestampConverted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTimestamp;
        private System.Windows.Forms.Button btnTimestampToTime;
        private System.Windows.Forms.ComboBox combTimeUnit;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button btnTimeToTimestamp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combTimeUnit2;
        private System.Windows.Forms.TextBox tbCurrentTimestamp;
    }
}