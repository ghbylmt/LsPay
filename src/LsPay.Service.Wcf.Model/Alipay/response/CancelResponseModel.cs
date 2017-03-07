using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    /// <summary>
    /// 撤销响应数据传输类
    /// </summary>
    [DataContract]
    public class CancelResponseModel : BaseResponseModel
    {
        /// <summary>
        /// 商户订单号
        /// String(64)
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 是否可重试标志
        /// 撤销已成功，无需重试
        /// </summary>
        [DataMember]
        public string retry_flag { get; set; }
        /// <summary>
        /// 支付宝交易号
        /// String(64)
        /// 支付宝交易凭证号
        /// </summary>
        [DataMember]
        public string trade_no { get; set; }
        /// <summary>
        /// 撤销执行的动作
        /// String(10)
        /// 撤销执行的动作。
        /// close：关闭交易，无退款；
        /// refund：退款。
        /// </summary>
        [DataMember]
        public string action { get; set; }

    }
}
