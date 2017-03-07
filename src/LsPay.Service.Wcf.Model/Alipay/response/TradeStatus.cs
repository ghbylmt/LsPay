using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    /// <summary>
    /// 交易状态
    /// </summary>
    public class TradeStatus
    {
        /// <summary>
        /// 交易创建，等待买家付款
        /// </summary>
        public const string WAIT_BUYER_PAY = "WAIT_BUYER_PAY";
        /// <summary>
        /// 未付款交易超时关闭，或支付完成后全额退款
        /// </summary>
        public const string TRADE_CLOSED = "TRADE_CLOSED";
        /// <summary>
        /// 交易支付成功
        /// </summary>
        public const string TRADE_SUCCESS = "TRADE_SUCCESS";
        /// <summary>
        /// 交易结束，不可退款
        /// </summary>

        public const string TRADE_FINISHED = "TRADE_FINISHED";

    }
}
