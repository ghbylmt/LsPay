/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：支付预处理接口
 * 文件：IPayPreTeatment.cs
 * 类名：IPayPreTeatment
 * 命名空间：LsPay.Service.Interface
 * 
 * 创建标识：尚春城 2015/07/02  
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 支付预处理接口
    /// </summary>
    public interface IPayPreTeatment
    {
        /// <summary>
        /// 支付预处理
        /// </summary>
        /// <param name="terminalNo">终端号</param>
        /// <param name="PayMoney">支付金额</param>
        /// <param name="pinBlock">加密后密码</param>
        /// <param name="creditCard">银行卡信息</param>
        /// <returns></returns>
        byte[] Pay(string terminalNo, string PayMoney, string pinBlock, CreditCard creditCard);
        /// <summary>
        /// 撤销预处理
        /// </summary>
        /// <param name="preMsg">支付信息</param>
        /// <param name="creditCard">银行卡信息</param>
        /// <param name="posSerialNo">POS中心流水号</param>
        /// <returns></returns>
        byte[] CancelPay(byte[] preMsg, CreditCard creditCard,string posSerialNo);
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="terminalNo">终端号</param>
        /// <param name="pinBlock">加密后密码</param>
        /// <param name="creditCard">银行卡信息</param>
        /// <returns></returns>
        byte[] Query(string terminalNo,string pinBlock, CreditCard creditCard);
    }
}
