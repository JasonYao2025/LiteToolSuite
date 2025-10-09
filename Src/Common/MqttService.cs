using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;


namespace Common
{
    public class MqttService
    {
        public static MqttServer mqttServer { get; set; }

        public static void PublishData(string sTopic,string jsonData)
        { 
            //server publish message to device
            //var message = new MqttApplicationMessage
            //{
            //    //topic should be "via/cmd/request/[DeviceId]", then device with [DeviceId] can receive the message from Server.  
            //    //Topic = "topic_01",  
            //    Topic                 = sTopic,     
            //    Payload               = Encoding.Default.GetBytes(jsonData),
            //    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
            //    Retain = true,     //the message will be kept in server side, if true.

            //};
            

            //mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(message) //send message to client which subscribe topic_01
            //{
            //    SenderClientId = "Server_01"
            //}).GetAwaiter().GetResult();


            //MQTTnet 提供了一个静态类 MqttNetTrace 来对消息进行跟踪
            //MqttNetTrace 的事件 TraceMessagePublished 用于跟踪服务端和客户端应用的日志消息，比如启动、停止、心跳、消息订阅和发布等
            MqttNetTrace.TraceMessagePublished += MqttNetTrace_TraceMessagePublished;
            //启动服务端
            new Thread(StartMqttServer).Start();
            while (true)
            {
                //获取输入字符
                var inputString = Console.ReadLine().ToLower().Trim();
                //exit则停止服务
                if (inputString == "exit")
                {
                    mqttServer.StopAsync();
                    Console.WriteLine("MQTT服务已停止！");
                    break;
                }
                //clients则输出所有客户端
                else if (inputString == "clients")
                {
                    foreach (var item in mqttServer.GetConnectedClients())
                    {
                        Console.WriteLine($"客户端标识：{item.ClientId}，协议版本：{item.ProtocolVersion}");
                    }
                }
                else
                {
                    Console.WriteLine($"命令[{inputString}]无效！");
                }

            }

        }


        //启动服务端
        private static void StartMqttServer()
        {

            if (mqttServer == null)
            {
                try
                {
                    //在 MqttServerOptions 选项中，你可以使用 ConnectionValidator 来对客户端连接进行验证。
                    //比如客户端ID标识 ClientId，用户名 Username 和密码 Password 等。
                    var options = new MqttServerOptions
                    {
                        ConnectionValidator = p =>
                        {
                            if (p.ClientId == "c001")
                            {
                                if (p.Username != "u001" || p.Password != "p001")
                                {
                                    return MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                                }
                            }
                            return MqttConnectReturnCode.ConnectionAccepted;
                        }
                    };
                    //创建服务端最简单的方式是采用 MqttServerFactory 对象的 CreateMqttServer 方法来实现，该方法需要一个
                    //MqttServerOptions 参数。
                    mqttServer = new MqttServerFactory().CreateMqttServer(options) as MqttServer;
                    //服务端支持 ClientConnected、ClientDisconnected 和 ApplicationMessageReceived 事件，
                    //分别用来检查客户端连接、客户端断开以及接收客户端发来的消息。
                    //ApplicationMessageReceived 的事件参数包含了客户端ID标识 ClientId 和 MQTT 应用消息 MqttApplicationMessage 对象，
                    //通过该对象可以获取主题 Topic、QoS QualityOfServiceLevel 和消息内容 Payload 等信息
                    mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                    //ClientConnected 和 ClientDisconnected 事件的事件参数一个客户端连接对象 ConnectedMqttClient
                    //通过该对象可以获取客户端ID标识 ClientId 和 MQTT 版本 ProtocolVersion。
                    mqttServer.ClientConnected += MqttServer_ClientConnected;
                    mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); return;
                }
            }
            //创建了一个 IMqttServer 对象后，调用其 StartAsync 方法即可启动 MQTT 服务
            mqttServer.StartAsync();
            Console.WriteLine("MQTT服务启动成功！");
        }

        //ClientConnected 和 ClientDisconnected 事件的事件参数一个客户端连接对象 ConnectedMqttClient
        //通过该对象可以获取客户端ID标识 ClientId 和 MQTT 版本 ProtocolVersion。
        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.Client.ClientId}]已连接，协议版本：{e.Client.ProtocolVersion}");
        }

        //ClientConnected 和 ClientDisconnected 事件的事件参数一个客户端连接对象 ConnectedMqttClient
        //通过该对象可以获取客户端ID标识 ClientId 和 MQTT 版本 ProtocolVersion。
        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.Client.ClientId}]已断开连接！");
        }

        //ApplicationMessageReceived 的事件参数包含了客户端ID标识 ClientId 和 MQTT 应用消息 MqttApplicationMessage 对象，
        //通过该对象可以获取主题 Topic、QoS QualityOfServiceLevel 和消息内容 Payload 等信息
        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 负荷：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
        }

        //事件参数 MqttNetTraceMessagePublishedEventArgs 包含了线程ID ThreadId、来源 Source、日志级别 Level、日志消息 Message、异常信息 Exception 等。
        //MqttNetTrace 类还提供了4个不同消息等级的静态方法，Verbose、Information、Warning 和 Error，
        //用于给出不同级别的日志消息，该消息将会在 TraceMessagePublished 事件中输出，
        //你可以使用 e.Level 进行过虑。
        private static void MqttNetTrace_TraceMessagePublished(object sender, MqttNetTraceMessagePublishedEventArgs e)
        {
            Console.WriteLine($">> 线程ID：{e.ThreadId} 来源：{e.Source} 跟踪级别：{e.Level} 消息: {e.Message}"); if (e.Exception != null) { Console.WriteLine(e.Exception); }
        }
    }
}
