using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Common
{
    /// <sumary>
    /// MQT客户端帮助类 
    /// </sumary>
    public class MQTTClientHelper
    {
        private IMqttClient _client;
        /// <sumary> 
        /// 接收消息 
        /// </sumary> 
        //public MQTTReceivedMessageHandle ReceivedMessage;
        public bool IsConnected { get; set; } = false;
        public bool IsDisConnected { get; set; } = true;
        private string _serverIp; private int _serverPort;

        /// <sumary> 
        /// 订阅主题集合 
        /// </sumary> 
        private Dictionary<string, bool> _subscribeTopicList = null;
        #region 连接/断开服务端     
        /// <sumary> 
        /// 连接服务端 
        /// </sumary> 
        /// <param name="serverIp">服务端IP</param>
        /// <param name="serverPort">服务端口号</param> 
        public async void Start(string serverIp, int serverPort,string userName,string password)
        {
            this._serverIp = serverIp;
            this._serverPort = serverPort;
            
            if (!string.IsNullOrEmpty(serverIp) && !string.IsNullOrWhiteSpace(serverIp) && serverPort > 0)
            {
                try
                {
                    // 1. 构建配置参数
                    var options = new MqttClientOptionsBuilder()
                        //.WithClientId(Guid.NewGuid().ToString("N")) // 推荐使用唯一ID
                        .WithTcpServer(serverIp, serverPort)
                        .WithCredentials(userName, password) // 自动UTF-8编码
                        .WithCleanSession()
                        //.WithKeepAlivePeriod(TimeSpan.FromSeconds(10))
                        .WithTimeout(TimeSpan.FromSeconds(15)) // 新增连接超时
                        .Build();

                    // 2. 安全释放旧连接
                    if (_client != null)
                    {
                        await _client.DisconnectAsync(
                            new MqttClientDisconnectOptions
                            {
                                Reason = MqttClientDisconnectOptionsReason.NormalDisconnection
                            },
                            CancellationToken.None);
                        _client.Dispose();
                    }

                    // 3. 创建并配置客户端
                    _client = new MqttFactory().CreateMqttClient();

                    // 4. 安全注册事件（先移除后添加）
                    _client.ConnectedAsync -= Client_ConnectedAsync;
                    _client.ConnectedAsync += Client_ConnectedAsync;

                    _client.DisconnectedAsync -= Client_DisconnectedAsync;
                    _client.DisconnectedAsync += Client_DisconnectedAsync;

                    _client.ApplicationMessageReceivedAsync -= Client_ApplicationMessageReceivedAsync;
                    _client.ApplicationMessageReceivedAsync += Client_ApplicationMessageReceivedAsync;

                    // 5. 建立连接
                    var connectResult = await _client.ConnectAsync(
                        options,
                        CancellationToken.None);

                    if (connectResult.ResultCode != MqttClientConnectResultCode.Success)
                    {
                        throw new Exception($"连接失败: {connectResult.ResultCode}");
                    }
                }
                catch (Exception ex)
                {
                    // 建议使用结构化日志
                    //logger.LogError(ex, "MQTT连接异常");
                    throw; // 根据业务需求决定是否重新抛出
                }

            }
            else
            {
                // SLog.Loger.Warning("MQT服务端地址或端口号不能为空！");
            }
        }

        /// <sumary> 
        /// 断开MQT客户端 
        /// </sumary> 
        public void Client_Disconect()
        {
            if (_client != null)
            {
                _client.DisconnectAsync();
                _client.Dispose();
                Console.WriteLine($"关闭MQT客户端成功！");
            }
        }
        /// <sumary> 
        /// 客户端重新MQT服务端 
        /// </sumary> 
        public void Client_ConectAsync()
        {
            if (_client != null)
            {
                _client.ReconnectAsync();
                Console.WriteLine($"连接MQT服务端成功！");
            }
        }
        #endregion
        #region MQTT方法 
        /// <sumary> 
        /// 客户端与服务端建立连接 
        /// </sumary> 
        /// <param name="arg"></param> 
        private Task Client_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            return Task.Run(async() => 
        {
                IsConnected = true;
                IsDisConnected = false;
                Console.WriteLine($"连接到MQTT服务端成功.{arg.ConnectResult.AssignedClientIdentifier}");
                //订阅主题（可接收来自服务端消息，与客户端发布消息不能用同一个主题） 
                //try
                //{
                //    if (_subscribeTopicList != null & _subscribeTopicList.Count > 0)
                //    {
                //        List<string> subscribeTopics = _subscribeTopicList.Keys.ToList();
                //        foreach (var topic in subscribeTopics)
                //            SubscribeAsync(topic);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //SLog.Loger.Eror("MQT客户端与服务端[{0}:{1}]建立连接订阅主题错误：{2}", _serverIp, _serverPort, ex.Mesage); 
                //}
            });
        }

        /// <sumary> 
        /// 客户端与服务端断开连接 
        /// </sumary> / <param name="arg"></param> 
        private Task Client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            return Task.Run(new Action(async () =>
            {
                IsConnected = false;
                IsDisConnected = true;
                Console.WriteLine($"已断开到MQT服务端的连接.尝试重新连接");
                try
                {
                    await Task.Delay(30);
                    //MqttClientOptions options = new MqttClientOptions(); 
                    //await mqttClient.ConnectAsync(options); 
                    await _client.ReconnectAsync();
                }
                catch (Exception ex)
                {
                    //SLog.Loger.Eror("MQTT客户端与服务端[{0}:{1}]断开连接退订主题错误：{2}", _serverIp, _serverPort, ex.Mesage); 
                }
            }));
        }
        /// <sumary> 
        /// 客户端与服务端重新连接 
        /// </sumary> 
        /// <returns></returns> 
        public Task ReconnectedAsync()
        {
            try
            {
                if (_client != null)
                {
                    _client.ReconnectAsync();
                }
            }
            catch (Exception ex)
            {
                // SLog.Loger.Eror("MQTT客户端与服务端[{0}:{1}]重新连接退订主题错误：{2}", _serverIp, _serverPort, ex.Message); 
            }
            return Task.CompletedTask;
        }
        /// <sumary> 
        /// 客户端收到消息 
        /// </sumary> 
        /// <param name="arg"></param> 
        private Task Client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            MessageBox.Show(arg.ToString(),"Message");
            try
            {
                return Task.Run( async() => {
                    string msg = arg.ApplicationMessage.ConvertPayloadToString();
                    Console.WriteLine($"接收消息：{msg}\nQoS={arg.ApplicationMessage.QualityOfServiceLevel}\n客户端={arg.ClientId}\n主题：{arg.ApplicationMessage.Topic}");
                });
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT收到来自服务端[{0}]消息错误：{1}", arg != nul ? arg.ClientId : ", ex.Mesage); 
            }
            return Task.CompletedTask;
        }
        #endregion
        #region 订阅主题 
        /// <sumary> 
        /// 订阅主题 
        /// </sumary> 
        /// <param name="topic">主题</param> 
        public void SubscribeAsync(string topic)
        {
            try
            {
                if (_subscribeTopicList == null)
                {
                    _subscribeTopicList = new Dictionary<string, bool>();
                    return;
                }
                    

                if (_subscribeTopicList.ContainsKey(topic) || _subscribeTopicList[topic])
                {
                    //SLog.Loger.Warning("MQT客户端已经订阅主题[{0}]，不能重复订阅", topic);     
                    return;
                }
                //订阅主题 
                _client.SubscribeAsync(topic, MqttQualityOfServiceLevel.AtLeastOnce);
                //添加订阅缓存 
                bool isSubscribed = _client != null & _client.IsConnected ? true : false;
                if (!_subscribeTopicList.ContainsKey(topic))
                    _subscribeTopicList.Add(topic, isSubscribed);
                else
                    _subscribeTopicList[topic] = isSubscribed;
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT客户端订阅主题[{0}]错误：{1}", topic, ex.Mesage); 
            }
        }

        /// <sumary> 
        /// 订阅主题集合 
        /// </sumary> 
        /// <param name="topicList">主题集合</param> 
        public void SubscribeAsync(List<string> topicList)
        {
            try
            {
                if (topicList == null | topicList.Count == 0)
                    return;
                foreach (var topic in topicList)
                    SubscribeAsync(topic);
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT客户端订阅主题集合错误：{0}", ex.Mesage); 
            }
        }
        /// <sumary> 
        /// 退订主题 
        /// </sumary> 
        /// <param name="topic">主题</param> 
        /// <param name="isRemove">是否移除缓存</param> 
        public void UnsubscribeAsync(string topic, bool isRemove = true)
        {
            try
            {
                if (_subscribeTopicList == null | _subscribeTopicList.Count == 0)
                {
                    //SLog.Loger.Warning("MQT客户端退订主题[{0}]不存在", topic); 
                    return;
                }
                if (!_subscribeTopicList.ContainsKey(topic))
                {
                    //SLog.Loger.Warning("MQT客户端退订主题[{0}]不存在", topic); 
                    return;
                }
                //退订主题 
                _client.UnsubscribeAsync(topic);
                //修改订阅主题缓存状态 
                if (isRemove)
                    _subscribeTopicList.Remove(topic);
                else
                    _subscribeTopicList[topic] = false;
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT客户端退订主题[{0}]错误：{1}", topic, ex.Mesage); 
            }
        }
        /// <sumary> 
        /// 退订主题集合 
        /// </sumary> 
        /// <param name="topicList">主题集合</param> 
        /// <param name="isRemove">是否移除缓存</param> 
        public void UnsubscribeAsync(List<string> topicList, bool isRemove = true)
        {
            try
            {
                if (topicList == null | topicList.Count == 0)
                    return;
                foreach (var topic in topicList)
                    UnsubscribeAsync(topic, isRemove);
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT客户端退订主题集合错误：{0}", ex.Mesage); 
            }
        }
        /// <sumary> 
        /// 订阅主题是否存在 
        /// </sumary> 
        /// <param name="topic">主题</param> 
        public bool IsExistSubscribeAsync(string topic)
        {
            try
            {
                if (_subscribeTopicList == null || _subscribeTopicList.Count == 0)
                    return false;
                if (!_subscribeTopicList.ContainsKey(topic))
                    return false;
                return _subscribeTopicList[topic];
            }
            catch (Exception ex)
            {
                return false;
                //SLog.Loger.Eror("MQT客户端订阅主题[{0}]是否存在错误：{1}", topic, ex.Mesage); return false; 
            }
        }
        #endregion
        #region 发布消息 
        /// <sumary> 
        /// 发布消息 
        /// 与客户端接收消息不能用同一个主题 
        /// </sumary> 
        /// <param name="topic">主题</param> 
        /// <param name="mesage">消息</param> 
        public async void PublishMesage(string topic, string message)
        {
            try
            {
                if (_client != null)
                {
                    if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                    {
                        //SLog.Loger.Warning("MQT客户端不能发布为空的消息！"); 
                        return;
                    }
                    MqttClientPublishResult result = await _client.PublishStringAsync(topic, message, MqttQualityOfServiceLevel.AtLeastOnce);//恰好一次， QoS 级别1  
                    Console.WriteLine($"发布消息-主题：{topic}，消息：{message}，结果： {result.ReasonCode}");
                }
                else
                {
                    //SLog.Loger.Warning("MQT客户端未连接服务端，不能发布主题为[{0}]的消息：{1}", topic, mesage); 
                    return;
                }
            }
            catch (Exception ex)
            {
                //SLog.Loger.Eror("MQT客户端发布主题为[{0}]的消息：{1}，错误：{2}", topic, mesage, ex.Mesage); 
            }
        }
        #endregion
    }
}
