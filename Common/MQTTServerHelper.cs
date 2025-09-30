using MQTTnet.Protocol;
using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MQTTServerHelper
    {
        private MqttServer _server;//MQTT服务器对象

        // 定义一个委托和事件(临时存储连接客户端数据)
        public event EventHandler<InterceptingPublishEventArgs> OnMessageReceived;
        public event EventHandler<bool> ServerStauts;
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientSubscribedTopicEventArgs> ClientSubscribedTopic;
        public event EventHandler<ClientUnsubscribedTopicEventArgs> ClientUnsubscribedTopic;

        /// <summary>
        /// 初始化MQTT服务并启动服务
        /// </summary>
        /// <param name="ip">IPV4地址</param>
        /// <param name="port">端口：0~65535之间</param>
        public Task StartMqtServer(string ip, int port)
        {
            MqttServerOptions mqttServerOptions = new MqttServerOptionsBuilder()
                   .WithDefaultEndpoint()
                   .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Parse(ip))
                   .WithDefaultEndpointPort(port)
                   .WithDefaultCommunicationTimeout(TimeSpan.FromMilliseconds(500)).Build();
            _server = new MqttFactory().CreateMqttServer(mqttServerOptions); // 创建MQT服务端对象
            _server.ValidatingConnectionAsync += Server_ValidatingConnectionAsync; // 验证用户名和密码
            _server.ClientConnectedAsync += Server_ClientConnectedAsync; // 绑定客户端连接事件
            _server.ClientDisconnectedAsync += Server_ClientDisconnectedAsync; // 绑定客户端断开事件
            _server.ClientSubscribedTopicAsync += Server_ClientSubscribedTopicAsync; // 绑定客户端订阅主题事件
            _server.ClientUnsubscribedTopicAsync += Server_ClientUnsubscribedTopicAsync; // 绑定客户端退订主题事件
            _server.InterceptingPublishAsync += Server_InterceptingPublishAsync; // 消息接收事件
            _server.ClientAcknowledgedPublishPacketAsync += Server_ClientAcknowledgedPublishPacketAsync; // 处理客户端确认发布的数据包
            _server.InterceptingClientEnqueueAsync += Server_InterceptingClientEnqueueAsync; // 订阅拦截客户端消息队列
            _server.ApplicationMessageNotConsumedAsync += Server_ApplicationMessageNotConsumedAsync; // 应用程序逻辑处理
            _server.StartedAsync += Server_StartedAsync;   // 绑定服务端启动事件
            _server.StoppedAsync += Server_StoppedAsync;    // 绑定服务端停止事件
            return _server.StartAsync();
        }

        /// <summary>
        /// 处理客户端确认发布事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ApplicationMessageNotConsumedAsync(ApplicationMessageNotConsumedEventArgs e)
        {
            try
            {
                Console.WriteLine($"【MesageNotConsumed】-SenderId:{e.SenderId}-Mesage:{e.ApplicationMessage.ConvertPayloadToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server_AplicationMesageNotConsumedAsync出现异常：{ex.Message}");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 订阅拦截客户端消息队列事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_InterceptingClientEnqueueAsync(InterceptingClientApplicationMessageEnqueueEventArgs e)
        {
            try
            {
                Console.WriteLine($"【InterceptingClientEnqueue】-SenderId:{e.SenderClientId}-Mesage:{e.ApplicationMessage.ConvertPayloadToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server_InterceptingClientEnqueueAsync出现异常：{ex.Message}");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当客户端处理完从MQT服务器接收到的应用消息后触发。
        /// 此事件可以用于确认消息已被处理，更新应用状态，
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ClientAcknowledgedPublishPacketAsync(ClientAcknowledgedPublishPacketEventArgs e)
        {
            try
            {
                Console.WriteLine($"【ClientAcknowledgedPublishPacket】-SenderId:{e.ClientId}-Mesage:{Encoding.UTF8.GetString(e.PublishPacket.PayloadSegment.ToArray())}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server_ClientAcknowledgedPublishPacketAsync出现异常：{ex.Message}");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 服务端消息接收
        /// </summary>
        /// <param name="e"></param>
        private Task Server_InterceptingPublishAsync(InterceptingPublishEventArgs e)
        {
            try
            {
                string client = e.ClientId; string topic = e.ApplicationMessage.Topic;
                string contents = e.ApplicationMessage.ConvertPayloadToString();
                //Encoding.UTF8.GetString(arg.AplicationMesage.PayloadSegment.ToAray();
                OnMessageReceived?.Invoke(this, e);
                Console.WriteLine($"接收到消息：Client：【{client}】 Topic：【{topic}】 Mesage：【{contents}】");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server_InterceptingPublishAsync出现异常：{ex.Message}");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 服务端断开事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_StoppedAsync(EventArgs arg)
        {
            return Task.Run(async () => 
            {
                ServerStauts?.Invoke(this, false);
                Console.WriteLine($"服务端【IP:Port】已停止MQT");
            });
        }

        /// <summary>
        /// 服务端启动事件
        /// </summary>
        /// <param name="e"></param>
        public Task Server_StartedAsync(EventArgs e)
        {
            return Task.Run(async () => 
        {
                ServerStauts?.Invoke(this, true);
                Console.WriteLine($"服务端【IP:Port】已启用MQT");
            });
        }

        /// <summary>
        /// 客户端退订主题事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs e)
        {
            return Task.Run(async () => 
        {
                ClientUnsubscribedTopic?.Invoke(this, e);
                Console.WriteLine($"客户端【{e.ClientId}】退订主题【{e.TopicFilter}】");
            });
        }

        /// <summary>
        /// 客户端订阅主题事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs e)
        {
            return Task.Run(async () =>  
        {
                ClientSubscribedTopic?.Invoke(this, e);
                Console.WriteLine($"客户端【{e.ClientId}】订阅主题【{e.TopicFilter.Topic}】");
            });
        }

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ClientDisconnectedAsync(ClientDisconnectedEventArgs e)
        {
            return Task.Run(async () => 
        {
                ClientDisconnected?.Invoke(this, e);
                Console.WriteLine($"客户端已断开.ClientId:【{e.ClientId}】,Endpoint:【{e.Endpoint}】.ReasonCode:【{e.ReasonCode}】,DisconectType:【{e.DisconnectType}】");
            });
        }

        /// <summary>
        /// 绑定客户端连接事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ClientConnectedAsync(ClientConnectedEventArgs e)
        {
            return Task.Run(async () => 
        {
                ClientConnected?.Invoke(this, e);
                Console.WriteLine($"客户端已连接.ClientId:【{e.ClientId}】,Endpoint:【{e.Endpoint}】");
            });
        }

        /// <summary>
        /// 验证客户端事件
        /// </summary>
        /// <param name="e"></param>
        private Task Server_ValidatingConnectionAsync(ValidatingConnectionEventArgs e)
        {
            return Task.Run(async () => 
        {
                if (e.Password == "")
                {
                    e.ReasonCode = MqttConnectReasonCode.Success;
                    Console.WriteLine($"客户端已验证成功.ClientId:【{e.ClientId}】,Endpoint:【{e.Endpoint}】");
                }
                else
                {
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    Console.WriteLine($"客户端验证失败.ClientId:【{e.ClientId}】,Endpoint:【{e.Endpoint}】");
                }
            });
        }
    }

}
