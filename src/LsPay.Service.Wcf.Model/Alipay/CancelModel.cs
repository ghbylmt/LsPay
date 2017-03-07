using System.Runtime.Serialization;

namespace LsPay.Service.Wcf.Model.Alipay
{
    /// <summary>
    /// 撤销订单数据传输类
    /// </summary>
    [DataContract]
    public class CancelModel
    {
        /// <summary>
        /// 商户订单号
        /// String(64)
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
    }
}
