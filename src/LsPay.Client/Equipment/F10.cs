/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：F10密码键盘操作功能类
 * 文件：F10.cs
 * 类名：F10
 * 命名空间：LsPay.Client.Equipment
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LsPay.Client.Interface;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// F10密码键盘操作功能类
    /// </summary>
    public class F10
    {
        /// <summary>
        /// 加密字符
        /// </summary>
        private StringBuilder sbBlock;

        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_OpenCom(int port, long baud);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_CloseCom();
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_LoadWorkKey(byte mKeyId, byte wKeyId, byte wKeyLen, StringBuilder wKey, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_ActiveKey(byte mKeyId, byte wKeyId, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_SetAlgorithmParameter(byte ucPPara, byte ucFPara, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_DataEncrypt(byte dataLen, StringBuilder data, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_LoadCardNumber(StringBuilder cardNumber, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_UseEppPlainTextMode(byte textModeFormat, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_StartEpp(byte pinLen, byte algorithmMode, byte timeout, StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_ReadCypherPin(StringBuilder returnInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_ScanKeyPress(StringBuilder keyValue);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_MakeMac(int nDataLen, StringBuilder data, StringBuilder resultInfo);
        [DllImport("XZ_F10_API.dll")]
        public static extern int SUNSON_MakeUBCMac(int nDataLen, StringBuilder data, StringBuilder resultInfo, StringBuilder hexReturnInfo);

        /// <summary>
        /// 打开密码键盘
        /// </summary>
        /// <param name="CardNo">卡号</param>
        public void Open(string CardNo)
        {
            sbBlock = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            StringBuilder sbFlag = new StringBuilder();
            F10.SUNSON_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM),9600);
            F10.SUNSON_ActiveKey(0x00, 0x00, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x02, 0x00, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x01, 0x30, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x05, 0x01, sbReturn);
            F10.SUNSON_SetAlgorithmParameter(0x04, 0x10, sbReturn);
            F10.SUNSON_LoadCardNumber(new StringBuilder(CardNo.Substring(CardNo.Length - 13, 12)), sbReturn);
            F10.SUNSON_UseEppPlainTextMode(0x02, sbReturn);
            F10.SUNSON_StartEpp(6, 0x01, 20, sbReturn);
        }
        /// <summary>
        /// 关闭键盘
        /// </summary>
        public void Close()
        {
            StringBuilder sbReturn = new StringBuilder();
            F10.SUNSON_UseEppPlainTextMode(0x00, sbReturn);
        }

        /// <summary>
        /// 扫描键盘输入。
        /// </summary>
        /// <returns>返回输入结果，用于前台显示。</returns>
        public F10Key ScanKey()
        {
            StringBuilder sbKey = new StringBuilder();
            F10.SUNSON_ScanKeyPress(sbKey);
            return (F10Key)(Encoding.ASCII.GetBytes(sbKey.ToString())[0]);
        }

        public string GetPinBlock()
        {
            F10.SUNSON_ReadCypherPin(sbBlock);
            return sbBlock.ToString().Substring(0,16);
        }
    }
    /// <summary>
    /// F10按键返回值
    /// </summary>
    public enum F10Key
    {
        /// <summary>
        /// 取消
        /// </summary>
        CANCEL = 0x1B,
        /// <summary>
        /// 确认
        /// </summary>
        CONFIRM = 0x0D,
        /// <summary>
        /// 更正
        /// </summary>
        CORRECT = 0x08,
        /// <summary>
        /// 加密后的返回的字符
        /// </summary>
        ENCRY_KEY = 0x2A
    }
}
