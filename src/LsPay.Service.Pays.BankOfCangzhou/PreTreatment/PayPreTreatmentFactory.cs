using System;
using LsPay.Service.Interface;
using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Service.Pays.BankOfCangzhou.Pay.PreTreatment
{
    public class PayPreTreatmentFactory : IPayPreTreatmentFactory
    {
        public IPayPreTeatment GetPayPreObj(CreditCard creditCard)
        {
            Type type = creditCard.GetType();
            IPayPreTeatment payPreObj = null;
            if (type.Name == "ICCard")
                payPreObj = new ICCardPayPreTreatment();
            else if (type.Name == "MagCard")
                payPreObj = new MagCardPayPreTreatment();
            return payPreObj;
        }
    }
}
