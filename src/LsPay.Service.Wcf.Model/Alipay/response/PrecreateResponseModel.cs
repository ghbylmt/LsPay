using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    [DataContract]
    public class PrecreateResponseModel : BaseResponseModel
    {
        /// <summary>
        /// 商户订单号
        /// 原支付请求的商户订单号
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string qr_code { get; set; }
    }
}
