using System;
using System.Collections.Generic;
using System.Linq;
using LsPay.Service.Wcf.Model;
using LsPay.Service.ISO8583;
using LsPay.Service.Util;

namespace LsPay.Service.Pays.BankOfCangzhou.Pay
{
    /// <summary>
    /// 卡片支付类
    /// </summary>
    public class CreditCardPay
    {
        protected PayResponseModel Send(byte[] preMsg, string mac)
        {
            Bitmap map = new Bitmap();
            map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[64].Content = mac;
            iso8583[64].Pack().CopyTo(preMsg, preMsg.Length - 8);
            Iso8583 Res_iso8583 = new Iso8583();
            Message Res_Msg = new Message(Res_iso8583);
            Res_Msg.Unpack(Utilities.Send(Settings.BankIp, Convert.ToInt32(Settings.BankPort), preMsg));
            Settings.SysTraceNum = (Convert.ToInt32(Settings.SysTraceNum) >= 999999 ? 1 : Convert.ToInt32(Settings.SysTraceNum) + 1).ToString();
            #region 组织返回数据
            PayResponseModel resultModel = new PayResponseModel();
            resultModel.ResponseCode = Res_iso8583[39].Content;
            resultModel.Pan = Res_iso8583[2].Content;
            resultModel.Money = Res_iso8583[4].Content;
            resultModel.TransactionSerialNum = Res_iso8583[11].Content;
            string transactionTimeStr = Res_iso8583[13].Content + Res_iso8583[12].Content;
            resultModel.TransactionTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                DateTime.Now.Date.Year.ToString(),
                transactionTimeStr.Substring(0, 2),
                transactionTimeStr.Substring(2, 2),
                transactionTimeStr.Substring(4, 2),
                transactionTimeStr.Substring(6, 2),
                transactionTimeStr.Substring(8, 2));
            if (Res_iso8583.Keys.Contains(38))
                //附加数据
                //假如38域授权码存在时返回
                resultModel.ExtendInfo = string.Format("{0},{1}", Res_iso8583[37].Content, Res_iso8583[38].Content);
            else
                //假如38域不存在则只返回37域交易参考号用于撤销交易
                resultModel.ExtendInfo = string.Format("{0}", Res_iso8583[37].Content);
            #endregion
            return resultModel;
        }

        protected PayResponseModel Send(byte[] preMsg, string mac, out Iso8583 Res_iso8583)
        {
            Bitmap map = new Bitmap();
            map[64] = true;
            Iso8583 iso8583 = new Iso8583(map);
            iso8583[64].Content = mac;
            iso8583[64].Pack().CopyTo(preMsg, preMsg.Length - 8);
            Res_iso8583 = new Iso8583();
            Message Res_Msg = new Message(Res_iso8583);
            Res_Msg.Unpack(Utilities.Send(Settings.BankIp, Convert.ToInt32(Settings.BankPort), preMsg));
            Settings.SysTraceNum = (Convert.ToInt32(Settings.SysTraceNum) >= 999999 ? 1 : Convert.ToInt32(Settings.SysTraceNum) + 1).ToString();
            #region 组织返回数据
            PayResponseModel resultModel = new PayResponseModel();
            resultModel.ResponseCode = Res_iso8583[39].Content;
            resultModel.Pan = Res_iso8583[2].Content;
            resultModel.Money = Res_iso8583[4].Content;
            resultModel.TransactionSerialNum = Res_iso8583[11].Content;
            resultModel.TransactionTime = Res_iso8583[12].Content + Res_iso8583[13].Content;
            resultModel.ExtendInfo = Res_iso8583[54].Content;
            #endregion
            return resultModel;
        }
    }
}
