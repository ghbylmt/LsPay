using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LsPay.Service.Wcf.ServicePay
{
    [ServiceContract]
    public interface IPayService
    {
        [OperationContract]
        string Pay(byte[] preMsg, string mac);
        [OperationContract]
        string CancelPay(byte[] preMsg, string mac);
        [OperationContract]
        string Query(byte[] preMsg, string mac);
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string Sign();
    }
}
