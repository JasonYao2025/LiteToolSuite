using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteToolSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.BLL
{
    public class VehicleOperation
    {
        /// <summary>
        /// call api(host/vehicle/list) to get vehicle list with department
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="token"></param>
        /// <param name="vehicleDict"></param>
        /// <returns></returns>
        public static bool GetVehicleList(string URL, string token, out Dictionary<string, string> vehicleDict)
        {
            string statusCode = "";


            vehicleDict = new Dictionary<string, string>();

            string url = URL + "/vehicle/list";
            Department department = new Department();

            List<string> dpts = new List<string>();
            dpts.Add("dpt-root");
            department.DptIds = dpts;


            string payload = JsonConvert.SerializeObject(department);

            string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
            JObject jsonResponse = JObject.Parse(response);

            if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK" && jsonResponse["Result"].ToString() == "True")
            {

                string vehicleName = "", vehicleId = "", deviceId = "", deviceModel = "";
                foreach (var vehicle in jsonResponse["Data"])
                {
                    vehicleName = vehicle["Name"].ToString();
                    vehicleId = vehicle["Id"].ToString();

                    //jsonResponse.Property("Devices")  //判断是否有键值
                    if (vehicle["DeviceId"] != null)    //旧版本的数据结构，直接解析DeviceId和DeviceModel
                    {
                        deviceId = vehicle["DeviceId"].ToString();
                        deviceModel = vehicle["DeviceModel"].ToString();
                    }
                    else if (vehicle["Devices"].HasValues)  //判断是否有绑定设备
                    {
                        deviceId = vehicle["Devices"][0]["Id"].ToString();  //取主设备即可
                        deviceModel = vehicle["Devices"][0]["Model"].ToString();
                    }
                    else
                    {
                        deviceId = "没有绑定AI设备";
                        deviceModel = "";
                    }
                    vehicleDict.Add(vehicleName, deviceId + "," + deviceModel + "," + vehicleId);
                }

                // 调用排序函数使车辆按照中文首字母排序
                var sort = vehicleDict.OrderBy(kv => StringHelper.GetFirstPinyin(kv.Key));
                vehicleDict = sort.ToDictionary(kv => kv.Key, kv => kv.Value);
                return true;
            }
            return false;

        }

        /// <summary>
        /// 更加vehicleId获取vehicle配置信息
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="token"></param>
        /// <param name="vehicleId"></param>
        /// <param name="vehicleData"></param>
        /// <returns></returns>
        public static bool GetVehicleData(string URL, string token, string vehicleId,out string vehicleData)
        {
            string statusCode = "";
            vehicleData = "";
                                  
            string url = URL + "/vehicle/get";

            var vdr = new VehicleDataRequest();
            vdr.Id = vehicleId;
        
            string payload = JsonConvert.SerializeObject(vdr);

            string response = HttpClientHelper.HttpPost(url, payload, token, out statusCode);
            JObject jsonResponse = JObject.Parse(response);

            if (!string.IsNullOrEmpty(statusCode) && statusCode == "OK" && jsonResponse["Result"].ToString() == "True")
            {

               vehicleData = jsonResponse["Data"].ToString();
               return true;
            }
            return false;

        }


        #region 为了demo功能，直接读取SQLite里面的车辆数据

        public static bool GetVehicleListFromSQLite(SQLiteHelper sQLiteHelper, out Dictionary<string, string> vehicleDict)
        {         

            vehicleDict = new Dictionary<string, string>();
            string sqliteSQL = string.Format("SELECT vehicle_data FROM vehicle");
            var dataTable = sQLiteHelper.ExecuteDataset(sqliteSQL, null).Tables[0];

            if (dataTable.Rows.Count > 0 && !string.IsNullOrEmpty(dataTable.Rows[0][0].ToString()))
            {
                JObject jsonResponse = JObject.Parse(dataTable.Rows[0][0].ToString());
                if (jsonResponse["Result"].ToString() == "True")
                {

                    string vehicleName = "", vehicleId = "", deviceId = "", deviceModel = "";

                    vehicleName = jsonResponse["Data"]["Name"].ToString();
                    vehicleId = jsonResponse["Data"]["Id"].ToString();
                    deviceId=jsonResponse["Data"]["Devices"][0]["Id"].ToString() ;
                    deviceModel=jsonResponse["Data"]["Devices"][0]["Model"].ToString();
                                       
                    vehicleDict.Add(vehicleName, deviceId + "," + deviceModel + "," + vehicleId);                    

                    // 调用排序函数使车辆按照中文首字母排序
                    var sort = vehicleDict.OrderBy(kv => StringHelper.GetFirstPinyin(kv.Key));
                    vehicleDict = sort.ToDictionary(kv => kv.Key, kv => kv.Value);
                    return true;
                }             
            }
            return false;
        }


        public static bool GetVehicleDataFromSQLite(SQLiteHelper sQLiteHelper, string vehicleId, out string vehicleData)
        {           
            vehicleData = "";
            string sqliteSQL = string.Format("SELECT vehicle_data FROM vehicle");
            var dataTable = sQLiteHelper.ExecuteDataset(sqliteSQL, null).Tables[0];

            if (dataTable.Rows.Count > 0 && !string.IsNullOrEmpty(dataTable.Rows[0][0].ToString()))
            {
                JObject jsonResponse = JObject.Parse(dataTable.Rows[0][0].ToString());

                if (jsonResponse["Result"].ToString() == "True")
                {

                    vehicleData = jsonResponse["Data"].ToString();
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
