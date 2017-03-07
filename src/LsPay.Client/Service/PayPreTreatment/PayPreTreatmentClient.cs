using LsPay.Service.Contract;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Model.Card;
using System.ServiceModel;

namespace LsPay.Client.Service.PayPreTreatment
{
    /// <summary>
    /// 支付预处理代理类
    /// </summary>
    public class PayPreTreatmentClient : ClientBase<IPayPreTreatmentService>, IPayPreTreatmentService
    {
        public PayPreTreatmentClient(System.ServiceModel.Channels.Binding binding, EndpointAddress edpAddr)  
        : base(binding, edpAddr) { }

        public byte[] Query(VisualSelfServiceEquipment equipment)
        {
            return base.Channel.Query(equipment);
        }

        public byte[] Pay(VisualSelfServiceEquipment equipment)
        {
            return base.Channel.Pay(equipment);
        }

        public byte[] CancelPay(CreditCard creditCard, byte[] preMsg,string posSerialNo)
        {
            return base.Channel.CancelPay(creditCard,preMsg,posSerialNo);
        }
    }
}
