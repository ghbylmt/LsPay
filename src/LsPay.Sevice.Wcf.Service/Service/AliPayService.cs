using LsPay.Service.Wcf.Contract;
using LsPay.Service.Pays.AliPay;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Service.Wcf.Model.Alipay.response;
using LsPay.Service.Wcf.ServiceValidate.Attributes;

namespace LsPay.Service.Wcf.Service
{
    /// <summary>
    /// 支付服务
    /// </summary>
    [ServiceAuthorizationBehavior]
    public class AliPayService : IAliPayService
    {

        public PrecreateResponseModel PreCreate(PrecreateModel precreateModel)
        {
            return F2FPayUtil.Prepay(precreateModel);
        }
        public TradepayResponseModel TradePay(TradepayModel tradepayModel)
        {
            return F2FPayUtil.TradePay(tradepayModel);
        }
        
        public QueryResponseModel Query(QueryModel queryModel)
        {
            return F2FPayUtil.Query(queryModel);
        }


        public CancelResponseModel Cancel(CancelModel requestModel)
        {
            return F2FPayUtil.Cancel(requestModel);
        }

        public RefundResponseModel Refund(RefundModel requestModel)
        {
            return F2FPayUtil.Refund(requestModel);
        }
    }
}
