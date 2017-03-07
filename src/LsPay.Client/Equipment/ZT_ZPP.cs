using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// 证通密码键盘
    /// </summary>
    public class ZT_EPP
    {
        /// <summary>
        /// 加密字符
        /// </summary>
        private StringBuilder sbBlock;

        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_OpenCom(int port, long baud);
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_CloseCom();
        /// <summary>
        /// 下载工作密钥
        /// </summary>
        /// <param name="iKMode">对应主密钥的长度模式</param>
        /// <param name="mKeyId">主密钥号</param>
        /// <param name="wKeyId">工作密钥号</param>
        /// <param name="wKey">工作密钥,长度必须为 16/32/48(拆分之后的数据,如 8 字节的数据</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinLoadWorkKey(byte iKMode, byte mKeyId, byte wKeyId, StringBuilder wKey, StringBuilder cpExChk);
        /// <summary>
        /// 激活工作秘钥
        /// </summary>
        /// <param name="mKeyId">主密钥号</param>
        /// <param name="wKeyId">工作秘钥号</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_ActivWorkPin(byte mKeyId, byte wKeyId);
        /// <summary>
        /// 设置算法参数
        /// </summary>
        /// <param name="ucPPara">P参数</param>
        /// <param name="ucFPara">F参数</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_SetDesPara(byte ucPPara, byte ucFPara);
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="iKMode">原工作秘钥的长度模式</param>
        /// <param name="iMode">加解密的模式</param>
        /// <param name="data">需要加密的数据</param>
        /// <param name="returnInfo">加密的结果</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinAdd(byte iKMode,byte iMode, StringBuilder data, StringBuilder returnInfo);
        /// <summary>
        /// 下载卡号
        /// </summary>
        /// <param name="cardNumber">卡号</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinLoadCardNo(StringBuilder cardNumber);
        /// <summary>
        /// 发送开关键盘和按键声音
        /// </summary>
        /// <param name="textModeFormat"></param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_OpenKeyVoic(byte textModeFormat);
        /// <summary>
        /// 启动密码键盘加密
        /// </summary>
        /// <param name="iPinLen"></param>
        /// <param name="iDispMode"></param>
        /// <param name="iPINMode"></param>
        /// <param name="iPromMode"></param>
        /// <param name="timeout"></param>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinStartAdd(byte iPinLen, byte iDispMode, byte iPINMode, byte iPromMode, byte timeout, StringBuilder returnInfo);
        /// <summary>
        /// 读取密码密文
        /// </summary>
        /// <param name="returnInfo">用户密码密文 </param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinReadPin(int keyMode,StringBuilder returnInfo);
        /// <summary>
        /// 读取按键
        /// </summary>
        /// <param name="keyValue">按键值</param>
        /// <param name="iTimeOut">超时时间</param>
        /// <returns></returns>
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_PinReportPressed(StringBuilder keyValue, int iTimeOut);
        [DllImport("ZT_EPP_API.dll")]
        ///参数说明：
        ///iKMode:原工作密钥的长度模式
        ///iMacMode:运算的 MAC 算法
        ///MAC 运算 
        ///1:表示 X9.9/X9.19 算法 
        ///2 表示:PSAM(ECB)算法 
        ///3:表示 PBOC 算法 
        ///4:表示银联的算法 
        ///5:表示 CBC 算法
        ///
        public static extern int ZT_EPP_PinCalMAC(int iKMode, int iMacMode, StringBuilder data, StringBuilder resultInfo);
        [DllImport("ZT_EPP_API.dll")]
        public static extern int ZT_EPP_MakeUBCMac(int nDataLen, StringBuilder data, StringBuilder resultInfo, StringBuilder hexReturnInfo);

        /// <summary>
        /// 打开密码键盘
        /// </summary>
        /// <param name="CardNo">卡号</param>
        public void Open(string CardNo)
        {
            sbBlock = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            StringBuilder sbFlag = new StringBuilder();
            ZT_EPP.ZT_EPP_OpenCom(Convert.ToInt32(Settings.PasswordKeyBoard_COM), 9600);
            ZT_EPP.ZT_EPP_ActivWorkPin(0x00, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x02, 0x00);
            ZT_EPP.ZT_EPP_SetDesPara(0x01, 0x30);
            ZT_EPP.ZT_EPP_SetDesPara(0x05, 0x01);
            ZT_EPP.ZT_EPP_SetDesPara(0x04, 0x10);
            ZT_EPP.ZT_EPP_PinLoadCardNo(new StringBuilder(CardNo.Substring(CardNo.Length - 13, 12)));
            ZT_EPP.ZT_EPP_OpenKeyVoic(0x02);
            ZT_EPP.ZT_EPP_PinStartAdd(6, 0x01, 0x01, 0, 20, sbReturn);
        }
        /// <summary>
        /// 关闭键盘
        /// </summary>
        public void Close()
        {
            StringBuilder sbReturn = new StringBuilder();
            ZT_EPP.ZT_EPP_OpenKeyVoic(0x00);
            ZT_EPP.ZT_EPP_CloseCom();
        }

        /// <summary>
        /// 扫描键盘输入。
        /// </summary>
        /// <returns>返回输入结果，用于前台显示。</returns>
        public F10Key ScanKey()
        {
            StringBuilder sbKey = new StringBuilder();
            ZT_EPP.ZT_EPP_PinReportPressed(sbKey, 100);
            return (F10Key)(Encoding.ASCII.GetBytes(sbKey.ToString())[0]);
        }

        public string GetPinBlock()
        {
            ZT_EPP.ZT_EPP_PinReadPin(2,sbBlock);
            return sbBlock.ToString().Substring(0, 16);
        }
    }
}
