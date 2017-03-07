using LsPay.Service.Interface;
using LsPay.Service.Wcf.Model.Card;
using System;

namespace LsPay.Service.Pays.AllInPay.Pay.PreTreatment
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
