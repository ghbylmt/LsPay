using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 关闭订单接口
    /// </summary>
    [DataContract]
    public class CloseOrderModel
    {
        /// <summary>
        /// 商户订单号
        /// 商户系统内部的订单号,订单号长度最多32位
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
    }
}
