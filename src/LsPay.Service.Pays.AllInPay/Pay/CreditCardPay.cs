using System;
using LsPay.Service.ISO8583;
using LsPay.Service.Util;
using LsPay.Service.Wcf.Model;
using System.Linq;

namespace LsPay.Service.Pays.AllInPay.Pay
{
    /// <summary>
    /// 卡片支付类
    /// </summary>
    public class CreditCardPay
    {
        /// <summary>
        /// 向银行服务器发送请求
        /// </summary>
        /// <param name="preMsg"></param>
        /// <param name="mac"></param>
        /// <returns></returns>
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
            if(Res_iso8583.Keys.Contains(38))
                resultModel.ExtendInfo =string.Format("{0},{1}",Res_iso8583[37].Content,Res_iso8583[38].Content);//终端号
            else
                resultModel.ExtendInfo =string.Format("{0}",Res_iso8583[37].Content);//终端号
            #endregion
            return resultModel;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="preMsg"></param>
        /// <param name="mac"></param>
        /// <param name="Res_iso8583"></param>
        /// <returns></returns>
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
            if (resultModel.ResponseCode == "00")
                resultModel.ExtendInfo = Res_iso8583[54].Content;
            #endregion
            return resultModel;
        }
    }
}
