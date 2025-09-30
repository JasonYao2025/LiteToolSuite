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
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(24, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(921, 247);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间戳转换：";
            // 
            // tbCurrentTimestamp
            // 
            this.tbCurrentTimestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCurrentTimestamp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCurrentTimestamp.Location = new System.Drawing.Point(165, 46);
            this.tbCurrentTimestamp.Margin = new System.Windows.Forms.Padding(4);
            this.tbCurrentTimestamp.Name = "tbCurrentTimestamp";
            this.tbCurrentTimestamp.ReadOnly = true;
            this.tbCurrentTimestamp.Size = new System.Drawing.Size(200, 23);
            this.tbCurrentTimestamp.TabIndex = 59;
            // 
            // combTimeUnit2
            // 
            this.combTimeUnit2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combTimeUnit2.FormattingEnabled = true;
            this.combTimeUnit2.Items.AddRange(new object[] {
            ""});
            this.combTimeUnit2.Location = new System.Drawing.Point(793, 169);
            this.combTimeUnit2.Name = "combTimeUnit2";
            this.combTimeUnit2.Size = new System.Drawing.Size(80, 25);
            this.combTimeUnit2.TabIndex = 58;
            // 
            // btnTimeToTimestamp
            // 
            this.btnTimeToTimestamp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTimeToTimestamp.Location = new System.Drawing.Point(486, 169);
            this.btnTimeToTimestamp.Name = "btnTimeToTimestamp";
            this.btnTimeToTimestamp.Size = new System.Drawing.Size(75, 27);
            this.btnTimeToTimestamp.TabIndex = 57;
            this.btnTimeToTimestamp.Text = "转换>>";
            this.btnTimeToTimestamp.UseVisualStyleBackColor = true;
            this.btnTimeToTimestamp.Click += new System.EventHandler(this.btnTimeToTimestamp_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(386, 173);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 22);
            this.label4.TabIndex = 56;
            this.label4.Text = "北京时间";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTime
            // 
            this.dtpTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTime.Location = new System.Drawing.Point(165, 172);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(200, 23);
            this.dtpTime.TabIndex = 55;
            // 
            // tbTime
            // 
            this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTime.Location = new System.Drawing.Point(578, 106);
            this.tbTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(204, 23);
            this.tbTime.TabIndex = 54;
            // 
            // btnTimestampToTime
            // 
            this.btnTimestampToTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTimestampToTime.Location = new System.Drawing.Point(486, 104);
            this.btnTimestampToTime.Name = "btnTimestampToTime";
            this.btnTimestampToTime.Size = new System.Drawing.Size(75, 27);
            this.btnTimestampToTime.TabIndex = 53;
            this.btnTimestampToTime.Text = "转换>>";
            this.btnTimestampToTime.UseVisualStyleBackColor = true;
            this.btnTimestampToTime.Click += new System.EventHandler(this.btnTimestampToTime_Click);
            // 
            // combTimeUnit
            // 
            this.combTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTimeUnit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combTimeUnit.FormattingEnabled = true;
            this.combTimeUnit.Location = new System.Drawing.Point(389, 104);
            this.combTimeUnit.Name = "combTimeUnit";
            this.combTimeUnit.Size = new System.Drawing.Size(83, 25);
            this.combTimeUnit.TabIndex = 52;
            // 
            // tbTimestamp
            // 
            this.tbTimestamp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTimestamp.Location = new System.Drawing.Point(164, 106);
            this.tbTimestamp.Margin = new System.Windows.Forms.Padding(4);
            this.tbTimestamp.Name = "tbTimestamp";
            this.tbTimestamp.Size = new System.Drawing.Size(201, 23);
            this.tbTimestamp.TabIndex = 51;
            // 
            // tbTimestampConverted
            // 
            this.tbTimestampConverted.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTimestampConverted.Location = new System.Drawing.Point(578, 169);
            this.tbTimestampConverted.Margin = new System.Windows.Forms.Padding(4);
            this.tbTimestampConverted.Name = "tbTimestampConverted";
            this.tbTimestampConverted.Size = new System.Drawing.Size(204, 23);
            this.tbTimestampConverted.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(33, 179);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 17);
            this.label6.TabIndex = 41;
            this.label6.Text = "时间：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(790, 109);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 22);
            this.label5.TabIndex = 40;
            this.label5.Text = "北京时间";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(33, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 39;
            this.label2.Text = "时间戳：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(33, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "当前时间：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTimestamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 631);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTimestamp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间/时间戳计算窗口";
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