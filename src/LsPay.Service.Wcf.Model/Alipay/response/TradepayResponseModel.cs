using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    /// <summary>
    /// 条码支付项目数据传输类
    /// </summary>
    [DataContract]
    public class TradepayResponseModel : BaseResponseModel
    {
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [DataMember]
        public string trade_no { get; set; }
        /// <summary>
        /// 商户订单号   
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 买家支付宝用户号
        /// </summary>
        [DataMember]
        public string buyer_user_id { get; set; }
        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        [DataMember]
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        [DataMember]
        public string total_amount { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        [DataMember]
        public string receipt_amount { get; set; }
        /// <summary>
        /// 开票金额
        /// </summary>
        [DataMember]
        public string invoice_amount { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        [DataMember]
        public string buyer_pay_amount { get; set; }
        /// <summary>
        /// 集分宝金额
        /// </summary>
        [DataMember]
        public string point_amount { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        [DataMember]
        public string gmt_payment { get; set; }
        /// <summary>
        /// 交易资金明细信息集合
        /// </summary>
        [DataMember]
        public string fund_bill_list { get; set; }
        /// <summary>
        /// 门名名称
        /// </summary>
        [DataMember]
        public string store_name { get; set; }
        /// <summary>
        /// 优惠商品明细
        /// </summary>
        [DataMember]
        public string discount_goods_detail { get; set; }

    }
}
