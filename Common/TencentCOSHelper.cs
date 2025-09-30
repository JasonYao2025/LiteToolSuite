using COSXML;
using COSXML.Auth;
using COSXML.CosException;
using COSXML.Model.Bucket;
using COSXML.Model.Object;
using COSXML.Model.Tag;
using COSXML.Transfer;
using LiteToolSuite;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ZXing;
using static COSXML.Model.Tag.ListAllMyBuckets;

namespace Common
{

    public class TencentCOSHelper
    {
        private readonly CosXml _cosClient;

        public TencentCOSHelper(string secretId, string secretKey, string region)
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
                .SetRegion(region)
                .IsHttps(true)
                .SetDebugLog(true) // 可选调试日志
                .Build();

            QCloudCredentialProvider credential = new DefaultQCloudCredentialProvider(
                secretId, secretKey, 600); // 临时密钥有效期600秒

            _cosClient = new CosXmlServer(config, credential);
        }

        public string DownloadFile(string bucket, string cosKey, string localDir, string localFileName)
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest(bucket, cosKey, localDir, localFileName);

                var result = _cosClient.GetObject(request);
                return result.IsSuccessful() ? localDir + localFileName : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"下载失败: {ex.Message}");
                return null;
            }
        }

        public List<BucketFile> ListFiles(string bucket, string prefix = "",string delimiter="",string nextMarker="",string maxKeys="100")
        {
            List<BucketFile> bucketFiles=new List<BucketFile> ();
            BucketFile bucketFile;

            var request = new GetBucketRequest(bucket);
            request.SetPrefix(prefix);  // 指定对象键前缀过滤，常用于按目录层级筛选文件           
            //request.SetDelimiter(delimiter);   //扩展名过滤，只查询pdf文件，设置分隔符（通常为/），与SetPrefix配合实现目录层级展示
            //request.SetMarker(nextMarker); //分页标记符，用于从指定对象键之后开始列举。首次请求可不设置，后续请求使用上次返回的NextMarker值实现分页遍历
            request.SetMaxKeys(maxKeys);    // 限制返回数量避免超长响应

            // 设置查询2025年9月日志目录
            //request.SetPrefix("logs/2025/09/");
            //request.SetDelimiter("/");  // SetDelimiter需与SetPrefix配合使用才能生效
            //request.SetMarker("logs/2025/09/25.log"); // 从指定文件开始分页

            try
            {
                var result = _cosClient.GetBucket(request);
                
                List<ListBucket.Contents> contents = result.listBucket.contentsList;

                //输出文件List
                int total = contents.Count;
                
                foreach (var item in contents)
                {
                    bucketFile = new BucketFile();
                    bucketFile.CosKey = item.key;
                    bucketFile.Size = item.size;

                    //item.lastModified得到的時間是ISO8601 = "yyyy-MM-ddTHH:mm:ssZ"格式，并且是utc時間，所以要要转换成本地时间
                    item.lastModified = item.lastModified.Replace("T"," ").Replace("Z","").Replace(".000","");
                    string localTime = TimeHelper.ToLocalTime(item.lastModified).ToString("yyyy-MM-dd HH:mm:ss");
;
                    bucketFile.LastModified = localTime;

                    bucketFiles.Add(bucketFile);
                    
                }
                return bucketFiles;

            }
            catch (CosServerException e)
            {
                //MessageBox.Show("COS server error:" + e.errorCode+""+e.errorMessage, "提示");
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Unknown Error" + ex.Message, "提示");
                return null;
            }
        }     


        //var tencentCOS = new TencentCOSHelper(
        //    "AKIDxxxxxx",  // 替换为实际SecretId
        //    "xxxxxx",      // 替换为实际SecretKey
        //    "ap-guangzhou"); // 替换为实际Region

        //// 示例：列出存储桶文件
        //var fileList = tencentCOS.ListFiles("example-bucket");

        //// 示例：下载文件
        //string localFile = TencentCOSHelper.DownloadFile( "example-bucket",  "test/example.txt", @"C:\Downloads\example.txt");



        #region 测试Download COS Files
        //string secretId = "XXXXXX";
        //string secretKey = "XXXXXX";
        //string region = "ap-shanghai";
        ////string bucketName = "trip-dev-1316710660";
        //if (string.IsNullOrEmpty(FrmLogin.URL)) { return; }
        //var site = FrmLogin.URL.Replace("https://", "").Split('.');
        //string bucketName = "trip-" + site[0] + "-1316710660";

        //即bucket下面文件的全路径示例
        //string cosKey = "Debug/Log/20250904/2CC6822D67D1@qvl-1701142032-1701149234@sys@1756970561.tgz";  

        //var tencentCOS = new TencentCOSHelper(secretId, secretKey, region);

        //列出所有文件的名称
        //var fileList = tencentCOS.ListFiles(bucketName);

        //下载文件
        //var result = tencentCOS.DownloadFile(bucketName, cosKey, "d:\\Log\\", "syslog.tgz");

        //    if (!string.IsNullOrEmpty(result))
        //    {
        //        MessageBox.Show("Log has been saved to " + result, "提醒");
        //    }

        #endregion



    }
}
