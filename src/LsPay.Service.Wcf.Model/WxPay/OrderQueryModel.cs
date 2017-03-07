using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 查询订单接口
    /// </summary>
    [DataContract]
    public class OrderQueryModel
    {
        /// <summary>
        /// 微信订单号
        /// 微信订单号优先使用，订单号长度最多32位
        /// </summary>
        [DataMember]
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户系统内部的订单号，当没提供transaction_id时需要传这个,订单号长度最多32位。
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
    }
}
