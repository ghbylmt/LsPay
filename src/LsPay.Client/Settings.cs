/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：配置信息数据类
 * 文件：Settings.cs
 * 类名：Settings
 * 命名空间：TTS.Core.ChinaUnionPay.Function.Code
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Client.Function.File;
using System;
using System.Xml;

namespace LsPay.Client
{
    /// <summary>
    /// 配置信息数据类
    /// </summary>
    public class Settings
    {
        #region 字段
        /// <summary>
        /// 文件路径
        /// </summary>
        private static string _xmlFilePath = "LsPay.Setttings.xml";

        private static string _terminalno;
        private static string _cardreader_com;
        private static string _passwordkeyboard_com;
        private static string _paypretreatment_service;
        private static string _pay_service;
        private static string _alipay_service;
        private static string _wxpay_service;
        #endregion

        #region 属性

        /// <summary>
        /// 终端编号
        /// </summary>
        public static string TerminalNo
        {
            get { return GetPropertyValue(ref _terminalno, "Settings//TerminalNo"); }
        }
        /// <summary>
        /// 读卡器COM串口配置
        /// </summary>
        public static string CardReader_COM
        {
            get { return GetPropertyValue(ref _cardreader_com, "Settings//CardReader_COM"); }
        }
        /// <summary>
        /// 密码键盘串口配置
        /// </summary>
        public static string PasswordKeyBoard_COM
        {
            get { return GetPropertyValue(ref _passwordkeyboard_com, "Settings//PasswordKeyBoard_COM"); }
        }
        /// <summary>
        /// 支付预处理服务地址
        /// </summary>
        public static string PayPreTreatmentService
        {
            get { return GetPropertyValue(ref _paypretreatment_service, "Settings//PayPreTreatmentService"); }
        }
        /// <summary>
        /// 支付处理服务地址
        /// </summary>
        public static string PayService
        {
            get { return GetPropertyValue(ref _pay_service, "Settings//PayService"); }
        }
        /// <summary>
        /// 支付宝支付服务地址
        /// </summary>
        public static string AliPayService
        {
            get { return GetPropertyValue(ref _alipay_service, "Settings//AliPayService"); }
        }
        /// <summary>
        /// 微信支付服务地址
        /// </summary>
        public static string WxPayService
        {
            get { return GetPropertyValue(ref _wxpay_service, "Settings//WxPayService"); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="value"></param>
        private static string GetPropertyValue(ref string value, string xmlpath, bool getLatest = false)
        {
            if (string.IsNullOrEmpty(value) || getLatest)
            {
                XmlDocument doc = XmlUtil.GetXmlDocByFilePath(_xmlFilePath);
                XmlNodeList nodes = XmlUtil.GetChildNodesByXPathExpr(doc, xmlpath);
                if (nodes == null)
                    throw new ArgumentException(string.Format("未找到文件{0}或不存在节点{1}", _xmlFilePath,xmlpath));
                value = nodes[0].InnerText;
            }
            return value;
        }
        /// <summary>
        /// 给对应的属性赋值
        /// </summary>
        private static void SetPropertyValue(string value, string xmlpath)
        {
            XmlDocument doc = XmlUtil.GetXmlDocByFilePath(_xmlFilePath);
            XmlNodeList nodes = XmlUtil.GetChildNodesByXPathExpr(doc, xmlpath);
            if (nodes == null)
                throw new ArgumentException(string.Format("未找到文件TTS.ChinaUnionPay.Setttings.xml或不存在节点{0}", xmlpath));
            nodes[0].InnerText = value;
            doc.Save(_xmlFilePath);
        }
        #endregion

    }
}
