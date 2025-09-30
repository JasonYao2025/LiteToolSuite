using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{


    #region used to send command to upload log file
    public class MqttUploadLogData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DebugType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleName { get; set; }
    }

    public class MqttUploadLog
    {
        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MqttUploadLogData Data { get; set; }
    }
    #endregion

    public class BucketFile
    {
        public string CosKey { get; set; }

        /// <summary>
        /// byte
        /// </summary>
        public long Size{ get; set; }
        public string LastModified {  get; set; }
    }
}
