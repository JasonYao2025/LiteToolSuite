using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{
    public class DeviceModel
    {
        //{"DeviceId": "2CC6822D8490",   "AVersion":"v1r010"}
        //{"DeviceId": "73D53ED33C210F1821E0475DCC872A59", "AVersion":"2"
        public string DeviceId { get; set; }
        public string AVersion { get; set; }
    }


    /// <summary>
    /// Camera状态
    /// </summary>
    public class CameraPlugStatus
    {
        public string Id { get; set; }
        public int Status { get; set; }
    }

}
