using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpManager
    {

        public static string Get(string Url)
        {
            string retString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            retString = streamReader.ReadToEnd();

            streamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if(request!=null)
            {
                request.Abort();
            }

            return retString;
        }

        public static string Post(string Url,string Data,string Referer)
        {
            string retString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Referer = Referer;
            byte[] bytes = Encoding.UTF8.GetBytes(Data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();            
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            retString = streamReader.ReadToEnd();

            streamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return retString;
        }


        public static Task<string> HttpPostAsync(string url, string postData = null, string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }
            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers[header.Key] = header.Value;
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(postData ?? "");
                using (Stream sendStream = request.GetRequestStream())
                {
                    sendStream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    return streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }

        }
        public static Task<string> HttpGetAsync(string url, Dictionary<string, string> headers = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (headers != null)
                {
                    foreach (var header in headers)
                        request.Headers[header.Key] = header.Value;
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    return streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        #region WebRequest added by jasonyao May.9,2023 
        public static string HttpGet(string url, string token)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;

            //构造一个Web请求的对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            var headers = request.Headers;
            headers["Authorization"] = "Bearer " + token; //传递进去认证Token
            request.Headers = headers;

            //获取web请求的响应的内容
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string HttpPost(string url, string body, string token)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST"; //Post请求方式
            //request.Accept = "application/json";
            request.MediaType = "application/json";
            request.Accept = "text/html, application/xhtml+xml,application/json, */*";

            // 内容类型
            request.ContentType = "application/json;charset=UTF-8";//参数格式:  {username:"admin",password:"123} 如果参数不是json类型会报错
            //request.ContentType = "application/x-www-form-urlencoded"; //参数：username=admin&password=123 如果参数是json格式或者参数写错不会报错的

            var headers = new WebHeaderCollection();
            //headers.Add("Authorization", token);
            //headers.Add("via-agent", "com.viatech.serverful.wechat");
            //headers.Add("via-verify-web-version", "d69e903d32452d4b91130e441f00a61d7d601c177079322f9ffd1b5422a59000");
            headers["Authorization"] = token; //传递进去认证Token            
            //headers["Authorization"] = "Bearer " + token; //JWT的方式，统一采用上面一句，在调用的时候拼接加入Bearer 

            //server端有版本验证, 如果配置文件里面有版本，就在header里面添加相应内容，否则不传值
            //string version = AppSetting.GetSection("VIACloud:Version").Value;
            //if (!string.IsNullOrEmpty(version))
            //{
            //    string salt = AppSetting.GetSection("VIACloud:Salt").Value;
            //    string sha256value = SecurityEncDecrypt.SHA256EncryptString(version + salt);
            //    headers["via-verify-web-version"] = sha256value;
            //}

            //headers["via-agent"] = "com.viatech.serverful.qa";           

            request.Headers = headers;

            byte[] dataBytes = Encoding.UTF8.GetBytes(body);
            request.ContentLength = dataBytes.Length;

            using (var stream = request.GetRequestStream())
            {
                //byte[] dataBytes = Encoding.UTF8.GetBytes(body);
                stream.Write(dataBytes, 0, dataBytes.Length);
            }


            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //上面的代码会有异常出现，更改如下：

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                //WriteLogToDB("Server", "", "", "HttpRequest", "headers: " + headers + " response: " + response);
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                //WriteLogToDB("Server", "", "", "HttpRequest", "headers: " + headers + " error response: " + ex);
            }


            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        ///  API发送DELETE请求，返回状态：200成功，201失败
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpDelete(string url)
        {
            //Web访问对象64
            //string serviceUrl = string.Format("{0}/{1}", this.BaseUri, uri);
            string serviceUrl = url;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            myRequest.Method = "DELETE";
            // 获得接口返回值68
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string ReturnXml = HttpUtility.UrlDecode(reader.ReadToEnd());
            string ReturnXml = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReturnXml;
        }

        public static void WriteLogToDB(string Operator, string DeviceId, string OpName, string Type, string OpContent)
        {
            // WebAPI calling log with calling time
            DateTime dtOpTime;
            //string sNow = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss:fff");  //utc 时间
            string sNow = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff");  //包括时区的时间，即服务器本地时间
            string DateTimeNow = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            string sSQL = string.Format(@"INSERT INTO t_log(Operator,DeviceId,OpName,Type,OpContent,LocalTimeFormat,DatetimeOpTime) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                                        Operator, DeviceId, OpName, Type, OpContent, sNow, DateTimeNow);
            try
            {
                //Startup.myDB.ExecuteDataSet(sSQL);
                //DBServerProvider.SqlDapper.ExecuteScalar(sSQL, null);   //直接执行SQL Statement
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}
