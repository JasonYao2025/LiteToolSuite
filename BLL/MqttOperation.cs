using Common;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using Newtonsoft.Json;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;

namespace LiteToolSuite.BLL
{
    public class MqttOperation
    {
        /// <summary>
        /// 根据选择组合出 Mqtt订阅消息
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static MqttClientSubscribeOptions GetMqttClientSubscribeOptions(int sum,string deviceId)
        {

            //新版写法,通过MqttTopicFilter来赋值，可以加With参数
            MqttClientSubscribeOptions subscribeOptions = null;

            switch (sum)
            {
                case 10:
                    subscribeOptions = new MqttClientSubscribeOptions
                    {
                        TopicFilters = new List<MqttTopicFilter>{ 
                            new MqttTopicFilterBuilder().WithTopic("via/liveData/"+deviceId).Build()
                        }
                    };
                    break;
                case 20:
                    subscribeOptions = new MqttClientSubscribeOptions
                    {
                        TopicFilters = new List<MqttTopicFilter> { 
                            new MqttTopicFilterBuilder().WithTopic("via/cmd/request/" + deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("via/cmd/response/" + deviceId).Build()
                        }
                    };
                    break;
                case 30:
                    subscribeOptions = new MqttClientSubscribeOptions
                    {
                        TopicFilters = new List<MqttTopicFilter> {
                            new MqttTopicFilterBuilder().WithTopic("via/liveData/"+deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("via/cmd/request/" + deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("via/cmd/response/" + deviceId).Build()
                        }
                    };
                    break;             
           
                default:
                    break;
            }
            return subscribeOptions;
        }

        /// <summary>
        /// 上传Debug Log的Payload
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="debugType"></param>
        /// <param name="moduleName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string GetLogPayload(string deviceId,string debugType,string moduleName,string action)
        {
            string payload = "";
            MqttUploadLog mqttUploadLog = new MqttUploadLog();
            MqttUploadLogData data = new MqttUploadLogData();
            data.Id = deviceId + "-" + TimeHelper.DateTimeToUnixTimestamp(DateTime.Now);
            data.DebugType = debugType;
            data.ModuleName = moduleName;  //sys，表示记录system log；如果修改成iot或者ota，表示记录iot或者ota的log

            mqttUploadLog.Data = data;
            mqttUploadLog.Action = "SystemDebug";
            payload = JsonConvert.SerializeObject(mqttUploadLog);

            return payload;
        }       
      
    }
}
