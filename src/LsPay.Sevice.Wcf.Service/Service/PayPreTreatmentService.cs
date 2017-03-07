using System;
using System.ServiceModel;
using LsPay.Service.Contract;
using LsPay.Service.Interface;
using LsPay.Service.Wcf.Model.Card;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Service;

namespace LsPay.Service.Wcf.Service
{
    public class PayPreTreatmentService : IPayPreTreatmentService
    {
        /// <summary>
        /// 支付预处理
        /// </summary>
        /// <param name="equipment">设备信息</param>
        /// <returns></returns>
        [ServiceKnownType(typeof(MagCard))]
        [ServiceKnownType(typeof(ICCard))]
        public byte[] Pay(VisualSelfServiceEquipment equipment)
        {
            if (equipment == null)
                throw new ArgumentException("无效的设备信息");
            IPayPreTeatment payPreObj =
            PaymentPlatFormFactory.GetPayPreTreatmentFactory().GetPayPreObj(equipment.creditCard);
            return payPreObj.Pay(equipment.TerminalNo,equipment.PayMoney,equipment.PinBlock,equipment.creditCard);
        }

        /// <summary>
        /// 查询预处理
        /// </summary>
        /// <param name="equipment">设备信息</param>
        /// <returns></returns>
        public byte[] Query(Model.VisualSelfServiceEquipment equipment)
        {
            if (equipment == null)
                throw new ArgumentException("无效的设备信息");
            IPayPreTeatment payPreObj = 
            PaymentPlatFormFactory.GetPayPreTreatmentFactory().GetPayPreObj(equipment.creditCard);
            return payPreObj.Query(equipment.TerminalNo, equipment.PinBlock, equipment.creditCard);
        }

        /// <summary>
        /// 冲正预处理
        /// </summary>
        /// <param name="creditCard">银行卡信息</param>
        /// <param name="preMsg">上次支付的信息</param>
        /// <returns></returns>
        public byte[] CancelPay(CreditCard creditCard, byte[] preMsg, string posSerialNo)
        {
            IPayPreTeatment payPreObj =
            PaymentPlatFormFactory.GetPayPreTreatmentFactory().GetPayPreObj(creditCard);
            return payPreObj.CancelPay(preMsg, creditCard,posSerialNo);
        }
    }
}
