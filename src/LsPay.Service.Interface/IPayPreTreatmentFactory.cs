using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 支付预处理工厂接口
    /// </summary>
    public interface IPayPreTreatmentFactory
    {
        /// <summary>
        /// 获取支付预处理对象
        /// </summary>
        /// <param name="creditCard">银行卡</param>
        /// <returns></returns>
        IPayPreTeatment GetPayPreObj(CreditCard creditCard);
    }
}
