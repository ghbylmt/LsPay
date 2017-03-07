using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using LsPay.Service.Wcf.Model.Alipay.response;
using Newtonsoft.Json;
using System.Collections.Generic;
using LsPay.Service.Pays.AliPay.Core;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Service.Util.Log;

namespace LsPay.Service.Pays.AliPay
{
    /// <summary>
    /// 支付宝当面付统一操作类
    /// </summary>
    public class F2FPayUtil
    {
        private static readonly IAopClient _client = new DefaultAopClient(
            Config.serverUrl,
            Config.appId,
            Config.merchant_private_key,
            "",
            Config.version,
            Config.sign_type,
            Config.alipay_public_key,
            Config.charset);

        /// <summary>
        /// 预下单请求
        /// </summary>
        /// <returns></returns>
        public static PrecreateResponseModel Prepay(PrecreateModel precreateModel)
        {
            AlipayTradePrecreateRequest payRequst = new AlipayTradePrecreateRequest();
            if (precreateModel.extend_params == null) precreateModel.extend_params = new ExtendParams ();
            precreateModel.extend_params.sys_service_provider_id = Config.providerid;
            payRequst.BizContent = JsonConvert.SerializeObject(precreateModel);
            LogUtil.WriteAlipayLog("预下单请求", "请求参数", payRequst.BizContent);
            //需要异步通知的时候，需要是指接收异步通知的地址。
            payRequst.SetNotifyUrl("http://10.5.21.14/notify_url.aspx");
            Dictionary<string, string> paramsDict = (Dictionary<string, string>)payRequst.GetParameters();
            AlipayTradePrecreateResponse payResponse = _client.Execute(payRequst);
            LogUtil.WriteAlipayLog("预下单响应", "响应原始参数", payResponse.Body);
            return new PrecreateResponseModel
            {
                code = payResponse.Code,
                msg = payResponse.Msg,
                subcode = payResponse.SubCode,
                submsg = payResponse.SubMsg,
                out_trade_no = payResponse.OutTradeNo,
                qr_code = payResponse.QrCode
            };
        }

        /// <summary>
        /// 条码支付
        /// </summary>
        /// <param name="tradepayModel">请求内容</param>
        /// <returns></returns>
        public static TradepayResponseModel TradePay(TradepayModel tradepayModel)
        {
            AlipayTradePayRequest payRequst = new AlipayTradePayRequest();
            payRequst.BizContent = JsonConvert.SerializeObject(tradepayModel);
            LogUtil.WriteAlipayLog("条码支付请求", "请求参数", payRequst.BizContent);
            Dictionary<string, string> paramsDict = (Dictionary<string, string>)payRequst.GetParameters();
            AlipayTradePayResponse payResponse = _client.Execute(payRequst);
            LogUtil.WriteAlipayLog("条码支付响应", "响应参数", payResponse.Body);
            return new TradepayResponseModel
            {
                msg = payResponse.Msg,
                code = payResponse.Code,
                subcode = payResponse.SubCode,
                submsg = payResponse.SubMsg,
                buyer_logon_id = payResponse.BuyerLogonId,
                store_name = payResponse.StoreName,
                buyer_pay_amount = payResponse.BuyerPayAmount,
                buyer_user_id = payResponse.BuyerUserId,
                gmt_payment = payResponse.GmtPayment,
                out_trade_no = payResponse.OutTradeNo,
                point_amount = payResponse.PointAmount,
                invoice_amount = payResponse.InvoiceAmount,
                total_amount = payResponse.TotalAmount,
                trade_no = payResponse.TradeNo,
                receipt_amount = payResponse.ReceiptAmount
            };
        }


        /// <summary>
        /// 订单撤销
        /// </summary>
        /// <param name="queryRequset">请求内容</param>
        /// <returns></returns>
        public static CancelResponseModel Cancel(CancelModel requestModel)
        {
            AlipayTradeCancelRequest cancelRequst = new AlipayTradeCancelRequest();
            cancelRequst.BizContent = JsonConvert.SerializeObject(requestModel);
            LogUtil.WriteAlipayLog("订单撤销请求", "请求参数", cancelRequst.BizContent);
            Dictionary<string, string> paramsDict = (Dictionary<string, string>)cancelRequst.GetParameters();
            AlipayTradeCancelResponse cancelResponse = _client.Execute(cancelRequst);
            LogUtil.WriteAlipayLog("撤销订单响应", "响应参数", cancelResponse.Body);
            return new CancelResponseModel
            {
                msg = cancelResponse.Msg,
                code = cancelResponse.Code,
                subcode = cancelResponse.SubCode,
                submsg = cancelResponse.SubMsg,
                action = cancelResponse.Action,
                out_trade_no = cancelResponse.OutTradeNo,
                retry_flag = cancelResponse.RetryFlag,
                trade_no = cancelResponse.TradeNo
            };
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="requestModel">请求内容</param>
        /// <returns></returns>
        public static QueryResponseModel Query(QueryModel requestModel)
        {
            AlipayTradeQueryRequest queryRequset = new AlipayTradeQueryRequest();
            queryRequset.BizContent = JsonConvert.SerializeObject(requestModel);
            LogUtil.WriteAlipayLog("查询订单请求", "请求参数", queryRequset.BizContent);
            Dictionary<string, string> paramsDict = (Dictionary<string, string>)queryRequset.GetParameters();
            AlipayTradeQueryResponse queryResponse = _client.Execute(queryRequset);
            LogUtil.WriteAlipayLog("查询订单响应", "响应参数", queryResponse.Body);
            return new QueryResponseModel
            {
                msg = queryResponse.Msg,
                code = queryResponse.Code,
                subcode = queryResponse.SubCode,
                submsg = queryResponse.SubMsg,
                trade_no = queryResponse.TradeNo,
                invoice_amount = queryResponse.InvoiceAmount,
                point_amount = queryResponse.PointAmount,
                receipt_amount = queryResponse.ReceiptAmount,
                trade_status = queryResponse.TradeStatus,
                buyer_logon_id = queryResponse.BuyerLogonId,
                buyer_pay_amount = queryResponse.BuyerPayAmount,
                out_trade_no = queryResponse.OutTradeNo,
                buyer_user_id = queryResponse.BuyerUserId,
                total_amount = queryResponse.TotalAmount
            };
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="requestModel">请求内容</param>
        /// <returns></returns>
        public static RefundResponseModel Refund(RefundModel requestModel)
        {
            AlipayTradeRefundRequest queryRequset = new AlipayTradeRefundRequest();
            queryRequset.BizContent = JsonConvert.SerializeObject(requestModel);
            LogUtil.WriteAlipayLog("申请退款请求", "请求参数", queryRequset.BizContent);
            Dictionary<string, string> paramsDict = (Dictionary<string, string>)queryRequset.GetParameters();
            AlipayTradeRefundResponse refundResponse = _client.Execute(queryRequset);
            LogUtil.WriteAlipayLog("申请退款响应", "响应参数", refundResponse.Body);
            return new RefundResponseModel
            {
                msg = refundResponse.Msg,
                code = refundResponse.Code,
                subcode = refundResponse.SubCode,
                submsg = refundResponse.SubMsg,
                trade_no = refundResponse.TradeNo,
                send_back_fee = refundResponse.SendBackFee,
                store_name = refundResponse.StoreName,
                fund_change = refundResponse.FundChange,
                buyer_logon_id = refundResponse.BuyerLogonId,
                amount =refundResponse.RefundDetailItemList==null || refundResponse.RefundDetailItemList.Count == 0 ? "0.0" : refundResponse.RefundDetailItemList[0].Amount,
                fund_channel = refundResponse.RefundDetailItemList == null||refundResponse.RefundDetailItemList.Count == 0 ? "": refundResponse.RefundDetailItemList[0].FundChannel,
                out_trade_no = refundResponse.OutTradeNo,
                buyer_user_id = refundResponse.BuyerUserId,
                gmt_refund_pay = refundResponse.GmtRefundPay,
                refund_fee = refundResponse.RefundFee
            };
        }
    }
}
