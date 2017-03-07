using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Util.Log
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LogModule
    {
        /// <summary>
        /// 银联卡支付
        /// </summary>
        public const string UNIONPAY = "银联卡支付";
        /// <summary>
        /// 支付宝支付
        /// </summary>
        public const string ALIPAY = "支付宝支付";
        /// <summary>
        /// 微信支付
        /// </summary>
        public const string WXPAY = "微信支付";
    }
}
