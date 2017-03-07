using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay
{
    /// <summary>
    /// 申请退款数据传输类
    /// </summary>
    [DataContract]
    public class RefundModel
    {
        /// <summary>
        /// 商户订单号
        /// String(64)
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付宝订单号
        /// </summary>
        [DataMember]
        public string trade_no { get; set; }
        /// <summary>
        /// 需要退款的金额，该金额不能大于订单金额,单位为元，支持两位小数
        /// </summary>
        [DataMember]
        public string refund_amount { get; set; }
        /// <summary>
        /// 退款的原因说明
        /// </summary>
        [DataMember]
        public string refund_reason { get; set; }
        /// <summary>
        /// 标识一次退款请求，同一笔交易多次退款需要保证唯一
        /// </summary>
        [DataMember]
        public string out_request_no { get; set; }
        /// <summary>
        /// 商户的操作员编号
        /// </summary>
        [DataMember]
        public string operator_id { get; set; }
        /// <summary>
        /// 商户的门店编号
        /// </summary>
        [DataMember]
        public string store_id { get; set; }
        /// <summary>
        /// 商户的终端编号
        /// </summary>
        [DataMember]
        public string terminal_id { get; set; }
    }
}
