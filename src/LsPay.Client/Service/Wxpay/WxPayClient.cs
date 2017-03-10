using System.ServiceModel;
using LsPay.Service.Wcf.Model.WxPay;
using LsPay.Service.Wcf.Model.WxPay.micropay;
using LsPay.Service.Wcf.Model.WxPay.response;
using LsPay.Service.Wcf.Contract;

namespace LsPay.Client.Service.Wxpay
{
    public class WxPayClient : ClientBase<IWxPayService>, IWxPayService
    {
        public WxPayClient(System.ServiceModel.Channels.Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr) { }

        public UnifiedOrderResponseModel UnifiedOrder(UnifiedOrderModel requestModel)
        {
            return base.Channel.UnifiedOrder(requestModel);
        }

        public MicropayResponseModel Micropay(MicropayModel requestModel)
        {
            return base.Channel.Micropay(requestModel);
        }

        public OrderQueryResponseModel OrderQuery(OrderQueryModel requestModel)
        {
            return base.Channel.OrderQuery(requestModel);
        }


        public CloseOrderResponseModel CloseOrder(CloseOrderModel requestModel)
        {
            return base.Channel.CloseOrder(requestModel);
        }


        public RefundResponseModel Refund(RefundModel requestModel)
        {
            return base.Channel.Refund(requestModel);
        }
    }
}
