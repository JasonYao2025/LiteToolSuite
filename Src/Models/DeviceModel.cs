using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{
    public class DeviceModel
    {
        
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
