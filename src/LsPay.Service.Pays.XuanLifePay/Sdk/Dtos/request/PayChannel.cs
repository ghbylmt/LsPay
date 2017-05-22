using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    /// <summary>
    /// 支付渠道
    /// </summary>
    public enum PayChannel
    {
        Alipay = 10,
        Wxpay = 20,
        BaiduPay = 30,
        YiPay = 40,
        JdPay = 50,
        QQPay = 60,
    }
}
