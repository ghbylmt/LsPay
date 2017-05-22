using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class TradePayBaseDto : BaseDto
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int merchant_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 终端号
        /// </summary>
        public string terminal_id { get; set; }
    }
}
