/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：[中国银联]芯片卡支付类
 * 文件：ICCardPay.cs
 * 类名：ICCardPay
 * 命名空间：LsPay.Service.Pays.AllInPay.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Interface;
using LsPay.Service.ISO8583;
using LsPay.Service.Wcf.Model;

namespace LsPay.Service.Pays.AllInPay.Pay
{
    /// <summary>
    /// 芯片卡支付类
    /// </summary>
    public class ICCardPay :CreditCardPay,IPay
    {
        
        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="payMoney">消费金额</param>
        /// <param name="pinBlock">加密后密码</param>
        /// <returns></returns>
        public PayResponseModel Pay(byte[] preMsg, string mac)
        {
            return Send(preMsg,mac);
        }
        /// <summary>
        /// 冲正
        /// </summary>
        /// <param name="pinBlock"></param>
        /// <returns></returns>
        public PayResponseModel CancelPay(byte[] preMsg, string mac)
        {
            return Send(preMsg, mac);
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <returns></returns>
        public PayResponseModel Query(byte[] preMsg, string mac)
        {
            Iso8583 Result = new Iso8583();
            return Send(preMsg, mac, out Result);
        }

    }
}
