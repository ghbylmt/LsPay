using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using LsPay.Service.Pays.XuanLifePay.Sdk.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay
{
    /// <summary>
    /// 支付操作类
    /// </summary>
    public static class PayUtil
    {
        public static void Precreate(TradePreCreateDto request)
        {
            request.Sign = EncryptUtil.GetSign(request);
            WebUtils.HttpPost("http://118.178.35.56/tradeprecreate", JsonConvert.SerializeObject(request));
        }
    }
}
