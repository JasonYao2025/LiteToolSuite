using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;


namespace Common
{
    public class LanguageHelper
    {
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
