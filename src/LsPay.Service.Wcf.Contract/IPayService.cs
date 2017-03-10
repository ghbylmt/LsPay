using System.ServiceModel;
using LsPay.Service.Wcf.Model;

namespace LsPay.Service.Wcf.Contract
{
    [ServiceContract]
    public interface IPayService
    {
        [OperationContract]
        PayResponseModel Pay(byte[] preMsg, string mac);
        [OperationContract]
        PayResponseModel CancelPay(byte[] preMsg, string mac);
        [OperationContract]
        PayResponseModel Query(byte[] preMsg, string mac);
        
        /// <summary>
        /// 虚拟自助设备信息
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        [OperationContract]
        SignResponseModel Sign(VisualSelfServiceEquipment equipment);
    }
}
