/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：配置信息数据类
 * 文件：Settings.cs
 * 类名：Settings
 * 命名空间：LsPay.Service.Util.Code
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LsPay.Service.Util.File;
using LsPay.Service.Util;

namespace LsPay.Service.Util
{
    /// <summary>
    /// 配置信息数据类
    /// </summary>
    public class Settings
    {
        #region 字段
        private static string _bankIp;
        private static string _bankPort;
        private static string _tpdu;
        private static string _msgHead;
        private static string _batchNo;
        private static string _merchantCode;
        private static string _systraceNum;
        private static string _passwordKeyBorad_Com;
        private static string _cardReader_Com;
        private static string _payment_platform;
        #endregion

        #region 属性

        #region Server
        /// <summary>
        /// 银行IP地址
        /// </summary>
        public static string BankIp
        {
            get { return GetPropertyValue(ref _bankIp, "BankIp"); }
        }
        /// <summary>
        /// 银行IP端口
        /// </summary>
        public static string BankPort
        {
            get { return GetPropertyValue(ref _bankPort, "BankPort"); }
        }
        #endregion

        #region Terminal
        public static string TPDU
        {
            get { return GetPropertyValue(ref _tpdu, "TPDU"); }
        }
        /// <summary>
        /// 消息头
        /// </summary>
        public static string MsgHead
        {
            get { return GetPropertyValue(ref _msgHead, "MsgHead"); }
        }


        /// <summary>
        /// 商户代码
        /// </summary>
        public static string MerchantCode
        {
            get { return GetPropertyValue(ref _merchantCode, "MerchantCode"); }
        }
        /// <summary>
        /// 密码键盘端口
        /// </summary>
        public static string PasswordKeyBoard_COM
        {
            get { return GetPropertyValue(ref _passwordKeyBorad_Com, "PasswordKeyBoard_COM"); }
        }
        /// <summary>
        /// 读卡器端口
        /// </summary>
        public static string CardReader_COM
        {
            get { return GetPropertyValue(ref _cardReader_Com, "CardReader_COM"); }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public static string SysTraceNum
        {
            get { return GetPropertyValue(ref _systraceNum, "SysTraceNum", true).PadLeft(6,'0'); }
            set { SetPropertyValue(value.PadLeft(6, '0'), "SysTraceNum"); }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public static string BatchNo
        {
            get { return GetPropertyValue(ref _batchNo, "BatchNo", true).PadLeft(6, '0'); }
            set { SetPropertyValue(value, "BatchNo"); }
        }
        #endregion

        #region Other
        /// <summary>
        /// 支付平台
        /// </summary>
        public static string PaymentPlatForm
        {
            get { return GetPropertyValue(ref _payment_platform, "PaymentPlatForm"); }
        }
        #endregion

        #endregion

        #region 方法
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="value"></param>
        private static string GetPropertyValue(ref string value, string key, bool getLatest = false)
        {
            if (string.IsNullOrEmpty(value) || getLatest)
            {
                if (ConfigurationManager.AppSettings[key] == null)
                    throw new ArgumentException(string.Format("AppSettings中找不到键值为{0}的节点", key));
                value = ConfigurationManager.AppSettings[key].ToString();
            }
            return value;
        }
        /// <summary>
        /// 给对应的属性赋值
        /// </summary>
        private static void SetPropertyValue(string value, string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
                throw new ArgumentException(string.Format("AppSettings中找不到键值为{0}的节点", key));
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

    }
}
