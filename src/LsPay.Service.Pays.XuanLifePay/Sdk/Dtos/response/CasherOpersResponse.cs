using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class CasherOpersResponse
    {
        public bool success { get; set; }
        public string merchant_id { get; set; }
        public string store_id { get; set; }
        public string casher_no { get; set; }
        public string casher_name { get; set; }
        public string casher_pwd { get; set; }
        public string casher_phone { get; set; }
        public string casher_status { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public string operatore_type { get; set; }
        public string responseTime { get; set; }
        public string fun { get; set; }

    }
}
