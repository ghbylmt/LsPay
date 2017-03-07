using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 支付工厂接口
    /// </summary>
    public interface IPayFactory
    {
        /// <summary>
        /// 获取支付对象
        /// </summary>
        /// <param name="creditCard">银行卡</param>
        /// <returns></returns>
        IPay GetPayObj(CreditCard creditCard);
    }
}
