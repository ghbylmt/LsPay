using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class CasherOpersDto
    {
        public string merchant_id { get { return Config.Merchant_id; } }
        public string store_id { get; set; }
        public string shopowner_pwd { get; set; }
        public string operatore_type { get; set; }
        public string casher_no { get; set; }
        public string casher_name { get; set; }
        public string casher_pwd { get; set; }
        public string casher_phone { get; set; }
        public string sign { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string time_stamp { get; set; }
    }
}
