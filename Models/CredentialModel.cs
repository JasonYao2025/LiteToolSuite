using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{
    public class CredentialModel
    {
    }

    public class RequestCredential
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 服务请求凭证(credentials). 服务类型包含 "Mqtt", "MinIO" 或者 "Tencent-COS".
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 32位验证码, MD5(DeviceId + Timestamp/100|0). 所有字符小写。 Timestamp为请求发起时的时间戳,然后再除以100并取整，c#支持通过 '|0' 取整。
        /// </summary>
        public string Code { get; set; }
    }

    #region Minio, Tencent-COS etc. Storage server credential
    public class StorageCredentials
    {
        /// <summary>
        /// 
        /// </summary>
        public string AccessKeyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SecretAccessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SessionToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Expiration { get; set; }
    }

    public class StorageCredential
    {
        /// <summary>
        /// 
        /// </summary>
        public StorageCredentials Credentials { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExpiryTimestamp { get; set; }
    }

    #endregion


    #region Mqtt server credential
    public class MqttCredentials
    {
        /// <summary>
        /// 
        /// </summary>
        public string MqttUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MqttPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509CAfile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509CAfileMD5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509CertFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509CertFileMD5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509PrivateFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string X509PrivateFileMD5 { get; set; }
    }

    public class MessageCredential
    {
        /// <summary>
        /// 
        /// </summary>
        public MqttCredentials Credentials { get; set; }
    }

    #endregion
}
