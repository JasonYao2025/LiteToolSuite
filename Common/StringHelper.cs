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

        public UInt32 ParseUInt32()
        {
            Byte[] vBytes = reader.ReadBytes(4);

            Array.Reverse(vBytes);

            return BitConverter.ToUInt32(vBytes, 0);
        }

        public Int32 ParseInt32()
        {
            int flow = reader.ReadInt32();
            return IPAddress.NetworkToHostOrder(flow);
        }
        public Int16 ParseInt16()
        {
            Int16 test = reader.ReadInt16();

            //byte[] bytes = BitConverter.GetBytes(test);

            //string str = Convert.ToString(test, 2);

            Int16 test2 = IPAddress.NetworkToHostOrder(test);

            //bytes = BitConverter.GetBytes(test2);

            //str = Convert.ToString(test2, 2);

            return test2;
        }


        public Int32 ParseIntEx()
        {
            int flow = reader.ReadInt32();
            return flow;
        }

        public string ParseString(int len)
        {
            Byte[] bytes = reader.ReadBytes(len);

            return ParseString(bytes, len);
        }

        public static string ParseString(byte[] bytes, int len)
        {
            if (bytes[0] != '\0')
            {
                string str = System.Text.Encoding.Default.GetString(bytes);

                int index = str.IndexOf('\0');

                if (index > 0)
                    return str.Substring(0, index);
                else
                    return str;

            }
            return "";
        }

        /// <summary>
        /// bytes转换为string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes, int len)
        {
            return ToHexString(bytes, 0, len);
        }
        public static string ToHexString(byte[] bytes, int start, int len)
        {
            string strReturn = "";
            for (int i = start; i < (start + len); i++)
            {
                byte bt = bytes[i];
                strReturn += bt.ToString("x2");
            }
            return strReturn;
        }

        //16进制string转换为Byte
        public static byte[] ToByteByHex(string hexStr)
        {
            int len = hexStr.Length;

            byte[] data = new byte[len / 2];

            for (int k = 0; k < data.Length; k++)
            {
                data[k] = Convert.ToByte(hexStr.Substring(k * 2, 2), 16);
                //k = k* 2;
            }

            return data;
        }

        //字符串变成16进制字符串
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string变成数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字按照ASCII码值为16进制字符，以空格隔开
            {
                result += Convert.ToString(b[i], 16);//" " + 本来中间有空格
            }
            return result;
        }

        // 16进制字符串，字节与字节之间以空格分开的情况下转换成ASCII码的string
        public static string HexStringToString(string hs, Encoding encode)
        {
            //以空格分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        ////16进制字符串转换成ASCII字符串，与上一个类似，只是这里16进制字符串没有空格分隔
        public static string HexStringToASCIIString(string hs, Encoding encode)
        {
            byte[] buff = new byte[hs.Length / 2];
            int index = 0;
            for (int i = 0; i < hs.Length; i += 2)
            {
                buff[index] = Convert.ToByte(hs.Substring(i, 2), 16);
                ++index;
            }
            string result = encode.GetString(buff);
            return result;
        }

        /// <summary>
        ///16进制字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <param name="fenge">是否每字符用逗号分隔</param>
        /// <returns></returns>
        public static string ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (fenge && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToLower();
        }

        ///<summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }

        public static string ProtocalToString()
        {
            string temp = string.Empty;
            return temp;
        }

        //16进制字符串转换为浮点型数，需要4个字节的16进制数
        public static float hexToFloat(string hexString)
        {
            if (hexString == "")
            {
                return 0;
            }
            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            return f;
        }


        /// <summary>
        /// 转换数字
        /// </summary>
        protected static long CharToNumber(char c)
        {
            switch (c)
            {
                case '一': return 1;
                case '二': return 2;
                case '三': return 3;
                case '四': return 4;
                case '五': return 5;
                case '六': return 6;
                case '七': return 7;
                case '八': return 8;
                case '九': return 9;
                case '零': return 0;
                default: return -1;
            }
        }


        /// <summary>
        /// 转换单位
        /// </summary>
        protected static long CharToUnit(char c)
        {
            switch (c)
            {
                case '十': return 10;
                case '百': return 100;
                case '千': return 1000;
                case '万': return 10000;
                case '亿': return 100000000;
                default: return 1;
            }
        }


        /// <summary>
        /// 将中文数字转换阿拉伯数字
        /// </summary>
        /// <param name="cnum">汉字数字</param>
        /// <returns>长整型阿拉伯数字</returns>
        public static long ParseCnToInt(string cnum)
        {
            cnum = Regex.Replace(cnum, "\\s+", "");
            long firstUnit = 1;//一级单位
            long secondUnit = 1;//二级单位
            long result = 0;//结果
            for (var i = cnum.Length - 1; i > -1; --i)//从低到高位依次处理
            {
                var tmpUnit = CharToUnit(cnum[i]);//临时单位变量
                if (tmpUnit > firstUnit)//判断此位是数字还是单位
                {
                    firstUnit = tmpUnit;//是的话就赋值,以备下次循环使用
                    secondUnit = 1;
                    if (i == 0)//处理如果是"十","十一"这样的开头的
                    {
                        result += firstUnit * secondUnit;
                    }
                    continue;//结束本次循环
                }
                if (tmpUnit > secondUnit)
                {
                    secondUnit = tmpUnit;
                    continue;
                }
                result += firstUnit * secondUnit * CharToNumber(cnum[i]);//如果是数字,则和单位想乘然后存到结果里
            }
            return result;
        }

        /// <summary>
        /// 阿拉伯数字转换成中文数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public string NumToChinese(string x)
        {
            string[] pArrayNum = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            //为数字位数建立一个位数组
            string[] pArrayDigit = { "", "十", "百", "千" };
            //为数字单位建立一个单位数组
            string[] pArrayUnits = { "", "万", "亿", "万亿" };
            var pStrReturnValue = ""; //返回值
            var finger = 0; //字符位置指针
            var pIntM = x.Length % 4; //取模
            int pIntK;
            if (pIntM > 0)
                pIntK = x.Length / 4 + 1;
            else
                pIntK = x.Length / 4;
            //外层循环,四位一组,每组最后加上单位: ",万亿,",",亿,",",万,"
            for (var i = pIntK; i > 0; i--)
            {
                var pIntL = 4;
                if (i == pIntK && pIntM != 0)
                    pIntL = pIntM;
                //得到一组四位数
                var four = x.Substring(finger, pIntL);
                var P_int_l = four.Length;
                //内层循环在该组中的每一位数上循环
                for (int j = 0; j < P_int_l; j++)
                {
                    //处理组中的每一位数加上所在的位
                    int n = Convert.ToInt32(four.Substring(j, 1));
                    if (n == 0)
                    {
                        if (j < P_int_l - 1 && Convert.ToInt32(four.Substring(j + 1, 1)) > 0 && !pStrReturnValue.EndsWith(pArrayNum[n]))
                            pStrReturnValue += pArrayNum[n];
                    }
                    else
                    {
                        if (!(n == 1 && (pStrReturnValue.EndsWith(pArrayNum[0]) | pStrReturnValue.Length == 0) && j == P_int_l - 2))
                            pStrReturnValue += pArrayNum[n];
                        pStrReturnValue += pArrayDigit[P_int_l - j - 1];
                    }
                }
                finger += pIntL;
                //每组最后加上一个单位:",万,",",亿," 等
                if (i < pIntK) //如果不是最高位的一组
                {
                    if (Convert.ToInt32(four) != 0)
                        //如果所有4位不全是0则加上单位",万,",",亿,"等
                        pStrReturnValue += pArrayUnits[i - 1];
                }
                else
                {
                    //处理最高位的一组,最后必须加上单位
                    pStrReturnValue += pArrayUnits[i - 1];
                }
            }
            return pStrReturnValue;
        }

        public static List<string> GetChCHAR(string str)
        {
            List<string> lChChar = new List<string>();

            string chRegS = @"[\u4e00-\u9fa5]+";
            Regex chRegR = new Regex(chRegS);
            Match chMatch = chRegR.Match(str);
            while (chMatch.Success)
            {
                lChChar.Add(chMatch.ToString());
                chMatch = chMatch.NextMatch();
            }

            return lChChar;
        }

        /// <summary>
        /// 获取字符串中的数字，包括小数和整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<double> GetNumberFromString(string str)
        {

            //string intNumber = Regex.Replace("K50+123", @"[^0-9]+", "");  提取小数
            // string doubleNumber = Regex.Replace("K50+123.01", @"[^0-9,.]+", "");  提取整数

            List<double> lNumber = new List<double>();

            string numRegS = @"\d+(\.\d+)?";  //提取字符串中的小数和整数
            Regex numRegR = new Regex(numRegS);
            Match numMatch = numRegR.Match(str);
            while (numMatch.Success)
            {
                //numMatch.ToString();
                lNumber.Add(Convert.ToDouble(numMatch.ToString()));
                numMatch = numMatch.NextMatch();
            }
            return lNumber;
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
