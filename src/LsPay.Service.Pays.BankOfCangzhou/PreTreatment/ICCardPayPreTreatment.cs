﻿/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：芯片卡支付预处理类
 * 文件：ICCardPayPreTreatment.cs
 * 类名：ICCardPayPreTreatment
 * 命名空间：LsPay.Service.Pays.BankOfCangzhou.Pay
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Interface;
using LsPay.Service.ISO8583;
using LsPay.Service.Util;
using LsPay.Service.Util.Data;
using LsPay.Service.Wcf.Model.Card;
using System;
using System.Text;

namespace LsPay.Service.Pays.BankOfCangzhou.Pay
{
    /// <summary>
    /// 芯片卡支付预处理
    /// </summary>
    public class ICCardPayPreTreatment : IPayPreTeatment
    {
        public byte[] Pay(string terminalNo,string PayMoney, string pinBlock, CreditCard creditCard)
        {
            ICCard Card = creditCard as ICCard;
            if (Card == null)
                throw new ArgumentException("无效的卡片");
            Bitmap map = new Bitmap();
            map[2] =
            map[3] =
            map[4] =
            map[11] =
            map[14] =
            map[22] =
            map[23] =
            map[25] =
            map[26] =
            map[35] =
            map[41] =
            map[42] =
            map[49] =
            map[52] =
            map[53] =
            map[55] =
            map[60] =
            map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[2].FieldLenIndicator.Content = Card.CardNo.Length.ToString();
            iso8583[2].Content = Card.CardNo;
            iso8583[3].Content = "000000";
            iso8583[4].Content = PayMoney;
            iso8583[11].Content = Settings.SysTraceNum;

            iso8583[14].Content = Card.EffectiveDate.ToString("yyMM");//有效日期
            iso8583[22].Content = "051";//IC卡支付
            iso8583[23].Content = Card.PANSerialNum.PadLeft(4, '0');
            iso8583[25].Content = "00";
            iso8583[26].Content = "06";
            iso8583[35].FieldLenIndicator.Content = Card.Msg2.Length.ToString();
            iso8583[35].Content = Card.Msg2;
            iso8583[41].Content = terminalNo;
            iso8583[42].Content = Settings.MerchantCode;
            iso8583[49].Content = "156";
            iso8583[52].Content = pinBlock;
            iso8583[53].Content = "26";
            iso8583[55].Content = Card.BuildPart55Data(terminalNo,TransactionType.Pay, PayMoney, Settings.SysTraceNum);
            iso8583[55].FieldLenIndicator.Content = string.Format("{0:000}", (iso8583[55].Content.Length / 2));
            iso8583[60].FieldLenIndicator.Content = "019";
            iso8583[60].Content = string.Format("22{0}00050000000",Settings.BatchNo);
            iso8583[64].Content = "1111111111111111";
            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0200";
            StringBuilder mac = new StringBuilder();
            byte[] subMsg = msg.Pack();
            return subMsg;
        }

        public byte[] CancelPay(byte[] preMsg,CreditCard creditCard,string posSerialNo)
        {
            ICCard Card = creditCard as ICCard;
            if (Card == null)
                throw new ArgumentException("无效的卡片");
            Iso8583 pre_iso8583 = new Iso8583();
            Message pre_Msg = new Message(pre_iso8583);
            pre_Msg.Unpack(preMsg);
            string[] extendData = posSerialNo.Split(',');
            string posserialNo = extendData[0];
            string AuthorizationCode = extendData.Length > 1 ? extendData[1] : null;
            string preSysTraceNum = pre_iso8583[11].Content;
            if (Settings.SysTraceNum.Equals(preSysTraceNum))
                Settings.SysTraceNum = (Convert.ToInt32(Settings.SysTraceNum) + 1).ToString();
            Bitmap map = new Bitmap();
                map[2] =
                map[3] =
                map[4] =
                map[11] =
                map[22] =
                map[23] =
                map[25] =
                map[35] =
                map[37] =
                map[39] =
                map[41] =
                map[42] =
                map[49] =
                map[52] =
                map[53] =
                map[55] =
                map[60] =
                map[64] = true;
            //if (!string.IsNullOrEmpty(AuthorizationCode)) map[38] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[2].FieldLenIndicator.Content = Card.CardNo.Length.ToString();
            iso8583[2].Content = Card.CardNo;
            iso8583[3].Content = pre_iso8583[3].Content;
            iso8583[4].Content = pre_iso8583[4].Content;
            iso8583[11].Content = pre_iso8583[11].Content;
            iso8583[22].Content = pre_iso8583[22].Content;
            iso8583[23].Content = pre_iso8583[23].Content;
            iso8583[25].Content = pre_iso8583[25].Content;
            iso8583[35].FieldLenIndicator.Content = Card.Msg2.Length.ToString();
            iso8583[35].Content = Card.Msg2;
            iso8583[37].Content = posserialNo;
            iso8583[39].Content = "06";
            //if (!string.IsNullOrEmpty(AuthorizationCode)) 
              //  iso8583[38].Content = AuthorizationCode;
            iso8583[41].Content = pre_iso8583[41].Content;
            iso8583[42].Content = pre_iso8583[42].Content;
            iso8583[49].Content = pre_iso8583[49].Content;
            iso8583[52].Content = pre_iso8583[52].Content;
            iso8583[53].Content = pre_iso8583[53].Content;
            iso8583[55].Content = string.Format("9F3602{0}", Card.ATC);
            iso8583[55].FieldLenIndicator.Content = string.Format("{0:000}", (iso8583[55].Content.Length / 2));
            iso8583[60].FieldLenIndicator.Content = "013";
            iso8583[60].Content = string.Format("23{0}00050",Settings.BatchNo);
            iso8583[64].Content = "1111111111111111";
            Message msg = new Message(iso8583);
            msg.TPDU.Content = Settings.TPDU;
            msg.MessageHead.Content = Settings.MsgHead;
            msg.MessageType.Content = "0400";
            byte[] subMsg = msg.Pack();
            return subMsg;
        }

        public byte[] Query(string terminalNo, string pinBlock, CreditCard creditCard)
        {
            ICCard Card = creditCard as ICCard;
            if (Card == null)
                throw new ArgumentException("无效的卡片");
            Bitmap map = new Bitmap();
            map[2] = map[3] = map[11] = map[22] = map[25] = map[26] = map[41] = map[42] = map[49] = map[52] = map[53] = map[60] = map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[2].FieldLenIndicator.Content = Card.CardNo.Length.ToString();
            iso8583[2].Content = Card.CardNo;
            iso8583[3].Content = "310000";

            iso8583[11].Content = Settings.SysTraceNum;

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
