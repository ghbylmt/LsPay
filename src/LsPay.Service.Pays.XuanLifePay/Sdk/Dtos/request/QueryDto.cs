﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class QueryDto:TradePayBaseDto
    {
        public string sign { get; set; }
    }
}
