using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class TradePreCreateResponse
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 响应描述
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 二维码串
        /// </summary>
        public string qr_code { get; set; }
    }
}
