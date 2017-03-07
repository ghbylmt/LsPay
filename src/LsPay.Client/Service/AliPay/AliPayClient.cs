using LsPay.Service.Contract;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Service.Wcf.Model.Alipay.response;
using System.ServiceModel;

namespace LsPay.Client.Service.AliPay
{
    public class AliPayClient : ClientBase<IAliPayService>, IAliPayService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="edpAddr"></param>
        public AliPayClient(System.ServiceModel.Channels.Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr) { }
        public PrecreateResponseModel PreCreate(PrecreateModel precreateModel)
        {
            return base.Channel.PreCreate(precreateModel);
        }

        public TradepayResponseModel TradePay(TradepayModel tradepayModel)
        {
            return base.Channel.TradePay(tradepayModel);
        }

        public QueryResponseModel Query(QueryModel queryModel)
        {
            return base.Channel.Query(queryModel);
        }

        public CancelResponseModel Cancel(CancelModel requestModel)
        {
            return base.Channel.Cancel(requestModel);
        }

        public RefundResponseModel Refund(RefundModel requestModel)
        {
            return base.Channel.Refund(requestModel);
        }
    }
}
