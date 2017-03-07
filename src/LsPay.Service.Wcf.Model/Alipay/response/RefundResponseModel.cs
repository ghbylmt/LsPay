using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    [DataContract]
    public class RefundResponseModel : BaseResponseModel
    {
        /// <summary>
        /// 支付宝交易号
        /// 支付宝交易凭证号
        /// </summary>
        [DataMember]
        public string trade_no { get; set; }
        /// <summary>
        /// 商户订单号
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 买家支付宝用户号，该参数已废弃，请不要使用
        /// </summary>
        public string open_id { get; set; }
        /// <summary>
        /// 用户的登录id
        /// </summary>
        [DataMember]
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 本次退款是否发生了资金变化
        /// </summary>
        [DataMember]
        public string fund_change { get; set; }
        /// <summary>
        /// 本次发生的退款金额
        /// </summary>
        [DataMember]
        public string refund_fee { get; set; }
        /// <summary>
        /// 退款支付时间
        /// </summary>

        [DataMember]
        public string gmt_refund_pay { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [DataMember]
        public string store_name { get; set; }
        /// <summary>
        /// 买家在支付宝的用户id
        /// </summary>
        [DataMember]
        public string buyer_user_id { get; set; }
        /// <summary>
        /// 实际退回给用户的金额
        /// </summary>
        [DataMember]
        public string send_back_fee { get; set; }
        /// <summary>
        /// 支付所使用的渠道
        /// </summary>
        [DataMember]
        public string fund_channel { get; set; }
        /// <summary>
        /// 该支付工具类型所使用的金额
        /// </summary>
        [DataMember]
        public string amount { get; set; }

    }
}
