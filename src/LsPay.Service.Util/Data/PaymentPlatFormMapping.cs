using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Util.Data
{
    /// <summary>
    /// 支付平台
    /// </summary>
    public class PaymentPlatFormMapping
    {
        /// <summary>
        /// 银联支付
        /// </summary>
        public const string CHINA_UNION_PAY = "ChinaUnionPay";
        /// <summary>
        /// 通联支付
        /// </summary>
        public const string ALL_IN_PAY = "AllInPay";
        /// <summary>
        /// 沧州银行
        /// </summary>
        public const string BANK_OF_CANGZHOU = "BankOfCangzhou";
    }
}
