using Common;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.BLL;
using LiteToolSuite.Common;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace LiteToolSuite
{
    public partial class FrmVehicle : Form
    {
        private ContextMenuStrip ctxMenu;

        public FrmVehicle()
        {
            InitializeComponent();
                      
           
            Dictionary<string, string> vehicles = new Dictionary<string, string>();
            Dictionary<string, string> devices = new Dictionary<string, string>();
            //if (VehicleOperation.GetVehicleList(FrmFunctionPortal.URL, FrmFunctionPortal.Token, out vehicles))
            if(VehicleOperation.GetVehicleListFromSQLite(Program.SQLiteHelper,out vehicles))
            {
                if (vehicles.Count > 0)
                {
                    #region 填充combVehicle
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

                    #endregion

                    #region 填充combAIDevice

                    foreach (var vehicle in vehicles)
                    {

                        if (!vehicle.Value.Contains("没有绑定") && !string.IsNullOrEmpty(vehicle.Value))
                        {
                            device = vehicle.Value.Split(',');

                            var key = device[0];
                            var value = vehicle.Key + "," + device[1];

                            devices.Add(key, value);

                        }
                    }

                    // 先去除空值，然后调用排序函数使车辆按照中文首字母排序
                    RemoveEmptyValues(devices);

                    var sort = devices.OrderBy(kv => StringHelper.GetFirstPinyin(kv.Key));
                    devices = sort.ToDictionary(kv => kv.Key, kv => kv.Value);


                    if (devices.Count > 0)
                    {
                        //先删除change事件，绑定数据后再添加change事件，否则会在绑定数据的时候就触发change事件，数据显示出错
                        combAIDevice.SelectedIndexChanged -= combAIDevice_SelectedIndexChanged;
                        combAIDevice.DataSource = new BindingSource(devices, null);
                        combAIDevice.DisplayMember = "Key";
                        combAIDevice.ValueMember = "Value";

                        combAIDevice.SelectedIndex = 0;
                        string[] tmpVehicle = combAIDevice.SelectedValue.ToString().Split(',');
                        tbVehicleName.Text = tmpVehicle[0];
                        tbAIDeviceModel.Text = tmpVehicle[1];

                        combAIDevice.SelectedIndexChanged += combAIDevice_SelectedIndexChanged;
                    }

                    #endregion

                    label1.Location = new Point(label2.Left, label2.Top + 50);

                    #region 设置Treeview
                    // 右键菜单
                    ctxMenu = new ContextMenuStrip();
                    ctxMenu.Items.Add("复制节点", null, (s, e) => CopySelectedNode(false));
                    ctxMenu.Items.Add("复制子树", null, (s, e) => CopySelectedNode(true));
                    treeView1.ContextMenuStrip = ctxMenu;

                    //搜索框
                    tbSearchText.Text = LiteToolSuite.Properties.Resources.SearchTextPrompt;
                    tbSearchText.ForeColor = Color.Gray;

                    tbSearchText.GotFocus += (s, e) =>
                    {
                        if (tbSearchText.Text == LiteToolSuite.Properties.Resources.SearchTextPrompt)
                        {
                            tbSearchText.Text = "";
                            tbSearchText.ForeColor = Color.Black;
                        }
                    };

                    tbSearchText.LostFocus += (s, e) =>
                    {
                        if (string.IsNullOrEmpty(tbSearchText.Text))
                        {
                            tbSearchText.Text = LiteToolSuite.Properties.Resources.SearchTextPrompt;
                            tbSearchText.ForeColor = Color.Gray;
                        }
                    };
                    #endregion
                }
                else
                {
                    MessageBox.Show("未找到车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void CopySelectedNode(bool withChildren)
        {
            if (treeView1.SelectedNode != null)
            {
                string content = withChildren
                    ? TreeViewHelper.CopyNodeWithChildren(treeView1.SelectedNode)
                    : treeView1.SelectedNode.Text;

                TreeViewHelper.CopyToClipboard(content);
            }
        }

        static void RemoveEmptyValues(Dictionary<string, string> dict)
        {
            foreach (var key in dict.Keys.ToList())
            {

                if (string.IsNullOrEmpty(key))
                {
                    dict.Remove(key);
                }
            }
        }       
        
        private void combVehicleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] device = combVehicleList.SelectedValue.ToString().Split(',');

            tbDeviceId.Text = device[0];
            tbDeviceModel.Text = device[1];
        }      

       

        private void btnCopyDeviceId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDeviceId.Text.Trim()))
            {
                Clipboard.SetText(tbDeviceId.Text.Trim());
            }
        }     
               

        private void btnVehicleConfig_Click(object sender, EventArgs e)
        {
            //get vehicle info
            string vehicleData = "";
            string[] device = combVehicleList.SelectedValue.ToString().Split(',');
            treeView1.Nodes.Clear();


            ResourceManager rm = new ResourceManager("LiteToolSuite.FrmVehicle", Assembly.GetExecutingAssembly());
            string text = rm.GetString("MessageBoxVehicleConfigContent");  // 获取字符串
            string caption = rm.GetString("MessageBoxCaption");
            MessageBox.Show(text, caption,MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (VehicleOperation.GetVehicleData(FrmFunctionPortal.URL, FrmFunctionPortal.Token, device[2], out vehicleData)) 
            if (VehicleOperation.GetVehicleDataFromSQLite(Program.SQLiteHelper, device[2],out vehicleData))
            {
               
                try
                {
                    var jToken = JToken.Parse(vehicleData);
                    TreeViewHelper.BuildTreeNodes(jToken, treeView1.Nodes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"JSON解析错误: {ex.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }       
        }      
           

        private void combAIDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] vehicle = combAIDevice.SelectedValue.ToString().Split(',');

            tbVehicleName.Text = vehicle[0];
            tbAIDeviceModel.Text = vehicle[1];
        }       

        private void btnResetRichtextbox_Click(object sender, EventArgs e)
        {
            tbSearchText.Text = LiteToolSuite.Properties.Resources.SearchTextPrompt;

            // 清空所有节点
            //treeView1.Nodes.Clear();
            //MessageBox.Show("TreeView已清空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TreeViewHelper.SafeCollapse(treeView1.Nodes);
        }

        private void tbSearchText_TextChanged(object sender, EventArgs e)
        {          
            //执行搜索
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                //TreeViewHelper.SearchNodes(treeView1.Nodes, tbSearchText.Text);
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            //执行搜索
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                TreeViewHelper.SearchNodes(treeView1.Nodes, tbSearchText.Text);
            }
        }
    }
}
