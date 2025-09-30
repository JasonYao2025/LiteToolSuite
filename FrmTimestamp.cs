using Common;
using Newtonsoft.Json;
using LiteToolSuite.BLL;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite
{
    public partial class FrmTimestamp : Form
    {
        public FrmTimestamp()
        {
            InitializeComponent();

            InitializeTimer();    //初始化Timer

            var timeUnit = new Dictionary<string, string>();
            timeUnit.Add("秒(s)", "秒(s)");
            timeUnit.Add("毫秒(ms)", "毫秒(ms)");

            combTimeUnit.DataSource = new BindingSource(timeUnit, null);
            combTimeUnit.DisplayMember = "Key";
            combTimeUnit.ValueMember = "Value";
            combTimeUnit.SelectedIndex = 0;

            combTimeUnit2.DataSource = new BindingSource(timeUnit, null);
            combTimeUnit2.DisplayMember = "Key";
            combTimeUnit2.ValueMember = "Value";            
            combTimeUnit2.SelectedIndex = 0;

            dtpTime.Format = DateTimePickerFormat.Custom;
            dtpTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpTime.ShowUpDown = true;  //启用数字调整模式
        
        }

        private void InitializeTimer() 
        { 
            Timer timer = new Timer();
            timer.Interval=1000;      //时间间隔1000ms
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            //更新label，显示时间戳
           tbCurrentTimestamp.Text = TimeHelper.DateTimeToUnixTimestamp(DateTime.Now).ToString();
        }

        private void btnTimestampToTime_Click(object sender, EventArgs e)
        {
            string txtTimestamp = tbTimestamp.Text.Trim();
            DateTime dateTime;
            if (string.IsNullOrEmpty(txtTimestamp)) 
            {
                MessageBox.Show("请输入时间戳", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            long timestamp = StringHelper.StringToInt(txtTimestamp); //如果txtTimestamp不是数字或者是空，返回0
            if (timestamp > 0 && combTimeUnit.SelectedValue.ToString() == "秒(s)")
            {
                dateTime = TimeHelper.UnixTimestampToDateTime(timestamp);
                tbTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timestamp > 0 && combTimeUnit.SelectedValue.ToString() == "毫秒(ms)")
            {
                dateTime = TimeHelper.MilliUnixTimeStampToDatetime(txtTimestamp);
                tbTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else 
            {
                MessageBox.Show("请输入有效时间戳", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTimeToTimestamp_Click(object sender, EventArgs e)
        {
            long timestamp = 0;
            
          
            if (combTimeUnit2.SelectedValue.ToString() == "秒(s)")
            {
                timestamp = TimeHelper.GetTimestamp(dtpTime.Text);
                tbTimestampConverted.Text = timestamp.ToString();
            }
            else
            {
                MessageBox.Show("暂未实现", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
