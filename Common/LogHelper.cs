/*++

Copyright © 2022  VIA Technologies, Inc. All Rights Reserved.

This PROPRIETARY SOFTWARE is the property of VIA Technologies, Inc. and may contain trade secrets and/or 
other confidential information of VIA Technologies, Inc. This file shall not be disclosed to any third party, 
in whole or in part, without prior written consent of VIA. 

THIS PROPRIETARY SOFTWARE AND ANY RELATED DOCUMENTATION ARE PROVIDED AS IS, WITH ALL FAULTS, AND WITHOUT 
WARRANTY OF ANY KIND EITHER EXPRESS OR IMPLIED, AND VIA TECHNOLOGIES, INC. DISCLAIMS ALL EXPRESS OR IMPLIED
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, QUIET ENJOYMENT OR NON-INFRINGEMENT. 
--*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Common
{
    public class LogHelper
    {
        /// <summary>
        /// Write operation log to database
        /// </summary>
        /// <param name="Operator"></param>
        /// <param name="DeviceId"></param>
        /// <param name="OpName"></param>
        /// <param name="Type"></param>
        /// <param name="OpContent"></param>
        public static void WriteLogToDB(string Operator,string DeviceId,string OpName,string Type,string OpContent)
        {
            // WebAPI calling log with calling time
            DateTime dtOpTime;
            //string sNow = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss:fff");  //utc 时间
            string sNow = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff");  //包括时区的时间，即服务器本地时间
            string DateTimeNow = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");  
            int iRemainMilliSecond = Convert.ToInt32(sNow.Substring(sNow.Length - 3, 3));  //get the millisecond
            DateTime.TryParse(sNow.Substring(0,sNow.Length-4), out dtOpTime); //it will fail if there is millisecond in sNow
            Int64 OpTime = TimeHelper.DateTimeToUnixTimestamp(dtOpTime) + iRemainMilliSecond;
            string sSQL = string.Format(@"INSERT INTO t_log(OpTime,Operator,DeviceId,OpName,Type,OpContent,LocalTimeFormat,DatetimeOpTime) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                        OpTime, Operator, DeviceId, OpName, Type, OpContent, sNow, DateTimeNow);
            try
            {
                //Startup.myDB.ExecuteDataSet(sSQL);
               /* DBServerProvider.SqlDapper.ExecuteScalar(sSQL, null);*/   //直接执行SQL Statement
            }
            catch(Exception ex)
            {
                
            }            
        }

        public static void WriteLogToFile(string logFile,string message)
        {
            try
            {
                FileStream fs = new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(message + "  " + DateTime.Now.ToString() + "\n");

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch(Exception ex) { }        

        }        
    }
}
