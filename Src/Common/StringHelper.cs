using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class StringHelper
    {
        protected BinaryReader reader;

        public BinaryReader newReader(Byte[] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            reader = new BinaryReader(memoryStream);
            return reader;
        }

        public Byte Parse()
        {
            return reader.ReadByte();
        }

             
        /// <summary>
        /// C#中将字符串转换为整数的方法：如果为空，返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public static int StringToInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            return int.TryParse(input, out var result) ? result : 0;
        }


        /// <summary>
        /// 获取汉字首字母
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        public static string GetFirstPinyin(string chinese)
        {
            var sb = new StringBuilder();
            foreach (char c in chinese)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    var cc = new ChineseChar(c);
                    sb.Append(cc.Pinyins[0][0]); // 取首字母
                }
                else sb.Append(c);
            }
            return sb.ToString();
        }



    }


}
