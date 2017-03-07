using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client
{
    /// <summary>
    /// 刷卡支付类型
    /// </summary>
    public enum CreditCardPayType
    {
        /// <summary>
        /// 通联支付
        /// </summary>
        AllInPay = 0,
        /// <summary>
        /// 银联支付
        /// </summary>
        ChinaUnionPay =1
    }
}
