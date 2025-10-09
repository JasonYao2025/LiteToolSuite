using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteToolSuite.Models
{
    internal class Account
    {
    }

    public class RequestLoginAccount
    {
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        public int Type { get; set; }
    }
}
