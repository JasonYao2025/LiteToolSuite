using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace LiteToolSuite.BLL
{
    public class InputOperation
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="useNumbers">是否包含数字</param>
        /// <param name="useLowercase">是否包含小写字母</param>
        /// <param name="useUppercase">是否包含大写字母</param>
        /// <param name="useSpecialChars">是否包含特殊字符</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomString(int length = 20,
            bool useNumbers = true,
            bool useLowercase = true,
            bool useUppercase = true,
            bool useSpecialChars = true,
            bool useOtherChars=true)
        {
            // 定义字符集
            const string numbers = "0123456789";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialChars = "!@#$%^&*()-_=+<>?/~'§±©®™°•○●□■♠♣♥♦★☆";
            const string otherChars = "你好世界编程あいうえおアイウエオカキク안녕하세요프로그램테ر برنامج نظام شبكة Κόσμε ΠρογПривет мирสวัสดีชาวโลกה מערכת רשת";


            // 构建可用字符池
            string charPool = "";

            if (useNumbers) charPool += numbers;
            if (useLowercase) charPool += lowercase;
            if (useUppercase) charPool += uppercase;
            if (useSpecialChars) charPool += specialChars;
            if (useOtherChars) charPool += otherChars;

            // 验证字符池是否为空
            if (string.IsNullOrEmpty(charPool))
            {
                throw new ArgumentException("至少需要选择一种字符类型");
            }

            // 生成随机字符串
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = charPool[random.Next(charPool.Length)];
            }

            return new string(result);
        }
    }
}
