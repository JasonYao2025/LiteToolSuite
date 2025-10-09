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
                            new MqttTopicFilterBuilder().WithTopic("/liveData/"+deviceId).Build()
                        }
                    };
                    break;
                case 20:
                    subscribeOptions = new MqttClientSubscribeOptions
                    {
                        TopicFilters = new List<MqttTopicFilter> { 
                            new MqttTopicFilterBuilder().WithTopic("/cmd/request/" + deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("/cmd/response/" + deviceId).Build()
                        }
                    };
                    break;
                case 30:
                    subscribeOptions = new MqttClientSubscribeOptions
                    {
                        TopicFilters = new List<MqttTopicFilter> {
                            new MqttTopicFilterBuilder().WithTopic("/liveData/"+deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("/cmd/request/" + deviceId).Build(),
                            new MqttTopicFilterBuilder().WithTopic("/cmd/response/" + deviceId).Build()
                        }
                    };
                    break;             
           
                default:
                    break;
            }
            return subscribeOptions;
        }        
      
    }
}
