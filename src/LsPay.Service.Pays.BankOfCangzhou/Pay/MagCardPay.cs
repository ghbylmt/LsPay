/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：磁条卡支付类
 * 文件：MagCardPay.cs
 * 类名：MagCardPay
 * 命名空间：LsPay.Service.Pays.BankOfCangzhou.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/

using LsPay.Service.Interface;
using LsPay.Service.ISO8583;
using LsPay.Service.Wcf.Model;

namespace LsPay.Service.Pays.BankOfCangzhou.Pay
{
    /// <summary>
    /// 磁条卡支付类
    /// </summary>
    public class MagCardPay : CreditCardPay,IPay
    {
        public PayResponseModel Pay(byte[] preMsg, string mac)
        {
            return Send(preMsg,mac);
        }

        public PayResponseModel CancelPay(byte[] preMsg, string mac)
        {
            return Send(preMsg, mac);
        }

        public PayResponseModel Query(byte[] preMsg, string mac)
        {
            Iso8583 Result = new Iso8583();
            //Send(preMsg,mac,out Result);
            return Send(preMsg, mac, out Result);
        }
    }
}
