using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 查询退款接口
    /// </summary>
    [DataContract]
    public class RefundQueryModel
    {
        /// <summary>
        /// 微信订单号
        /// 微信订单号长度不超过28位
        /// </summary>
        [DataMember]
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户系统内部的订单号，长度不超过32位
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商户退款单号
        /// 商户侧传给微信的退款单号，长度不超过32位
        /// </summary>
        [DataMember]
        public string out_refund_no { get; set; }
        /// <summary>
        /// 微信退款单号
        /// 微信生成的退款单号，在申请退款接口有返回，退款单号不超过28位
        /// </summary>
        [DataMember]
        public string refund_id { get; set; }
    }
}
