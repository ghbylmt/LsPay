using System;
using WxPayAPI;
using LsPay.Service.Wcf.Model.WxPay.response;
using LsPay.Service.Wcf.Model.WxPay.micropay;
using LsPay.Service.Util.Log;
using LsPay.Service.Wcf.Model.WxPay;

namespace LsPay.Service.Pays.WxPay
{
    public class WxPayUtil
    {
        /// <summary>
        /// 刷卡支付
        /// </summary>
        public static MicropayResponseModel Micropay(MicropayModel requestModel)
        {
            WxPayData data = new WxPayData();
            data.SetValue("auth_code", requestModel.auth_code);//授权码
            data.SetValue("body", requestModel.body);//商品描述
            data.SetValue("total_fee", requestModel.total_fee);//总金额
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//产生随机的商户订单号
            data.SetValue("device_info", requestModel.device_info);
            LogUtil.WriteWxpayLog("刷卡支付请求", "请求参数", data.ToJson());
            WxPayData result = WxPayApi.Micropay(data, 10); //提交被扫支付，接收返回结果
            //如果提交被扫支付接口调用失败，则抛异常
            if (!result.IsSet("return_code") || result.GetValue("return_code").ToString() == ReturnCode.FAIL)
		    {
                string returnMsg = result.IsSet("return_msg") ? result.GetValue("return_msg").ToString() : "";
                //Log.Error("MicroPay", "Micropay API interface call failure, result : " + result.ToXml());
                throw new WxPayException("微信接口调用出错 : " + returnMsg);
		    }
            LogUtil.WriteWxpayLog("刷卡支付响应", "响应参数", result.ToJson());
            //签名验证
            result.CheckSign();
            MicropayResponseModel responseModel = LitJson.JsonMapper.ToObject<MicropayResponseModel>(result.ToJson());
            responseModel.out_trade_no = responseModel.out_trade_no ?? data.GetValue("out_trade_no").ToString();
            //if (responseModel.return_code == ReturnCode.FAIL) throw new WxPayException(responseModel.return_msg);
            return responseModel;
        }
        
        /// <summary>
        /// 统一下单接口
        /// </summary>
        /// <param name="unifiedorderModel">请求参数</param>
        /// <returns></returns>
        public static UnifiedOrderResponseModel UnifiedOrder(UnifiedOrderModel unifiedorderModel)
        {
            UnifiedOrderResponseModel response = new UnifiedOrderResponseModel(); 
            WxPayData data = new WxPayData();
            data.SetValue("body", unifiedorderModel.body);//商品描述
            data.SetValue("attach", unifiedorderModel.attach);//附加数据
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//随机字符串
            data.SetValue("total_fee",unifiedorderModel.total_fee);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", unifiedorderModel.goods_tag);//商品标记
            data.SetValue("trade_type", unifiedorderModel.trade_type);//交易类型
            data.SetValue("product_id", unifiedorderModel.product_id);//商品ID
            LogUtil.WriteWxpayLog("统一下单请求", "请求参数", data.ToJson());
            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            string resultStr = result.ToJson();
            LogUtil.WriteWxpayLog("统一下单响应", "响应参数", resultStr);
            response = LitJson.JsonMapper.ToObject<UnifiedOrderResponseModel>(resultStr);
            response.out_trade_no = response.out_trade_no ?? data.GetValue("out_trade_no").ToString();
            return response;
            //Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="requestModel">请求实体</param>
        /// <returns></returns>
        public static OrderQueryResponseModel OrderQuery(OrderQueryModel requestModel)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(requestModel.transaction_id))
            {//如果微信订单号存在，则以微信订单号为准
                data.SetValue("transaction_id", requestModel.transaction_id);
            }
            else
            {//微信订单号不存在，才根据商户订单号去查单
                data.SetValue("out_trade_no", requestModel.out_trade_no);
            }
            LogUtil.WriteWxpayLog("订单查询请求", "请求参数", data.ToJson());
            //提交订单查询请求给API，接收返回数据
            WxPayData result = WxPayApi.OrderQuery(data);
            OrderQueryResponseModel response = LitJson.JsonMapper.ToObject<OrderQueryResponseModel>(result.ToJson());
            LogUtil.WriteWxpayLog("订单查询响应", "响应参数", result.ToJson());
            //Log.Info("OrderQuery", "OrderQuery process complete, result : " + result.ToXml());
            return response;
        }
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="requestModel">请求对象</param>
        /// <returns></returns>
        public static CloseOrderResponseModel CloseOrder(CloseOrderModel requestModel)
        {
            WxPayData data = new WxPayData();
            data.SetValue("out_trade_no", requestModel.out_trade_no);
            LogUtil.WriteWxpayLog("关闭订单请求", "请求参数", data.ToJson());
            //提交订单查询请求给API，接收返回数据
            WxPayData result = WxPayApi.CloseOrder(data);
            LogUtil.WriteWxpayLog("关闭订单响应", "响应参数", result.ToJson());
            CloseOrderResponseModel response = LitJson.JsonMapper.ToObject<CloseOrderResponseModel>(result.ToJson());
            //Log.Info("OrderQuery", "OrderQuery process complete, result : " + result.ToXml());
            return response;
        }

        /// <summary>
        /// 微信申请退款接口
        /// </summary>
        /// <param name="requestModel">请求参数</param>
        /// <returns></returns>
        public static RefundResponseModel Refund(RefundModel requestModel)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(requestModel.transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", requestModel.transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", requestModel.out_trade_no);
            }
            data.SetValue("total_fee", requestModel.total_fee);//订单总金额
            data.SetValue("refund_fee", requestModel.refund_fee);//退款金额
            data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号
            LogUtil.WriteWxpayLog("申请退款请求", "请求参数", data.ToJson());
            WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据
            LogUtil.WriteWxpayLog("申请退款响应", "响应参数", result.ToJson());
            RefundResponseModel response = LitJson.JsonMapper.ToObject<RefundResponseModel>(result.ToJson());
            //Log.Info("OrderQuery", "OrderQuery process complete, result : " + result.ToXml());
            return response;
        }
    }
}
