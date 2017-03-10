using LsPay.Service.Wcf.Contract;
using LsPay.Service.Pays.WxPay;
using LsPay.Service.Wcf.Model.WxPay;
using LsPay.Service.Wcf.Model.WxPay.micropay;
using LsPay.Service.Wcf.Model.WxPay.response;
using LsPay.Service.Wcf.ServiceValidate.Attributes;


namespace LsPay.Service.Wcf.Service
{
    /// <summary>
    /// 微信支付服务
    /// </summary>
    [ServiceAuthorizationBehavior]
    public class WxPayService : IWxPayService
    {
        public UnifiedOrderResponseModel UnifiedOrder(UnifiedOrderModel requestModel)
        {
            return WxPayUtil.UnifiedOrder(requestModel);
        }
        public MicropayResponseModel Micropay(MicropayModel micropayModel)
        {
            return WxPayUtil.Micropay(micropayModel);
        }
        public OrderQueryResponseModel OrderQuery(OrderQueryModel queryModel)
        {
            return WxPayUtil.OrderQuery(queryModel);
        }
        public CloseOrderResponseModel CloseOrder(CloseOrderModel queryModel)
        {
            return WxPayUtil.CloseOrder(queryModel);
        }
        public RefundResponseModel Refund(RefundModel requestModel)
        {
            return WxPayUtil.Refund(requestModel);
        }
    }
}
