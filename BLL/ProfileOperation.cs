using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.BLL
{
    public class ProfileOperation
    {
                   
        /// <summary>
        /// call /v1/device/profile to get mqtt server 
        /// </summary>
        public static bool GetMqttServer(string URL, string deviceId, string deviceModel, string token, out string MqttURL, out string MqttPort)
        {
            string statusCode = "";
            MqttURL = ""; MqttPort = "";

            string url = URL + "/v1/device/profile";

            if (!string.IsNullOrEmpty(token)&&!string.IsNullOrEmpty(URL)&&!string.IsNullOrEmpty(deviceId)&&!string.IsNullOrEmpty(deviceModel))
            {
                
                DeviceModel device = new DeviceModel();
                device.DeviceId = deviceId;

                if (deviceModel.Contains("C200"))
                {
                    device.AVersion = "v1r010";  //C200
                }
                else 
                {
                    device.AVersion = "2";
                }
                
                string payload = JsonConvert.SerializeObject(device);

                string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
                JObject jsonResponse = JObject.Parse(response);

                if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK")
                {

                    MqttURL = jsonResponse["Message"]["Endpoint"].ToString();
                    MqttPort = jsonResponse["Message"]["Port"].ToString();

                    return true;
                }
            }
            return false;
        }


    
        /// <summary>
        /// call /v1/device/profile to get profile including minio,mqtt etc. server configuration
        /// </summary>
     
        public static ProfileModel GetProfile(string URL, string deviceId, string deviceModel, string token)
        {
            string statusCode = "";
            ProfileModel profile = new ProfileModel();

            string url = URL + "/v1/device/profile";

            if (!string.IsNullOrEmpty(token))
            {
                DeviceModel device = new DeviceModel();
                device.DeviceId = deviceId;

                if (deviceModel.Contains("C200"))
                {
                    device.AVersion = "v1r010";  //C200
                }
                else
                {
                    device.AVersion = "2";
                }

                string payload = JsonConvert.SerializeObject(device);

                string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
                //JObject jsonResponse = JObject.Parse(response);

                profile = JsonConvert.DeserializeObject<ProfileModel>(response);

                if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK")
                {                 

                    return profile;
                }
            }
            return profile;
        }

        /// <summary>
        /// Call host/v1/device/credentials to get mqttuser and mqttpassword
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="deviceId"></param>
        /// <param name="token"></param>
        /// <param name="MqttUser"></param>
        /// <param name="MqttPassword"></param>
        /// <returns></returns>
        public static bool GetMqttCredential(string URL, string deviceId, string token, out string MqttUser, out string MqttPassword)
        {
            string statusCode = "";
            MqttUser = ""; MqttPassword = "";

            string url = URL + "/v1/device/credentials";

            if (!string.IsNullOrEmpty(token))
            {
                RequestCredential request = new RequestCredential();
                request.DeviceId = deviceId;
                request.ServiceType = "Mqtt";


                string timeStamp = (TimeHelper.DateTimeToUnixTimestamp(DateTime.Now) / 100 | 0).ToString();   //timeStamp要先除以100再取整
                request.Code = SecurityHelper.GetMD5Hash(deviceId + timeStamp);//32位，deviceid+timestamp


                string payload = JsonConvert.SerializeObject(request);

                string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
                JObject jsonResponse = JObject.Parse(response);

                if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK")
                {
                    MqttUser = jsonResponse["Credentials"]["MqttUser"].ToString();
                    MqttPassword = jsonResponse["Credentials"]["MqttPassword"].ToString();

                    return true;
                }
            }
            return false;
        }


        public static bool GetStorageCredential(string URL, string deviceId, string token, out string AccessKeyId, out string SecretAccessKey)
        {
            string statusCode = "";
            AccessKeyId = ""; SecretAccessKey = "";

            string url = URL + "/v1/device/credentials";

            if (!string.IsNullOrEmpty(token))
            {
                RequestCredential request = new RequestCredential();
                request.DeviceId = deviceId;
                request.ServiceType = "MinIO";


                string timeStamp = (TimeHelper.DateTimeToUnixTimestamp(DateTime.Now) / 100 | 0).ToString();   //timeStamp要先除以100再取整
                request.Code = SecurityHelper.GetMD5Hash(deviceId + timeStamp);//32位，deviceid+timestamp


                string payload = JsonConvert.SerializeObject(request);

                string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
                JObject jsonResponse = JObject.Parse(response);

                if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK")
                {
                    AccessKeyId = jsonResponse["Credentials"]["AccessKeyId"].ToString();
                    SecretAccessKey = jsonResponse["Credentials"]["SecretAccessKey"].ToString();

                    return true;
                }
            }
            return false;
        }


    }
}
