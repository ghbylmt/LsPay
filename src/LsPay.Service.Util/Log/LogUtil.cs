/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：日志操作类
 * 文件：LogUtil.cs
 * 类名：LogUtil
 * 命名空间：LsPay.Service.Util.Log
 * 
 * 创建标识：尚春城 2015/07/02  
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;

namespace LsPay.Service.Util.Log
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogUtil
    {
        /// <summary>
        /// 银联卡日志记录
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="module">模块</param>
        /// <param name="description">描述</param>
        /// <param name="content">内容(请求报文)</param>
        public static void WriteLog(string title, byte[] content, string description = "")
        {
            string contentStr = BitConverter.ToString(content).Replace("-", " ");
            LogUtil.WriteLog(title, LogModule.UNIONPAY, description, contentStr);
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="module">模块</param>
        /// <param name="description">描述</param>
        /// <param name="content">内容</param>
        public static void WriteLog(string title, string module, string description, string content)
        {
            Log.Info(new LogModel { Content = content, Description = description, Module = module, Title = title });
        }
        /// <summary>
        /// 支付宝支付日志记录
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="content">内容</param>
        public static void WriteAlipayLog(string title, string description, string content)
        {
            Log.Info(new LogModel { Content = content, Description = description, Module = LogModule.ALIPAY, Title = title });
        }

        /// <summary>
        /// 微信支付日志记录
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="content">内容</param>
        public static void WriteWxpayLog(string title, string description, string content)
        {
            Log.Info(new LogModel { Content = content, Description = description, Module = LogModule.WXPAY, Title = title });
        }
    }
}
