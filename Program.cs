using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite
{
    internal static class Program
    {
        public static SQLiteHelper TestDb;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            #region 语言设置，要在Program里面设置语言，否则某些界面会出错
            try
            {
                TestDb = new SQLiteHelper(System.Environment.CurrentDirectory + @"\LiteToolSuite.db");     //初始化数据库

                string sqliteSQL = string.Format("SELECT language FROM system_setting");
                var dataTable = TestDb.ExecuteDataset(sqliteSQL, null).Tables[0];

                if (dataTable.Rows.Count > 0 && !string.IsNullOrEmpty(dataTable.Rows[0][0].ToString()))
                {
                    LanguageHelper.SetDefaultLang(dataTable.Rows[0][0].ToString());    //读取SQLite的语言设置并写入默认语言
                }
                else
                {
                    // 如果数据未存系统语言，则读取系统语言并写入
                    CultureInfo systemCulture = CultureInfo.InstalledUICulture;  //获取系统语言
                    LanguageHelper.SetDefaultLang(systemCulture.ToString());    //将系统语言写入Properties.Settings里面
                }

            }
            catch (Exception ex) { }

            //直接指定语言
            //LanguageHelper.SetDefaultLang("en-US");

            LanguageHelper.ApplyDefaultLang();  //调用Properties.Settings中的语言，并初始化区域信息（即配置语言），所有Form都会变          
            #endregion

            // 在Program.cs的Main方法中添加权限检测
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas" // 触发UAC提权
                };
                Process.Start(startInfo);
                Application.Exit();
            }
            else
            {
                //Application.Run(new FrmLogin());
                Application.Run(new FrmFunctionPortal());
                //Application.Run(new FrmMqttUploadLog());
            }
        }
    }
}
