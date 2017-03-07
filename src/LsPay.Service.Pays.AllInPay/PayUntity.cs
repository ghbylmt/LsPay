/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：银行相关公共操作类
 * 文件：BankUtility.cs
 * 类名：BankUtility
 * 命名空间：LsPay.Service.Pays.AllInPay.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Interface;
using LsPay.Service.ISO8583;
using LsPay.Service.Util;
using LsPay.Service.Wcf.Model;
using System;


namespace LsPay.Service.Pays.AllInPay.Pay
{
    /// <summary>
    /// 银行相关公共操作类
    /// </summary>
    public class PayUntity : IPayUtility
    {
        /// <summary>
        /// 签到获取密钥
        /// </summary>
        /// <returns></returns>
        public SignResponseModel Sign(VisualSelfServiceEquipment equipment)
        {
            Bitmap map = new Bitmap();
            map[11] = map[41] = map[42] = map[60] = map[63] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[11].Content = Settings.SysTraceNum;
            iso8583[41].Content = equipment.TerminalNo;
            iso8583[42].Content = Settings.MerchantCode;
            iso8583[60].FieldLenIndicator.Content = "011";
            iso8583[60].Content = "00003010003";
            iso8583[63].FieldLenIndicator.Content = "003";
            iso8583[63].Content = "001";
            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0800";
            Iso8583 Res_iso8583 = new Iso8583();
            Message Res_Msg = new Message(Res_iso8583);
            Res_Msg.Unpack(msg.Send(Settings.BankIp, Convert.ToInt32(Settings.BankPort)));
            SignResponseModel responseModel = new SignResponseModel() { ResponseCode = Res_iso8583[39].Content};
            if (responseModel.ResponseCode == "00") responseModel.Content = BitConverter.ToString(Res_iso8583[62].Pack()).Replace("-", "");
            return responseModel;

        }
    }
}
