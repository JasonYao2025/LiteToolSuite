using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MqttClientService
    {
        /// <summary>
        /// 供参考用，在需要调用的地方直接导入所有的代码，否则窗口之间的调用可能会有问题
        /// </summary>
        public static IMqttClient _mqttClient;
        //public string sIP = "52.80.120.120", sUser = "admin", sPassword = "123456", sClientId = "321369C6B59B21353C987F6B71311312";
        public string URL = "dev-mqtt.api.workxconnect.cn", user = "device", password = "DeV)#0%", clientId = "2CC6822D8490"; //clentId为deviceId
        public int port = 1883;

        public void MqttClientStart(string URL,int port,string user,string password)
        {
            var optionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(URL, port)          // 要访问的mqtt服务端的 ip 和 端口号
                .WithCredentials(user, password) // 要访问的mqtt服务端的用户名和密码
                                                 //.WithClientId(clientId)          // 设置客户端id
                .WithCleanSession()
                //.WithTls(new MqttClientOptionsBuilderTlsParameters
                //{
                //    UseTls = false  // 是否使用 tls加密
                //})
                .WithTimeout(TimeSpan.FromSeconds(15));

            var clientOptions = optionsBuilder.Build();
            _mqttClient = new MqttFactory().CreateMqttClient();

            _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync; // 客户端连接成功事件
            _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync; // 客户端连接关闭事件
            _mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync; // 收到消息事件

            _mqttClient.ConnectAsync(clientOptions);
        }

        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已断开与服务端的连接……");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已连接服务端……");

            // 订阅消息主题
            // MqttQualityOfServiceLevel: （QoS）:  0 最多一次，接收者不确认收到消息，并且消息不被发送者存储和重新发送提供与底层 TCP 协议相同的保证。
            // 1: 保证一条消息至少有一次会传递给接收方。发送方存储消息，直到它从接收方收到确认收到消息的数据包。一条消息可以多次发送或传递。
            // 2: 保证每条消息仅由预期的收件人接收一次。级别2是最安全和最慢的服务质量级别，保证由发送方和接收方之间的至少两个请求/响应（四次握手）。
            _mqttClient.SubscribeAsync("via/liveData/" + clientId, MqttQualityOfServiceLevel.AtLeastOnce);
            //_mqttClient.SubscribeAsync("#", MqttQualityOfServiceLevel.AtLeastOnce);  //相当于 -t "#"  接收全部
            return Task.CompletedTask;
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine($"ApplicationMessageReceivedAsync：客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            
            return Task.CompletedTask;
        }

        public void Publish(string topic, string payload)
        {
            //    //topic should be "via/cmd/request/[DeviceId]", then device with [DeviceId] can receive the message from Server. 
            //    //payload就是具体的内容，比如上传log： {"Action":"SystemDebug","Data":{"Id":"qvl-1701142032-1701149234","DebugType":"UploadLog","ModuleName":"sys"}}
            
            #region 老的写法
            //var message = new MqttApplicationMessage
            //{
            //    Topic = topic,
            //    Payload = Encoding.Default.GetBytes(payload),
            //    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
            //    //Retain = true  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
            //    Retain = isRetain  //如果代码有bug，Mqtt重连，如设置成true，每次重连就要下发一次；所以保险起见设置成false
            //};
            #endregion

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload) // 字符串直接写入
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
                .Build();

            _mqttClient.PublishAsync(message);
        }
    }
}