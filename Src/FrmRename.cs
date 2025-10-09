using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization.Configuration;

namespace LiteToolSuite
{
    public partial class FrmRename : Form
    {
        private readonly string ROOT="";
        private static string FirstCode = "";
        private static string SecondCode = "";

        public FrmRename()
        {
            InitializeComponent();
            FirstCode = tbFirstCode.Text.Trim();
            SecondCode = tbSecondCode.Text.Trim();
        }

        private void btChangeName_Click(object sender, EventArgs e)
        {
            string sourcePath = tbSourcePath.Text.Trim();

            if (sourcePath == null || sourcePath == "") return;
            if (string.IsNullOrEmpty(FirstCode) || string.IsNullOrEmpty(SecondCode))
            {
                MessageBox.Show("请先输入文件名的第一码和第二码内容", "提醒");
                return;
            }

            string destPath = sourcePath + "\\NewFolder\\";
            int total = 0;
           
            rtbChangeHistory.Text = string.Empty;

            DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);          

            
            //int totalFile = sourceDirectory.GetFiles().Length;

            if (!Directory.Exists(destPath))
            {
                try
                {
                    Directory.CreateDirectory(destPath);
                }
                catch (Exception ex) { }
            }

            foreach (FileInfo item in sourceDirectory.GetFiles())
            {
                string fileName = item.Name;
                int beginLoation = fileName.IndexOf("@CaptureOnDemand@");

                

                if (fileName.Contains("@CaptureOnDemand@"))
                {
                    //lbNotes.Text = "Begin change, there are " + totalFile + " found. " + total + " haved been changed.";

                    string strTimestamp = fileName.Substring(beginLoation + 17, 13);
                    
                    DateTime dt = ConvertStringToDateTime(strTimestamp);

                    string datetime = dt.ToString("yyyy-MM-dd-HH-mm-ss");

                    string cameraId = fileName.Substring(beginLoation + 31, 1);
                              

                    string newFileName = FirstCode+"-" + SecondCode + cameraId + "-" + datetime + ".jpg";
                    string newFullName = destPath + newFileName;
                    item.CopyTo(newFullName,true);  //override file

                    rtbChangeHistory.Text += total+". "+fileName + "  变为  " + newFileName+"\r\n";
                    
                    total++;
                }
                else
                {
                    string newFullName = destPath + fileName;
                    item.CopyTo(newFullName);
                }
            }

            MessageBox.Show("Congratulation! "+total+" files have been changed successfully!", "Info");
            


        }

        private void btSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择图片所在文件夹";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
              tbSourcePath.Text = dialog.SelectedPath;
            }
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        private DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            rtbChangeHistory.Text= string.Empty;
            tbSourcePath.Text= string.Empty;
        }

        private void btFileName_Click(object sender, EventArgs e)
        {
            FirstCode = tbFirstCode.Text.Trim();
            SecondCode = tbSecondCode.Text.Trim();

            if (string.IsNullOrEmpty(FirstCode) || string.IsNullOrEmpty(SecondCode)) 
            {
                MessageBox.Show("请先输入文件名的第一码和第二码内容", "提醒");
                return;
            }
        }
    }
}
