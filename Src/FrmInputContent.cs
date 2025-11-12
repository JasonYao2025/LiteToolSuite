using Common;
using LiteToolSuite.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteToolSuite
{
    public partial class FrmInputContent : Form
    {       
        public FrmInputContent()
        {
            InitializeComponent();           
        
        }     
     

        private void btnCpToClipboard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbInputContent.Text.Trim())) { return; }
            Clipboard.SetText(rtbInputContent.Text.Trim());
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            bool useNumbers = false, useLowercase = false, useUppercase = false,useSpecialChars = false, useOtherChars = false;

            if (cbNumber.Checked) { useNumbers= true; }
            if (cbLowerChar.Checked) { useLowercase = true; }
            if (cbUpperChar.Checked) { useUppercase = true; }
            if (cbSpecialChar.Checked) { useSpecialChars = true; }
            if (cbOtherChar.Checked) { useOtherChars = true; }
            //if (cbUpperChar.Checked) { useLowercase = true; }
            if (!useNumbers && !useLowercase && !useUppercase && !useSpecialChars&&!useOtherChars) { return; }

            int length = StringHelper.StringToInt(nudContentLength.Value.ToString());
            if (length > 0) 
            {
                string content = InputOperation.GenerateRandomString(length, useNumbers, useLowercase, useUppercase, useSpecialChars,useOtherChars);

                if (!string.IsNullOrEmpty(content))
                {
                    rtbInputContent.Text = content;
                }
            }
           
        }

        private void btnHTML_Click(object sender, EventArgs e)
        {
            rtbInputContent.Text = "";
            rtbInputContent.Text = "<script>alert('XSS')</script>";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbInputContent.Text = "";
        }
    }
}
