using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class ActiveResponse
    {
        public string success { get; set; }
        public string terminal_id { get; set; }
        public string merchant_id { get; set; }
        public string store_id { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public string responseTime { get; set; }
        public string fun { get; set; }
        public string sign { get; set; }
        public string merchant_name { get; set; }
        public string storeName { get; set; }
        public string key { get; set; }
        public string oem { get; set; }
    }
}
