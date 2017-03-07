using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    public class RefundChannel
    {
        /// <summary>
        /// 原路退款
        /// </summary>
        public const string ORIGINAL = "ORIGINAL";
        /// <summary>
        /// 退回到余额
        /// </summary>
        public const string BALANCE = "BALANCE";
    }
}
