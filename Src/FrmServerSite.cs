using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite
{
    public partial class FrmServerSite : Form
    {
        public FrmServerSite()
        {
            InitializeComponent();

            tbUserName.Text = LiteToolSuite.Properties.Resources.PhoneNumberPrompt; 
            tbUserName.ForeColor = Color.Gray;

            tbUserName.GotFocus += (s, e) => {
                if (tbUserName.Text == LiteToolSuite.Properties.Resources.PhoneNumberPrompt)
                {
                    tbUserName.Text = "";
                    tbUserName.ForeColor = Color.Black;
                }
            };

            tbUserName.LostFocus += (s, e) => {
                if (string.IsNullOrEmpty(tbUserName.Text))
                {
                    tbUserName.Text = LiteToolSuite.Properties.Resources.PhoneNumberPrompt;
                    tbUserName.ForeColor = Color.Gray;
                }
            };

        }

        private void btSaveSite_Click(object sender, EventArgs e)
        {
            string siteName = tbSiteName.Text.Trim();
            string siteURL = tbSiteURL.Text.Trim();
            string mobile = tbUserName.Text.Trim();
            string password = tbPassword.Text.Trim();
            if (string.IsNullOrEmpty(siteName) || string.IsNullOrEmpty(siteURL) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("站点简称，站点URL，用户名和密码都不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!RegexHelper.IsValid11DigitNumber(mobile))
            {
                MessageBox.Show("请输入11数字手机号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

           
            if (!siteURL.Contains("api"))  //一般后端URL都有api字样
            {
                string text = "这里要输入后端接口URL, 一般都有 【api】 字样，确认【" + siteURL + "】 地址正确吗？";
                DialogResult result = MessageBox.Show(text: text, caption: "提醒", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveSiteToDb(siteName, siteURL, mobile, password);
                }       
               
            } 
            else   
            {
                SaveSiteToDb(siteName,siteURL,mobile,password);
            }

        }

        private void SaveSiteToDb(string siteName,string siteURL,string mobile,string password)
        {
            try
            {
                string sqliteSQL = string.Format("SELECT id FROM server_site WHERE site_url='{0}'", siteURL);
                var dataTable = Program.SQLiteHelper.ExecuteDataTable(sqliteSQL);

                if (dataTable.Rows.Count > 0)  //可以查到条目 
                {
                    MessageBox.Show("站点已经存在，不能重复保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                sqliteSQL = string.Format("INSERT INTO server_site(site_name,site_url,user_name,passwd) VALUES ('{0}','{1}','{2}','{3}')", siteName, siteURL, mobile, password);
                var val = Program.SQLiteHelper.ExecuteNonQuery(sqliteSQL);  //成功插入返回数量
                if (val > 0)
                {
                    MessageBox.Show("恭喜添加站点成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex) { }
        }
    }
}
