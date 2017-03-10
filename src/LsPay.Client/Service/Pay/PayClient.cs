using System.ServiceModel;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Contract;

namespace LsPay.Client.Service.Pay
{
    public class PayClient : ClientBase<IPayService>,IPayService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="edpAddr"></param>
        public PayClient(System.ServiceModel.Channels.Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr) { }

        public PayResponseModel Pay(byte[] preMsg, string mac)
        {
            return base.Channel.Pay(preMsg,mac);
        }

        public PayResponseModel CancelPay(byte[] preMsg, string mac )
        {
            return base.Channel.CancelPay(preMsg, mac);
        }

        public PayResponseModel Query(byte[] preMsg, string mac)
        {
            return base.Channel.Query(preMsg,mac);
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public SignResponseModel Sign(VisualSelfServiceEquipment equipment)
        {
            return Channel.Sign(equipment);
        }
    }
}
