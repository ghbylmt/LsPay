﻿/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：磁条卡支付预处理类
 * 文件：MagCardPayPreTreatment.cs
 * 类名：MagCardPayPreTreatment
 * 命名空间：LsPay.Service.Pays.AllInPay.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.Interface;
using LsPay.Service.Wcf.Model.Card;
using LsPay.Service.Util;
using LsPay.Service.ISO8583;

namespace LsPay.Service.Pays.AllInPay.Pay
{
    /// <summary>
    /// 磁条卡支付预处理类
    /// </summary>
    public class MagCardPayPreTreatment : IPayPreTeatment
    {
        /// <summary>
        /// 支付预处理
        /// </summary>
        /// <param name="payMoney">支付金额</param>
        /// <param name="pinBlock">加密后密码</param>
        /// <param name="creditCard">银行卡信息</param>
        /// <returns></returns>
        public byte[] Pay(string terminalNo,string payMoney, string pinBlock, CreditCard creditCard)
        {
            MagCard Card = creditCard as MagCard;
            if (Card == null)
                throw new ArgumentException("无效的卡片");
            Bitmap map = new Bitmap();
            map[2] = map[3] = map[4] = map[11] = map[22] = map[25] = map[26] = map[35] = map[41] = map[42] = map[49] = map[52] = map[53] = map[60] = map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);

            iso8583[2].FieldLenIndicator.Content = (Card.CardNo.Length/2).ToString();
            iso8583[2].Content = Card.CardNo;
            iso8583[3].Content = "000000";
            iso8583[4].Content = payMoney;

            iso8583[11].Content = Settings.SysTraceNum;
            Settings.SysTraceNum = (Convert.ToInt32(Settings.SysTraceNum) >= 999999 ? 0 : Convert.ToInt32(Settings.SysTraceNum) + 1).ToString().PadLeft(6, '0');

            iso8583[22].Content = "021";
            iso8583[25].Content = "00";
            iso8583[26].Content = "06";
            iso8583[35].FieldLenIndicator.Content = (Card.Msg2.Length/2).ToString();
            iso8583[35].Content = Card.Msg2;
            iso8583[41].Content = terminalNo;
            iso8583[42].Content = Settings.MerchantCode;
            iso8583[49].Content = "156";
            iso8583[52].Content = pinBlock;
            iso8583[53].Content = "26";
            iso8583[60].FieldLenIndicator.Content = "019";
            iso8583[60].Content = "2200301000020000000";
            iso8583[64].Content = "1111111111111111";

            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0200";
            StringBuilder mac = new StringBuilder();
            byte[] subMsg = msg.Pack();
            return subMsg;
        }
        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="preMsg">原始交易</param>
        /// <param name="creditCard">银行卡信息></param>
        /// <returns></returns>
        public byte[] CancelPay(byte[] preMsg, CreditCard creditCard,string posSerialNo)
        {
            Iso8583 Pre_iso8583 = new Iso8583();
            Message Pre_Msg = new Message(Pre_iso8583);
            Pre_Msg.Unpack(preMsg);
            string[] extendData = posSerialNo.Split(',');
            string posserialNo = extendData[0];
            string AuthorizationCode = extendData.Length > 1 ? extendData[1] : null;

            Bitmap map = new Bitmap();
                map[2] = 
                map[3] = 
                map[4] = 
                map[11] = 
                map[22] = 
                map[25] =
                map[26] =
                map[35] = 
                map[37] =
                map[41] = 
                map[42] = 
                map[49] =
                map[52] =
                map[53] =
                map[60] = 
                map[61] =
                map[64] = true;
                if (!string.IsNullOrEmpty(AuthorizationCode)) map[38] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[2].FieldLenIndicator.Content = (Pre_iso8583[2].Content.Length/2).ToString();
            iso8583[2].Content = Pre_iso8583[2].Content;
            iso8583[3].Content = "200000";//Pre_iso8583[3].Content;
            iso8583[4].Content = Pre_iso8583[4].Content;
            iso8583[11].Content = Settings.SysTraceNum;
            iso8583[22].Content =  Pre_iso8583[22].Content;
            iso8583[25].Content = Pre_iso8583[25].Content;
            iso8583[26].Content = Pre_iso8583[26].Content;
            iso8583[35].FieldLenIndicator.Content = (Pre_iso8583[35].Content.Length/2).ToString();
            iso8583[35].Content = Pre_iso8583[35].Content;
            iso8583[37].Content = posserialNo;
            if (!string.IsNullOrEmpty(AuthorizationCode))  iso8583[38].Content = AuthorizationCode;
            iso8583[41].Content = Pre_iso8583[41].Content;
            iso8583[42].Content = Pre_iso8583[42].Content;
            iso8583[49].Content = Pre_iso8583[49].Content;
            iso8583[52].Content = Pre_iso8583[52].Content;
            iso8583[53].Content = Pre_iso8583[53].Content;
            iso8583[60].FieldLenIndicator.Content = "019";
            iso8583[60].Content = "2200301000020000000";////Pre_iso8583[60].Content;
            iso8583[61].FieldLenIndicator.Content = "016";
            iso8583[61].Content = string.Format("{0}{1}{2}", "003010", Pre_iso8583[11].Content, DateTime.Now.ToString("MMdd"));
            iso8583[64].Content = "1111111111111111";
            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0200";
            byte[] subMsg = msg.Pack();
            return subMsg;
        }

        /// <summary>
        /// 冲正预处理
        /// </summary>
        /// <param name="preMsg">原始交易</param>
        /// <param name="creditCard">银行卡信息></param>
        /// <returns></returns>
        public byte[] Correct(byte[] preMsg, CreditCard creditCard)
        {
            Iso8583 Pre_iso8583 = new Iso8583();
            Message Pre_Msg = new Message(Pre_iso8583);
            Pre_Msg.Unpack(preMsg);

            Bitmap map = new Bitmap();
            map[2] = map[3] = map[4] =
                map[11] = map[22] = map[25] = map[35] = map[39] =
                map[41] =
                map[42] = map[49] =// map[52] = map[53] = 
                map[60] =
                map[61] =
                map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[2].FieldLenIndicator.Content = (Pre_iso8583[2].Content.Length / 2).ToString();
            iso8583[2].Content = Pre_iso8583[2].Content;
            iso8583[3].Content = Pre_iso8583[3].Content;
            iso8583[4].Content = Pre_iso8583[4].Content;
            iso8583[11].Content = Pre_iso8583[11].Content;
            iso8583[22].Content = Pre_iso8583[22].Content;
            iso8583[25].Content = Pre_iso8583[25].Content;
            iso8583[35].FieldLenIndicator.Content = (Pre_iso8583[35].Content.Length / 2).ToString();
            iso8583[35].Content = Pre_iso8583[35].Content;
            iso8583[39].Content = "06";
            iso8583[41].Content = Pre_iso8583[41].Content;
            iso8583[42].Content = Pre_iso8583[42].Content;
            iso8583[49].Content = Pre_iso8583[49].Content;
            iso8583[60].FieldLenIndicator.Content = "019";
            iso8583[60].Content = "2200301000020000000";////Pre_iso8583[60].Content;
            iso8583[61].FieldLenIndicator.Content = "016";
            iso8583[61].Content = string.Format("{0}{1}{2}", Pre_iso8583[11].Content, "003010", DateTime.Now.ToString("MMDD"));
            iso8583[64].Content = "1111111111111111";
            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0400";
            byte[] subMsg = msg.Pack();
            return subMsg;
        }

        /// <summary>
        /// 查询余额预处理
        /// </summary>
        /// <param name="pinBlock">加密后密码</param>
        /// <param name="creditCard">银行卡信息</param>
        /// <returns></returns>
        public byte[] Query(string terminalNo,string pinBlock,CreditCard creditCard)
        {
            MagCard Card = creditCard as MagCard;
            if (Card == null)
                throw new ArgumentException("无效的卡片");
            Bitmap map = new Bitmap();
            map[3] = map[11] = map[22] = map[25] = map[26] = map[35] = map[41] = map[42] = map[49] = map[52] = map[53] = map[60] = map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[35].FieldLenIndicator.Content = (Card.Msg2.Length/2).ToString();
            iso8583[35].Content = Card.Msg2;
            iso8583[3].Content = "310000";
            iso8583[11].Content = Settings.SysTraceNum;
            Settings.SysTraceNum = (Convert.ToInt32(Settings.SysTraceNum) >= 999999 ? 0 : Convert.ToInt32(Settings.SysTraceNum) + 1).ToString().PadLeft(6, '0');
            iso8583[22].Content = "021";
            iso8583[25].Content = "00";
            iso8583[26].Content = "06";
            iso8583[41].Content = terminalNo;
            iso8583[42].Content = Settings.MerchantCode;
            iso8583[49].Content = "156";
            iso8583[52].Content = pinBlock;
            iso8583[53].Content = "26";
            iso8583[60].FieldLenIndicator.Content = "011";
            iso8583[60].Content = "00003010003";
            iso8583[64].Content = "1111111111111111";

            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0200";
            StringBuilder mac = new StringBuilder();
            byte[] subMsg = msg.Pack();

            return subMsg;
        }
    }
}
