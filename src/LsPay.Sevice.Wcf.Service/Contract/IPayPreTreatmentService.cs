using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Model.Card;
using System.ServiceModel;

namespace LsPay.Service.Wcf.ServicePreTreatment
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPayPreTreatmentService”。
    [ServiceContract]
    public interface IPayPreTreatmentService
    {
        [OperationContract]
        byte[] Query(VisualSelfServiceEquipment equipment);

        [OperationContract]
        byte[] Pay(VisualSelfServiceEquipment equipment);

        [OperationContract]
        byte[] CancelPay(CreditCard creditCard,byte[] preMsg);
    }
}
