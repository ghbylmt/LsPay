using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    /// <summary>
    /// 微信退款接口响应数据传输类
    /// </summary>
    public class RefundResponseModel : BaseBusinessResponseModel
    {
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 微信支付分配的终端设备号，与下单一致
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户系统内部的订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string out_refund_no { get; set; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string refund_id { get; set; }
        /// <summary>
        /// 退款渠道
        /// ORIGINAL—原路退款
        /// BALANCE—退回到余额
        /// </summary>
        public string refund_channel { get; set; }
        /// <summary>
        /// 退款总金额,单位为分,可以做部分退款
        /// </summary>
        public string refund_fee { get; set; }
        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 订单金额货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 现金支付金额，单位为分，只能为整数
        /// </summary>
        public string cash_fee { get; set; }
        /// <summary>
        /// 现金退款金额，单位为分，只能为整数
        /// </summary>
        public string cash_refund_fee { get; set; }
        /// <summary>
        /// 代金券或立减优惠使用数量
        /// </summary>
        public string coupon_refund_count { get; set; }
        /// <summary>
        /// 代金券或立减优惠ID
        /// </summary>
        public string coupon_refund_id { get; set; }
    }
}
