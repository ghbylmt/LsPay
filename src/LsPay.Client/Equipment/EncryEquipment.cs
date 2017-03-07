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
    /// 加密设备 - 旭子F10
    /// </summary>
    public class EncryEquipment_F10 : IEncryEquipment
    {
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns></returns>
        public string GetPinBlock()
        {
            StringBuilder sbBlock = new StringBuilder();
            F10.SUNSON_ReadCypherPin(sbBlock);
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
            F10.SUNSON_ActiveKey(0x00, 0x00, result);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, result);
            F10.SUNSON_SetAlgorithmParameter(0x07, 0x20, result);
            F10.SUNSON_DataEncrypt((byte)(source.ToString().Length / 2), source, result);
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
            F10.SUNSON_ActiveKey(0x00, 0x00, result);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, result);
            F10.SUNSON_DataEncrypt((byte)(source.ToString().Length / 2), source, result);
            return result.ToString().Substring(0, 16);
        }

        /// <summary>
        /// 计算MAC码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CalculateMAC(StringBuilder source)
        {
            File.AppendAllText("mac.txt", source.ToString());
            StringBuilder sbMac = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            F10.SUNSON_ActiveKey(0x00, 0x01, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x20, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x06, 0x04, sbReturn);
            F10.SUNSON_MakeUBCMac(source.Length / 2, source, sbReturn, sbMac);
            return sbMac.ToString().Substring(0, 16);
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
            F10.SUNSON_ActiveKey(0x00, 0x01, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x20, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x07, 0x10, sbReturn);
            F10.SUNSON_DataEncrypt(8, source, sbMac);
            return sbMac.ToString().Substring(0, 16);
        }

        public string CheckTRK(StringBuilder source)
        {
            StringBuilder result = new StringBuilder();
            F10.SUNSON_ActiveKey(0x00, 0x02, result);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, result);
            F10.SUNSON_SetAlgorithmParameter(0x07, 0x20, result);
            F10.SUNSON_DataEncrypt((byte)(source.ToString().Length / 2), source, result);
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
            F10.SUNSON_ActiveKey(0x00, 0x02, result);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, result);
            int sourcelength = source.ToString().Length;
            F10.SUNSON_DataEncrypt((byte)(sourcelength / 2), source, result);
            return result.ToString().Substring(0, sourcelength);
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public void LoadWorkKeySign(StringBuilder sbWorkKey, StringBuilder sbMak, StringBuilder sbTrk = null)
        {
            StringBuilder sbReturn = new StringBuilder();
            F10.SUNSON_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM), 9600);

            #region 01. PIN加密工作密钥            
            F10.SUNSON_SetAlgorithmParameter(0x05, 0x04, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x00, 0x30, sbReturn);
            F10.SUNSON_LoadWorkKey(0x00, 0x00, 0x10, sbWorkKey, sbReturn);
            #endregion

            #region 02. Mac密钥
            F10.SUNSON_SetAlgorithmParameter(0x00, 0x20, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x05, 0x04, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x00, 0x30, sbReturn);
            F10.SUNSON_LoadWorkKey(0x00, 0x01, 0x08, sbMak, sbReturn);
            #endregion

            if (sbTrk == null) return;
            #region 03. Track工作密钥 TRK
            F10.SUNSON_SetAlgorithmParameter(0x05, 0x04, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x00, 0x30, sbReturn);
            F10.SUNSON_LoadWorkKey(0x00, 0x02, 0x10, sbTrk, sbReturn);
            #endregion

        }

        public void Open(string cardNum)
        {
            StringBuilder sbBlock = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            StringBuilder sbFlag = new StringBuilder();
            F10.SUNSON_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM), 9600);
            F10.SUNSON_ActiveKey(0x00, 0x00, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x02, 0x00, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x05, 0x01, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x04, 0x10, sbReturn);
            F10.SUNSON_LoadCardNumber(new StringBuilder(cardNum.Substring(cardNum.Length - 13, 12)), sbReturn);
            F10.SUNSON_UseEppPlainTextMode(0x02, sbReturn);
            F10.SUNSON_StartEpp(6, 0x01, 20, sbReturn);
        }

        public void Close()
        {
            StringBuilder sbReturn = new StringBuilder();
            F10.SUNSON_UseEppPlainTextMode(0x00, sbReturn);
        }
    }
}
