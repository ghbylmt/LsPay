using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LsPay.Client.Function.Code;
using LsPay.Client.Interface;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// 加密设备-证通密码键盘
    /// </summary>
    public class EncryEquipment_ZT : IEncryEquipment
    {
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns></returns>
        public string GetPinBlock()
        {
            StringBuilder sbBlock = new StringBuilder();
            ZT_EPP.ZT_EPP_PinReadPin(2, sbBlock);
            return sbBlock.ToString().Substring(0, 16);
        }
        /// <summary>
        /// 核对PIN码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CheckPIN(StringBuilder source)
        {
            StringBuilder result = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            ZT_EPP.ZT_EPP_SetDesPara(0x07, 0x20);
            ZT_EPP.ZT_EPP_PinAdd(2, 0, source, result);
            return result.ToString().Substring(0, 16);
        }
        /// <summary>
        /// 计算PIN码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CalculatePIN(StringBuilder source)
        {
            File.AppendAllText("pin.txt", source.ToString());
            StringBuilder result = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            ZT_EPP.ZT_EPP_PinAdd(2, 0, source, result);
            return result.ToString().Substring(0, 16);
        }

        /// <summary>
        /// 计算MAC码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CalculateMAC(StringBuilder source)
        {
            byte[] sourceBytes = CodeConvert.HexStringToByteArray(source.ToString());
            File.AppendAllText("mac.txt", source.ToString());
            StringBuilder sbMac = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x01);
            ZT_EPP.ZT_EPP_SetDesPara(0x06, 0x03);
            ZT_EPP.ZT_EPP_PinCalMAC(1, 4, source, sbMac);
            string mac = CodeConvert.ToHexString(Encoding.ASCII.GetBytes(sbMac.ToString().Substring(0, 8)));
            return mac;
        }

        /// <summary>
        /// 核对MAC码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CheckMAK(StringBuilder source)
        {
            StringBuilder sbMac = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x01);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x20);
            ZT_EPP.ZT_EPP_SetDesPara(0x07, 0x10);
            ZT_EPP.ZT_EPP_PinAdd(1, 0, source, sbMac);
            return sbMac.ToString().Substring(0, 16);
        }

        public string CheckTRK(StringBuilder source)
        {
            StringBuilder result = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x02);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            ZT_EPP.ZT_EPP_SetDesPara(0x07, 0x20);
            ZT_EPP.ZT_EPP_PinAdd(0, 0, source, result);
            return result.ToString().Substring(0, 16);
        }


        /// <summary>
        /// 计算Track码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CalculateTRK(StringBuilder source)
        {
            File.AppendAllText("track.txt", source.ToString());
            StringBuilder result = new StringBuilder();
            StringBuilder resultOnce = new StringBuilder();
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x02);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            int sourcelength = source.ToString().Length;
            if (sourcelength % 16 != 0) throw new ArgumentException("数据源长度不正确，必须为16的倍数！");
            int count = sourcelength / 16;
            for (int i = 0; i < count; i++)
            {
                ZT_EPP.ZT_EPP_PinAdd(2, 0, new StringBuilder(source.ToString().Substring(i * 16, 16)), resultOnce);
                result.Append(resultOnce.ToString());
            }
            return result.ToString().Substring(0, sourcelength);
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public void LoadWorkKeySign(StringBuilder sbWorkKey, StringBuilder sbMak, StringBuilder sbTrk = null)
        {
            StringBuilder sbReturn = new StringBuilder();
            ZT_EPP.ZT_EPP_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM), 9600);

            #region 01. PIN加密工作密钥
            ZT_EPP.ZT_EPP_SetDesPara(0x05, 0x04);
            ZT_EPP.ZT_EPP_SetDesPara(0x00, 0x30);
            ZT_EPP.ZT_EPP_PinLoadWorkKey(0x02, 0x00, 0x00, sbWorkKey, sbReturn);
            #endregion

            #region 02. Mac密钥
            ZT_EPP.ZT_EPP_SetDesPara(0x00, 0x20);
            ZT_EPP.ZT_EPP_SetDesPara(0x05, 0x04);
            ZT_EPP.ZT_EPP_SetDesPara(0x00, 0x30);
            ZT_EPP.ZT_EPP_PinLoadWorkKey(0x02, 0x00, 0x01, sbMak, sbReturn);
            #endregion

            if (sbTrk == null) return;
            #region 03. Track工作密钥 TRK
            ZT_EPP.ZT_EPP_SetDesPara(0x05, 0x04);
            ZT_EPP.ZT_EPP_SetDesPara(0x00, 0x30);
            ZT_EPP.ZT_EPP_PinLoadWorkKey(0x02, 0x00, 0x02, sbTrk, sbReturn);
            #endregion

        }


        public void Open(string cardNum)
        {
            StringBuilder sbBlock = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            StringBuilder sbFlag = new StringBuilder();
            ZT_EPP.ZT_EPP_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM), 9600);
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x02, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            ZT_EPP.ZT_EPP_SetDesPara(0x05, 0x01);
            ZT_EPP.ZT_EPP_SetDesPara(0x04, 0x10);
            ZT_EPP.ZT_EPP_PinLoadCardNo(new StringBuilder(cardNum.Substring(cardNum.Length - 13, 12)));
            ZT_EPP.ZT_EPP_OpenKeyVoic(0x02);
            ZT_EPP.ZT_EPP_PinStartAdd(6, 0x01, 0x01, 0, 20, sbReturn);
        }

        public void Close()
        {
            StringBuilder sbReturn = new StringBuilder();
            ZT_EPP.ZT_EPP_OpenKeyVoic(0x00);
            //ZT_EPP.ZT_EPP_CloseCom();
        }
    }
}
