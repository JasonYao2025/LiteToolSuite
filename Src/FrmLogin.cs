using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.Models;
using LiteToolSuite.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Common;

namespace LiteToolSuite
{
    public partial class FrmLogin : Form
    {

        private ProfileModel profile;
        private Dictionary<string, string> vehicles;

        public FrmLogin()
        {
            InitializeComponent();

            //简化FrmLogin的功能，Login只是负责URL和Token获取，并写入FrmFunctionPortal的全局变量
                        
           FilloutCombSite(0);

            //都以站点名为标准位置进行调整
            label2.Location = new Point(label1.Left - 10, label1.Top + 60);
            label3.Location = new Point(label1.Left - 10, label1.Top + 120);
            btAddSite.Location = new Point(combSite.Right + 20, combSite.Top);
            btRemoveSite.Location = new Point(btAddSite.Right + 20, btAddSite.Top);


            var validCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                                 .Select(c => c.Name)
                                                 .Where(n => !string.IsNullOrEmpty(n));
            // 检查目标区域性是否存在，validCultures是所有语言的string数组，注意是 zh-CN中间是横杠
            if (!validCultures.Contains("zh-CN"))
                throw new ArgumentException("无效区域性");

            FilloutCombLang(1);  //填充语言        
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string url, token;
            url = combSite.SelectedValue.ToString();
            string mobile = tbMobile.Text.Trim();
            string passwd = tbPasswd.Text.Trim();

            if (string.IsNullOrEmpty(combSite.SelectedValue.ToString().Trim()) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(passwd))
            {              
                MessageBox.Show("请输入正确的站点，用户名和密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!RegexHelper.IsValid11DigitNumber(mobile))
            {
                MessageBox.Show("请输入11数字手机号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                token = LoginOperation.GetToken(url, mobile, passwd);

                if (token != null && !string.IsNullOrEmpty(token))
                {
                    //给全局变量赋值
                    FrmFunctionPortal.URL = url;
                    FrmFunctionPortal.Token = token;
                    FrmFunctionPortal.Token_Get_Time = DateTime.Now;
                    var selectItem = combSite.Items[combSite.SelectedIndex];                    
                    FrmFunctionPortal.SiteName = ((KeyValuePair<string, string>)selectItem).Key;  //SelectedValue可以直接获取，而Key需要转换

                    //FrmMqttUploadLog frmMqttUploadLog = new FrmMqttUploadLog(profile, vehicles);  //简化操作，不再通过窗体构造函数传值
                    MessageBox.Show("恭喜登录成功，请点击功能按钮继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    //frmMqttUploadLog.ShowDialog();
                   
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("请保证正确的用户名和密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex) { }
        }       

           

        private void combLang_SelectedIndexChanged(object sender, EventArgs e)
        {           

            // 读取嵌入资源
            //foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            //{
            //    MessageBox.Show(name, "嵌入资源");
            //}

            //上面打印出的name，不用带 .resources，然后填入下面语句；  所有的资源文件会被编译成 Namespace.resources.dll，在不同语言的文件夹下
            ResourceManager rm = new ResourceManager("LiteToolSuite.FrmLogin", Assembly.GetExecutingAssembly());
            string value = rm.GetString("MessageForLang");  // 获取字符串
            //byte[] fileData = (byte[])rm.GetObject("BinaryKey");  // 获取二进制数据

            //MessageBox.Show(Properties.Resources.PopMessage);  //在Properties下面的各种语言的resx中，存系统的提示信息挺好
            //MessageBox.Show(value,"提示");

            // 询问是否重启
            DialogResult result = MessageBox.Show(
                text:value, caption: "",
                buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        /// <summary>
        /// 填充语言选中下拉框
        /// </summary>
        /// <param name="index"></param>
        private void FilloutCombLang(int index)
        {
            //先删除change事件，绑定数据后再添加change事件，否则会在绑定数据的时候就触发change事件，在事件中获取的value值出错
            combLang.SelectedIndexChanged -= combLang_SelectedIndexChanged;
            var LangDict = new Dictionary<string, string> { { "English", "en-US" }, { "简中", "zh-CN" }};
            combLang.DataSource = new BindingSource(LangDict, null);
            combLang.DisplayMember = "Key";
            combLang.ValueMember = "Value";

            combLang.SelectedIndex = index;
            combLang.SelectedIndexChanged += combLang_SelectedIndexChanged;
        }

        /// <summary>
        ///填充站点下拉框
        /// </summary>
        /// <param name="index"></param>
        private void FilloutCombSite(int index)
        {
            //var SiteDict = new Dictionary<string, string> { { "Dev", "dev" }, { "Dev天车", "dev-crane" }, { "Test", "test" }, { "Perf07", "perf07" }, { "Perf07天车", "perf07" }, { "Perf08", "perf08" }, { "Perf08天车", "perf08-crane" }, { "一汽富奥", "yqfa" } };
            var SiteDict = new Dictionary<string, string> ();

            //MessageBox.Show(SiteDict.Count.ToString(), "提示");

            try
            {
                string sqliteSQL = string.Format("SELECT site_name,site_url FROM server_site");
                var dataTable = Program.SQLiteHelper.ExecuteDataset(sqliteSQL,null).Tables[0];

                SiteDict = dataTable.AsEnumerable().ToDictionary(row => row["site_name"].ToString(), row => row["site_url"].ToString());

               
            }
            catch (Exception ex) { }

            if (SiteDict.Count > 0)
            {
                //按照首字母排序
                var sort = SiteDict.OrderBy(kv => StringHelper.GetFirstPinyin(kv.Key));
                SiteDict = sort.ToDictionary(kv => kv.Key, kv => kv.Value);

                combSite.DataSource = new BindingSource(SiteDict, null);
                combSite.SelectedIndex = index;
                combSite.DisplayMember = "Key";
                combSite.ValueMember = "Value";
            }        

        }

        private void btAddSite_Click(object sender, EventArgs e)
        {
            // 创建表等数据库操作，可以用其他工具操作，也可以参见 SQLiteHelper中的样例代码
            FrmServerSite frmServerSite = new FrmServerSite();
            frmServerSite.ShowDialog();

            FilloutCombSite(0); //update website list
        }

        private void btRemoveSite_Click(object sender, EventArgs e)
        {
            var selectItem = combSite.Items[combSite.SelectedIndex];
            var siteURL = combSite.SelectedValue.ToString();
            var siteName = ((KeyValuePair<string, string>)selectItem).Key;  //SelectedValue可以直接获取，而Key需要转换

            if (!string.IsNullOrEmpty(siteURL)) 
            {
                string sqliteSQL = string.Format("SELECT * FROM server_site");
                var dt = Program.SQLiteHelper.ExecuteDataTable(sqliteSQL);
                if (dt.Rows.Count <= 1)
                {                   
                    MessageBox.Show("至少需要保留一个站点，删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string text = "你真的要删除名为：【 "+siteName+"】站点，其URL地址为：【" + siteURL + "】吗？";
                DialogResult result = MessageBox.Show(
                   text: text, caption: "提醒",
                   buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        
                        sqliteSQL = string.Format("DELETE FROM server_site WHERE site_url='{0}'", siteURL);
                        var val = Program.SQLiteHelper.ExecuteNonQuery(sqliteSQL);
                                                
                        MessageBox.Show("【 " + siteName + "】站点删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { }
                }
            }

            FilloutCombSite(0); //update website list            
        }        

        private void combSite_SelectedIndexChanged(object sender, EventArgs e)
        {

            string siteUrl = combSite.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(siteUrl))
            {
                try
                {
                    string sqliteSQL = string.Format("SELECT user_name,passwd FROM server_site WHERE site_url='{0}'", siteUrl);
                    var dataTable = Program.SQLiteHelper.ExecuteDataset(sqliteSQL, null).Tables[0];

                    tbMobile.Text = dataTable.Rows[0][0].ToString();
                    tbPasswd.Text  = dataTable.Rows[0][1].ToString();
                }
                catch (Exception ex) { }
            }

        }
    }
}
