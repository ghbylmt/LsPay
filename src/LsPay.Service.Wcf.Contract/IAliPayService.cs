using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Service.Wcf.Model.Alipay.response;

namespace LsPay.Service.Wcf.Contract
{
    [ServiceContract]
    public interface IAliPayService
    {
        [OperationContract]
        PrecreateResponseModel PreCreate(PrecreateModel precreateModel);

        [OperationContract]
        TradepayResponseModel TradePay(TradepayModel tradepayModel);

        [OperationContract]
        QueryResponseModel Query(QueryModel queryModel);

        [OperationContract]
        CancelResponseModel Cancel(CancelModel requestModel);

        [OperationContract]
        RefundResponseModel Refund(RefundModel requestModel);
    }
}
