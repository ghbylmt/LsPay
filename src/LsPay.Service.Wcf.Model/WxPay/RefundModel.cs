using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 申请退款接口
    /// </summary>
    [DataContract]
    public class RefundModel
    {
        /// <summary>
        /// 微信订单号
        /// 微信生成的订单号，在支付通知中有返回,订单号长度最多28位
        /// </summary>
        [DataMember]
        public string  transaction_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户侧传给微信的订单号,最多32位
        /// </summary>
        [DataMember]
        public string  out_trade_no { get; set; }
        /// <summary>
        /// 商户退款单号
        /// 商户系统内部的退款单号，商户系统内部唯一，同一退款单号多次请求只退一笔,退款单号长度最多32位
        /// </summary>
        [DataMember]
        public string out_refund_no { get; set; }
        /// <summary>
        /// 总金额
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        [DataMember]
        public int total_fee { get; set; }
        /// <summary>
        /// 退款金额
        /// 退款总金额，订单总金额，单位为分，只能为整数
        /// </summary>
        [DataMember]
        public int refund_fee { get; set; }
        /// <summary>
        /// 货币种类
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，长度最多8位
        /// </summary>
        [DataMember]
        public string refund_fee_type { get; set; }
        /// <summary>
        /// 操作员
        /// 操作员帐号, 默认为商户号，账号长度不超过32位
        /// </summary>
        [DataMember]
        public string op_user_id { get; set; }
    }
}
