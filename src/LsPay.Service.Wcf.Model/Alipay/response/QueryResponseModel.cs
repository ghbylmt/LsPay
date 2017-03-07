using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    [DataContract]
    public class QueryResponseModel : BaseResponseModel
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
        /// 买家支付宝用户号
        /// 买家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。
        /// </summary>
        [DataMember]
        public string buyer_user_id { get; set; }
        /// <summary>
        /// 买家支付宝账号
        /// 买家支付宝账号，将用*号屏蔽部分内容
        /// </summary>
        [DataMember]
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 交易状态
        /// WAIT_BUYER_PAY	交易创建，等待买家付款
        /// TRADE_CLOSED	未付款交易超时关闭，或支付完成后全额退款
        /// TRADE_SUCCESS	交易支付成功
        /// TRADE_FINISHED	交易结束，不可退款
        /// </summary>
        [DataMember]
        public string trade_status { get; set; }
        /// <summary>
        /// 订单金额
        /// 本次交易支付的订单金额，单 位为人民币（元）。
        /// </summary>
       [DataMember]
        public string total_amount { get; set; }
        /// <summary>
        /// 实收金额
        /// 商家在交易中实际收到的款项，单位为元
        /// </summary>
        [DataMember]
        public string receipt_amount { get; set; }
        /// <summary>
        /// 开票金额
        /// 用户在交易中支付的可开发票的金额
        /// </summary>
        [DataMember]
        public string invoice_amount { get; set; }
        /// <summary>
        /// 付款金额
        /// 用户在交易中支付的金额
        /// </summary>
        [DataMember]
        public string buyer_pay_amount { get; set; }
        /// <summary>
        /// 积分宝金额
        /// 使用积分宝支付的金额
        /// </summary>
        [DataMember]
        public string point_amount { get; set; }
    }
}
