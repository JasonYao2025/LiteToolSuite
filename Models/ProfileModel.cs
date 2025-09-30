using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{
    #region Profile Class
    public class WebAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CertAPI { get; set; }
    }

    public class OTA
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JobAPI { get; set; }
    }

    public class Message
    {
        /// <summary>
        /// 
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoutineTopic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandTopic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthMode { get; set; }
    }

    public class Storage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TripBucket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MediaBucket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MediaUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthMode { get; set; }
    }

    public class StreamingServer
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Port { get; set; }
    }

    public class ProfileModel
    {
        /// <summary>
        /// 
        /// </summary>
        public WebAPI WebAPI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OTA OTA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Message Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Storage Storage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public StreamingServer StreamingServer { get; set; }
    }

    #endregion


    /// <summary>
    /// Request Profile Class
    /// </summary>
    public class RequestProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// AVersion不同Device不一样： C200: "v1r010"   M350: "2"
        /// </summary>
        public string AVersion { get; set; }
    }
}
