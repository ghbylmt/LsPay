using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    /// <summary>
    /// 结果码
    /// </summary>
    public class TradepayResultCode
    {
        /// <summary>
        /// 业务处理成功
        /// </summary>
        public const string TRADE_SUCCESS = "10000";
        /// <summary>
        /// 业务处理失败
        /// 具体失败原因参见接口返回的错误码
        /// </summary>
        public const string TRADE_FAILURE = "40004";
        /// <summary>
        /// 业务处理中
        /// 该结果码只有在条码支付请求API时才返回，代表付款还在进行中，需要调用查询接口查询最终的支付结果
        /// </summary>
        public const string PROCESSING = "10003";
        /// <summary>
        /// 业务出现未知错误或者系统异常
        /// 如果支付接口返回，需要调用查询接口确认订单状态或者发起撤销
        /// </summary>
        public const string EXCEPTION = "20000";
    }
}
