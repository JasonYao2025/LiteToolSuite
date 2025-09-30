using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;


namespace Common
{
    public class LanguageHelper
    {
        #region SetAllLang
        /// <summary>
        /// Set language
        /// </summary>
        /// <param name="lang">language:zh-CN, en-US</param>
        private static void SetAllLang(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Form frm = null;

            string name = "MainForm";

            frm = (Form)Assembly.Load("CameraTest").CreateInstance(name);

            if (frm != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();
                resources.ApplyResources(frm, "$this");
                AppLang(frm, resources);
            }
        }
        #endregion

        #region SetLang
        /// <summary>
        ///
        /// </summary>
        /// <param name="lang">language:zh-CN, en-US</param>
        /// <param name="form">the form you need to set</param>
        /// <param name="formType">the type of the form </param>
        public static void SetLang(string lang, Form form, Type formType)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            if (form != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(formType);
                resources.ApplyResources(form, "$this");
                AppLang(form, resources);
            }
        }
        #endregion

        #region AppLang for control
        /// <summary>
        ///  loop set the propery of the control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="resources"></param>
        private static void AppLang(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        AppLang(c, resources);
                    }
                }
            }

            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                AppLang(c, resources);
            }
        }
        #endregion

        #region AppLang for menuitem
        /// <summary>
        /// set the toolscript
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void AppLang(ToolStripMenuItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        AppLang(c, resources);
                    }
                }
            }
        }
        #endregion




        #region 以下的代码用于重启程序设置语言

        /// <summary>
        /// 支持的语言（区域）
        /// </summary>
        public static string[] SupportLanguages = new string[] { "zh-CN", "en-US" };

        /// <summary>
        /// 应用特定区域语言
        /// </summary>
        /// <param name="culture">区域标识</param>
        public static void ApplyLang(string culture)
        {
            CultureInfo ci = new CultureInfo(culture, false);
            CultureInfo.CurrentCulture = ci;
            CultureInfo.CurrentUICulture = ci;
        }

        /// <summary>
        /// 应用默认语言
        /// </summary>
        public static void ApplyDefaultLang()
        {
            if (!string.IsNullOrWhiteSpace(LiteToolSuite.Properties.Settings.Default.Language))
            {
                ApplyLang(LiteToolSuite.Properties.Settings.Default.Language);
            }
        }

        /// <summary>
        /// 保存默认语言的配置
        /// </summary>
        /// <param name="culture">区域（语言）</param>
        public static void SetDefaultLang(string culture)
        {
            LiteToolSuite.Properties.Settings.Default.Language = culture;
            LiteToolSuite.Properties.Settings.Default.Save();
        }
        #endregion
    }
}
