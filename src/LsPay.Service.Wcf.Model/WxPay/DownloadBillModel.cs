using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 下载对账单接口
    /// </summary>
    [DataContract]
    public class DownloadBillModel
    {
        /// <summary>
        /// 对账单日期
        /// 下载对账单的日期，格式：20140603，日期长度最多8位
        /// </summary>
        [DataMember]
        public string bill_date { get; set; }
        /// <summary>
        /// 账单类型
        /// ALL，返回当日所有订单信息，默认值;SUCCESS，返回当日成功支付的订单;
        /// REFUND，返回当日退款订单;REVOKED，已撤销的订单,账单类型长度最多8位
        /// </summary>
        [DataMember]
        public string bill_type { get; set; }
    }
}
