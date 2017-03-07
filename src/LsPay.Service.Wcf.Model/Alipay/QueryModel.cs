using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay
{
    [DataContract]
    public class QueryModel
    {
        // <summary>
        /// 商户订单号
        /// String(64)
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付宝交易流水号
        /// </summary>
        [DataMember]
        public string trade_no { get; set; }
    }
}
