using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class RefundDto : TradePayBaseDto
    {
        public string refund_amount { get; set; }
        public string sign { get; set; }
    }
}
