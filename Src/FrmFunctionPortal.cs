using Common;
using LiteToolSuite.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite
{
    public partial class FrmFunctionPortal : Form
    {
        public static string Token= "";
        public static DateTime Token_Get_Time;
        public static string URL = "";
        public static string SiteName = "";

        private FrmLogin frmLogin;
        private const int GRID_SIZE = 3;
        
        private readonly string[,] ToolName = new string[3, 3];
        
        
        public FrmFunctionPortal()
        {      

            InitializeComponent();
            
            FilloutCombLang(LiteToolSuite.Properties.Settings.Default.Language);  //下拉列表框填充内容，并设定选择项
            

            //先定义TableLayoutPanel的大小等等
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.None,  //设成fill会铺满整个界面
                ColumnCount = GRID_SIZE,
                RowCount = GRID_SIZE,
                Size = new Size(800, 550),
                Location = new Point(30, 55)

            };

            //取多语言Resources文件里面的内容
            ToolName[0, 0] = LiteToolSuite.Properties.Resources.Feature1;
            ToolName[0, 1] = LiteToolSuite.Properties.Resources.Feature2;
            ToolName[0, 2] = LiteToolSuite.Properties.Resources.Feature3;
            ToolName[1, 0] = LiteToolSuite.Properties.Resources.Feature4;
            ToolName[1, 1] = LiteToolSuite.Properties.Resources.Feature5;
            ToolName[1, 2] = LiteToolSuite.Properties.Resources.Feature6;
            ToolName[2, 0] = LiteToolSuite.Properties.Resources.Feature7;
            ToolName[2, 1] = LiteToolSuite.Properties.Resources.Feature8;
            ToolName[2, 2] = LiteToolSuite.Properties.Resources.Feature9;

            ResourceManager rm = new ResourceManager("LiteToolSuite.FrmFunctionPortal", Assembly.GetExecutingAssembly());
            string value = rm.GetString("lable5.Text");  // 获取字符串

            //在定义PictureBox，并循环加入TableLayoutPanel里面
            for (int i = 0; i < GRID_SIZE; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / GRID_SIZE));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / GRID_SIZE));

             
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    var pb = CreatePictureBox(ToolName[i, j], i, j);             
                    panel.Controls.Add(pb, j, i);                   
                }
            }

            this.Controls.Add(panel);            
        }


        /// <summary>
        /// create PictureBox并写上文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private PictureBox CreatePictureBox(string text, int row, int col)
        {
            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(200, 230, 250),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = Image.FromFile($"btn_{row}_{col}.png"),

                Tag = $"{row},{col}"  //可用于定位，因为要考虑多语言
                //Tag = text
            };

            pb.Paint += (sender, e) => {
                var rect = new Rectangle(0, 0, pb.Width, pb.Height - 5); //离pb底部5个像素
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Far
                };

                e.Graphics.DrawString(text,
                    new Font("微软雅黑", 14, FontStyle.Bold),
                    Brushes.DarkSlateBlue,
                    rect, sf);
            };

            pb.Click += PictureBox_Click;   //绑定点击事件

            return pb;
        }

        /// <summary>
        /// 根据点击位置判断需要启动的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void PictureBox_Click(object sender, EventArgs e)
        {
            var btn = sender as PictureBox;
            //MessageBox.Show($"点击了 {btn.Tag} 按钮");
            
            switch (btn.Tag) 
            {
                case "0,0":  //后端服务器的行为不一样，所以检查toke的code先注释掉
                    //if (LoginOperation.CheckToken(Token, Token_Get_Time))
                    //{
                        FrmMqtt frmMqtt = new FrmMqtt();
                        frmMqtt.ShowDialog();
                    //}
                    //else
                    //{
                    //    CallFrmLogin();
                    //}
                    return;
                case "0,1":
                    //if (LoginOperation.CheckToken(Token, Token_Get_Time))
                    //{
                        FrmVehicle vehicle = new FrmVehicle();
                        vehicle.ShowDialog();
                    //}
                    //else
                    //{
                    //    CallFrmLogin();
                    //}
                    return;
                case "0,2":
                    FrmRename rename = new FrmRename();
                    rename.ShowDialog();
                    return;
                case "1,0":
                    FrmTimestamp timestamp = new FrmTimestamp();
                    timestamp.ShowDialog();
                    return;
                case "1,1":
                    FrmAbout about = new FrmAbout();
                    about.ShowDialog();
                    return;
                case "1,2":
                    frmLogin = new FrmLogin();
                    frmLogin.ShowDialog();
                    return;
                case "2,0": 
                    FrmInputContent inputContent = new FrmInputContent();
                    inputContent.ShowDialog();
                    return;
                case "2,1":                   
                    return;
                case "2,2":
                    URL = "";
                    Token = "";
                    Application.Exit();
                    return;
            }
        }

        //Call Login Frm
        private void CallFrmLogin()
        {
            MessageBox.Show("需要先登录站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
        }


        /// <summary>
        /// 填充语言选中下拉框
        /// </summary>
        /// <param name="index"></param>
        private void FilloutCombLang(string lang)
        {
            //先删除change事件，绑定数据后再添加change事件，否则会在绑定数据的时候就触发change事件，在事件中获取的value值出错
            combLang.SelectedIndexChanged -= combLang_SelectedIndexChanged;
            var LangDict = new Dictionary<string, string> { { "English", "en-US" }, { "简中", "zh-CN" } };
            combLang.DataSource = new BindingSource(LangDict, null);
            combLang.DisplayMember = "Key";
            combLang.ValueMember = "Value";
            
            combLang.SelectedValue = lang;
            combLang.SelectedIndexChanged += combLang_SelectedIndexChanged;
        }

        private void combLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combLang.SelectedValue!=null && !string.IsNullOrEmpty(combLang.SelectedValue.ToString())) 
            {
                try
                {
                    string sqliteSQL = string.Format("SELECT id, language FROM system_setting");
                    var dataTable = Program.SQLiteHelper.ExecuteDataset(sqliteSQL, null).Tables[0];                 
                   

                    if (dataTable.Rows.Count == 1)  //已经有值，update
                    {
                        int id = StringHelper.StringToInt(dataTable.Rows[0][0].ToString());
                        sqliteSQL = string.Format("UPDATE system_setting SET language='{0}' WHERE id={1}", combLang.SelectedValue.ToString(), id);
                    }
                    else  //如果为空，插入一条数据
                    {
                        sqliteSQL = string.Format("INSERT INTO system_setting(language) VALUES('{0}')", combLang.SelectedValue.ToString());
                    }

                    Program.SQLiteHelper.ExecuteScalar(sqliteSQL);  //保存语言选择至数据库

                }
                catch (Exception ex) { }
            }                

            // 读取嵌入资源
            //foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            //{
            //    MessageBox.Show(name, "嵌入资源");
            //}

            //上面打印出的name，不用带 .resources，然后填入下面语句；  所有的资源文件会被编译成 Namespace.resources.dll，在不同语言的文件夹下
            ResourceManager rm = new ResourceManager("LiteToolSuite.FrmFunctionPortal", Assembly.GetExecutingAssembly());
            string value = rm.GetString("MessageForLang");  // 获取字符串
            //byte[] fileData = (byte[])rm.GetObject("BinaryKey");  // 获取二进制数据

            // 询问是否重启, 在Properties存系统共用的内容，比如消息框
            DialogResult result = MessageBox.Show( text: value, caption: LiteToolSuite.Properties.Resources.MessageBoxCapInformation, buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}
