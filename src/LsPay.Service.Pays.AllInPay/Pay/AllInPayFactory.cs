/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：银联支付工厂
 * 文件：ChinaUnionPayFactory.cs
 * 类名：ChinaUnionPayFactory
 * 命名空间：LsPay.Service.Pays.AllInPay.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Interface;
using LsPay.Service.Wcf.Model.Card;
using System;

namespace LsPay.Service.Pays.AllInPay.Pay
{
    /// <summary>
    /// 通联支付工厂
    /// </summary>
    public class AllInPayFactory : IPayFactory
    {
        /// <summary>
        /// 获取支付对象
        /// </summary>
        /// <param name="creditCard">银行卡</param>
        /// <param name="creditCard">加密设备</param>
        /// <returns></returns>
        public IPay GetPayObj(CreditCard creditCard)
        {
            Type type = creditCard.GetType();
            IPay payObj = null;
            if(type.Name == "ICCard")
                payObj = new ICCardPay();
            else if(type.Name == "MagCard")
                payObj = new MagCardPay();
            return payObj;
        }
    }
}
