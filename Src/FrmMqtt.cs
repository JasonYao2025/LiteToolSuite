using Common;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.BLL;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;


namespace LiteToolSuite
{
    public partial class FrmMqtt : Form
    {
        //MQTT客户端
        public static IMqttClient _mqttClient;
        public string TOPIC;
        public Dictionary<string, int> TopicValueDict;

        public bool IsConnected { get; set; } = false;
        public bool IsDisConnected { get; set; } = true;            

        private ProfileModel Profile;       
        
        public FrmMqtt()
        {
            InitializeComponent();

            this.btnConnectMqttServer.Enabled = true;
            this.btnDisconnect.Enabled = true;
           

            Dictionary<string, string> vehicles = new Dictionary<string, string>();
            //if (VehicleOperation.GetVehicleList(FrmFunctionPortal.URL, FrmFunctionPortal.Token, out vehicles))
            if(VehicleOperation.GetVehicleListFromSQLite(Program.SQLiteHelper,out vehicles))
            {
                if (vehicles.Count > 0)
                {
                    //先删除change事件，绑定数据后再添加change事件，否则会在绑定数据的时候就触发change事件，数据显示出错
                    combVehicleList.SelectedIndexChanged -= combVehicleList_SelectedIndexChanged;

                    combVehicleList.DataSource = new BindingSource(vehicles, null);
                    combVehicleList.DisplayMember = "Key";
                    combVehicleList.ValueMember = "Value";

                    combVehicleList.SelectedIndex = 0;
                    string[] device = combVehicleList.SelectedValue.ToString().Split(',');
                    tbDeviceId.Text = device[0];
                    tbDeviceModel.Text = device[1];

                    combVehicleList.SelectedIndexChanged += combVehicleList_SelectedIndexChanged;
                }
                else 
                {
                    MessageBox.Show("未找到车辆","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return; 
                }
            }
           


            //fill out checklistbox
            TopicValueDict = new Dictionary<string, int> { { "LiveData", 10 }, { "Request and Response", 20 } };
            clbTopic.Items.AddRange(TopicValueDict.Keys.ToArray());
            clbTopic.SetItemChecked(0, true);  //default "LiveData" is checked
        }


        //点击 连接服务器  按钮
        private void btnConnectMqttServer_Click(object sender, EventArgs e)
        {
            //string mqttURL = "", mqttPort = "", aVersion = "";
            string deviceId = tbDeviceId.Text.Trim();
            string deviceModel = tbDeviceModel.Text.Trim();


            //上面打印出的name，不用带 .resources，然后填入下面语句；  所有的资源文件会被编译成 Namespace.resources.dll，在不同语言的文件夹下
            ResourceManager rm = new ResourceManager("LiteToolSuite.FrmMqtt", Assembly.GetExecutingAssembly());
            string text = rm.GetString("MessageBoxConnectMqttContent");  // 获取字符串
            string caption = rm.GetString("MessageBoxConnectMqttCaption");

            if (string.IsNullOrEmpty(text)) { text = "请选择已绑定主机的车辆！"; }
            if (string.IsNullOrEmpty(caption)) { caption = "提醒"; }

            if (string.IsNullOrEmpty(deviceId) || deviceId == "没有绑定AI主机")
            {
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);                
                return;
            }

            if(string.IsNullOrEmpty(FrmFunctionPortal.Token)) { return; }
            Profile = ProfileOperation.GetProfile(FrmFunctionPortal.URL, deviceId, deviceModel, FrmFunctionPortal.Token);
            if (Profile != null)
            {
                int serverPort = StringHelper.StringToInt(Profile.Message.Port);
                               
                string MqttUser = "", MqttPassword = "";
                if (ProfileOperation.GetMqttCredential(FrmFunctionPortal.URL, deviceId, FrmFunctionPortal.Token, out MqttUser, out MqttPassword))
                {
                    this.MqttClientStart(Profile.Message.Endpoint, serverPort, MqttUser, MqttPassword);

                    this.btnConnectMqttServer.Enabled = false;
                    this.btnDisconnect.Enabled = true;
                }
                else
                {
                    MessageBox.Show("获取Mqtt服务器用户名和密码失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }               
            }
            else
            {
                MessageBox.Show("Mqtt的服务器地址，端口不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 连接Mqtt服务器方法
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="iPort"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        public void MqttClientStart(string sIP, int iPort, string sUser, string sPassword)
        {
            var optionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(sIP, iPort)          // 要访问的mqtt服务端的 ip 和 端口号
                .WithCredentials(sUser, sPassword) // 要访问的mqtt服务端的用户名和密码
                                                   //.WithClientId(sClientId)          // 设置客户端id
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
            //Console.WriteLine($"客户端已断开与服务端的连接……");            
            MessageBox.Show("客户端已断开与服务端的连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            IsConnected = false;
            IsDisConnected = true;

            this.btnConnectMqttServer.Enabled = true;
            this.btnDisconnect.Enabled = false;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            IsConnected = true;
            IsDisConnected = false;

            //Console.WriteLine($"客户端已连接服务端……");    
            MessageBox.Show("客户端已建立与服务端的连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            //Console.WriteLine($"ApplicationMessageReceivedAsync：客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            string text = $"Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 " + DateTime.Now.ToString() + "\r\n";
            UpdateRichTextBox(text);
            
            return Task.CompletedTask;
        }

        //发布命令
        public void PublishMessage(string topic, string payload, bool isRetain, string debugType)
        {
            if (IsConnected == false)
            {             
                MessageBox.Show("请先连接Mqtt服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string publishTime = DateTime.Now.ToString("HH:mm:ss");

            UpdateRichTextBox("【" + publishTime + " 发布 <" + debugType + "> 指令】： 【Topic】: " + topic + " 【payload】： " + payload + "\r\n");

            var message = new MqttApplicationMessageBuilder()
                 .WithTopic(topic)
                 .WithPayload(payload) // 字符串直接写入
                 .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                 .WithRetainFlag(false)  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
                 .Build();

            _mqttClient.PublishAsync(message);
        }

        // 线程安全更新RichTextBox内容
        private void UpdateRichTextBox(string text)
        {
            if (rtbSubContent.InvokeRequired)
            {
                rtbSubContent.BeginInvoke(new Action(() =>
                {
                    rtbSubContent.AppendText(text + Environment.NewLine);
                }));
            }
            else
            {
                rtbSubContent.AppendText(text);
            }
        }

        //调用事件完成Disconnect
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _mqttClient.DisconnectAsync();
            }
            catch (Exception ex) { return; }

            this.btnConnectMqttServer.Enabled = true;
            this.btnDisconnect.Enabled = false;
        }
   
        
        private void combVehicleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] device = combVehicleList.SelectedValue.ToString().Split(',');

            tbDeviceId.Text = device[0];
            tbDeviceModel.Text = device[1];
        }

        /// <summary>
        /// 发送一条空的mqtt消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClearCmd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbDeviceId.Text.Trim()) || IsConnected == false)
            {
                MessageBox.Show("请先连接Mqtt服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string topic = string.Format("via/cmd/request/" + tbDeviceId.Text.Trim());

            string payload = "";
            this.PublishMessage(topic, payload, true, "清理命令");

            MessageBox.Show("下发成功，需重启AI主机以后才能生效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVehicleQRCode_Click(object sender, EventArgs e)
        {
            string qrCodeContent;
            string device = combVehicleList.SelectedValue.ToString();
            string deviceId = device.Split(',')[0];
            string deviceModel = device.Split(',')[1];

            if (string.IsNullOrEmpty(deviceId)) 
            {
                MessageBox.Show("请选择已绑定主机的车辆！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }

            if (deviceModel.Contains("C200")) { qrCodeContent = "C200:" + deviceId; }
            else if (deviceModel.Contains("M350")) 
            { 
                qrCodeContent = "{}"; 
            }
            else
            {               
                MessageBox.Show("主机没有型号的车辆不能生成车辆二维码，请确认", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
        }

        private void btnCopyDeviceId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDeviceId.Text.Trim()))
            {
                Clipboard.SetText(tbDeviceId.Text.Trim());
            }
        }

        private void btPub_Click(object sender, EventArgs e)
        {
            string topic = combPubTopic.SelectedValue.ToString();
            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("发布主题不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string payload ="";  //根据topic返回payload

          
            //通过客户端对象调用其 PublishAsync 异步方法进行消息发布。
            this.PublishMessage(topic, payload, false,"");
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            TOPIC = "";
            int sum = clbTopic.CheckedItems.Cast<string>().Sum(text => TopicValueDict[text]);

            if (sum < 0)
            {             
                MessageBox.Show("请至少选择一个订阅主题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!IsConnected)
            {
                MessageBox.Show("MQTT客户端尚未连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var subscribeOptions = MqttOperation.GetMqttClientSubscribeOptions(sum, tbDeviceId.Text.Trim());

           

            _mqttClient.SubscribeAsync(subscribeOptions);


            foreach (var topicFilter in subscribeOptions.TopicFilters) 
            {
                TOPIC += topicFilter.Topic + "    ";   //完整的取值：subscribeOptions.TopicFilters[0].Topic;
            }
            
            UpdateRichTextBox($"已订阅【{TOPIC}】主题\r\n");

        }

        private void btCancelSub_Click(object sender, EventArgs e)
        {
            //string topic = combSubTopic.SelectedValue.ToString();
            //mqttClient.UnsubscribeAsync(new List<String> {
            //          topic
            //});          

            try
            {
                _mqttClient.UnsubscribeAsync(TOPIC);
            }
            catch (Exception ex) { return; }

            MessageBox.Show($"已取消订阅【{TOPIC}】", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateRichTextBox($"已取消订阅【{TOPIC}】\r\n");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            rtbSubContent.Text = "";
        }
    }
}
