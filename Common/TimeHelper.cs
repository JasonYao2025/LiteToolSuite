using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class TimeHelper
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

        //从字节流中读取时间
        public DateTime ParseDateTime()
        {
            //Int32 year = ParseInt32();// IPAddress.NetworkToHostOrder(reader.ReadInt32());

            int year = reader.ReadInt32();
            Byte month = reader.ReadByte();
            Byte day = reader.ReadByte();
            Byte hour = reader.ReadByte();
            Byte minute = reader.ReadByte();
            Byte sec = reader.ReadByte();

            return new DateTime(year, month, day, hour, minute, sec);
        }

        /// <summary>
        /// get Timestamp   string格式有要求，必须是yyyy-MM-dd hh:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetTimestamp(string dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);  //hour要是8，因为要考虑时区，否则转换结果会差8个小时
            DateTime dateTime= Convert.ToDateTime(dt);          
            int timeStamp = Convert.ToInt32((dateTime - dateStart).TotalSeconds);
            return timeStamp;
        }

        /// <summary>
        /// get DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int timeStamp)
        {
            DateTime dateStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = (long)timeStamp * 10000000;
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dateTime = dateStart.Add(toNow);
            return dateTime;
        }

        /// <summary>
        /// convert UnixTimeStamp into DateTime "yyyy/MM/dd HH:mm:ss:fff"
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime MilliUnixTimeStampToDatetime(string unixTimeStamp)
        {

            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local); //get current timezone
            //long lTimestamp=new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            long time = long.Parse(unixTimeStamp + "0000");
            TimeSpan toNow = new TimeSpan(time);
            return startTime.Add(toNow);
        }

        // 时间戳转时间（秒级）
        public static DateTime UnixTimestampToDateTime(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp).ToLocalTime();
        }

        // 时间转时间戳（秒级）
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dateTime.ToUniversalTime() - origin;
            return (long)diff.TotalSeconds;
        }

        /// <summary>
        /// Get dateTime in UnixTimestamp，毫秒级
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Int64 DateTimeToMilliUnixTimestamp(DateTime dateTime)
        {
            DateTime dtStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0); //milliseconds
            TimeSpan ts = dateTime - dtStartTime;
            return (Int64)ts.TotalMilliseconds;
        }

      

        public static string DateTimeToString(DateTime dateTime)
        {
            string str = dateTime.ToString("yyyy/MM/dd");
            return str;
        }

        // 常用格式定义
        public const string ISO8601 = "yyyy-MM-ddTHH:mm:ssZ";
        public const string ChineseFormat = "yyyy年MM月dd日 HH时mm分ss秒";

        /// <summary>
        /// 字符串转DateTime（自动识别格式）
        /// </summary>
        public static DateTime ParseDateTime(string input)
        {
            string[] formats = {
            ISO8601,
            "yyyy-MM-dd HH:mm:ss",
            "MM/dd/yyyy HH:mm:ss",
            ChineseFormat,
            "yyyyMMddHHmmss"
        };

            if (DateTime.TryParseExact(input, formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var result))
            {
                return result;
            }

            throw new FormatException($"无法识别的日期格式: {input}");
        }

        /// <summary>
        /// 转换为当前时区时间
        /// </summary>
        public static DateTime ToLocalTime(string utcTimeString)
        {
            var utcTime = ParseDateTime(utcTimeString);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local);
        }

        /// <summary>
        /// 格式化日期输出
        /// </summary>
        public static string Format(DateTime dateTime, string format = ISO8601)
        {
            return dateTime.ToString(format);
        }

        /// <summary>
        /// 获取当前系统时间（带时区）
        /// </summary>
        public static string GetCurrentLocalTime(string format = ISO8601)
        {
            return Format(DateTime.Now, format);
        }

    }
}
