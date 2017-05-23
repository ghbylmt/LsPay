using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response;
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
        public static ActiveResponse ActiveDevice(ActiveDeviceDto request)
        {
            var response = WebUtils.HttpPost<ActiveDeviceDto, ActiveResponse>("http://pay.xuanlife.com.cn/ManualActiveDevice", request);
            return response;
        }
        /// <summary>
        /// 收银员操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CasherOpersResponse CasherOper(CasherOpersDto request)
        {
            request.sign = EncryptUtil.GetSign(request);
            var response = WebUtils.HttpPost<CasherOpersDto, CasherOpersResponse>("http://pay.xuanlife.com.cn/casherOpers", request);
            return response;
        }
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="request"></param>
        public static TradePreCreateResponse Precreate(TradePreCreateDto request)
        {
            request.Sign = EncryptUtil.GetSign(request);
            var response = WebUtils.HttpPost<TradePreCreateDto, TradePreCreateResponse>("http://118.178.35.56/tradeprecreate",request);
            return response;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        public static QueryResponse Query(QueryDto request)
        {
            var response = WebUtils.HttpPost<QueryDto, QueryResponse>("http://118.178.35.56/query", request);
            return response;
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="request"></param>
        public static RefundResponse Refund(RefundDto request)
        {
            request.sign = EncryptUtil.GetSign(request);
            var response = WebUtils.HttpPost<RefundDto, RefundResponse>("http://118.178.35.56/refund", request);
            return response;
        }

    }
}
