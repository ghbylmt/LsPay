using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Model.WxPay;
using LsPay.Service.Wcf.Model.WxPay.micropay;
using LsPay.Service.Wcf.Model.WxPay.response;

namespace LsPay.Service.Wcf.Contract
{
    /// <summary>
    /// 微信支付服务接口定义
    /// </summary>
    [ServiceContract]
    public interface IWxPayService
    {
        [OperationContract]
        UnifiedOrderResponseModel UnifiedOrder(UnifiedOrderModel requestModel);
        [OperationContract]
        MicropayResponseModel Micropay(MicropayModel requestModel);

        [OperationContract]
        OrderQueryResponseModel OrderQuery(OrderQueryModel requestModel);

        [OperationContract]
        CloseOrderResponseModel CloseOrder(CloseOrderModel requestModel);

        [OperationContract]
        RefundResponseModel Refund(RefundModel requestModel);

    }
}
