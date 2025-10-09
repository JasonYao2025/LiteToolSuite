using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace LiteToolSuite.BLL
{
    public class LoginOperation
    {
        public static string GetToken(string hostName, string mobile, string passwd)
        {
            //API:  host/auth/login  {"Mobile":"17777777777","Password":"123456","Type":1}
            string token = "", statusCode = "";

            string url = hostName + "/auth/login";
            var loginAccount = new RequestLoginAccount();
            loginAccount.Mobile = mobile;
            loginAccount.Password = passwd;
            loginAccount.Type = 1;


            string body = JsonConvert.SerializeObject(loginAccount);
            string tokenResult = HttpClientHelper.HttpPostforLogin(url, body, out statusCode);

            JObject jsonData = JObject.Parse(tokenResult);

            if (!string.IsNullOrEmpty(statusCode) && jsonData["Result"].ToString() == "True")
            {

                token = jsonData["Data"]["Token"].ToString().Replace("{", "").Replace("}", "");
            }

            return token;
        }

        public static bool CheckToken(string token, DateTime token_time)
        {
            if (string.IsNullOrEmpty(token))
            {
                //MessageBox.Show("token不存在，请先登录！","提示");               
                return false;
            }

            var diff = DateTime.Now - token_time;
            if (diff.TotalMinutes > 28)
            {
                //MessageBox.Show("token已经超时，请重新登录！", "提示");
                return false;
            }
            return true;
        }
    }
}
