/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：日志操作类
 * 文件：LogUtil.cs
 * 类名：LogUtil
 * 命名空间：LsPay.Client.Function.Log
 * 
 * 创建标识：尚春城 2015/07/02  
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Client.Function.Log
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogUtil
    {
        public static void WriteRequestLog(byte[] content)
        {
            //文件路径
            string FilePath = string.Format("Log//{0}",DateTime.Now.ToString("yyyy-MM-dd"));
            System.IO.File.AppendAllText(FilePath, "\r\n");
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", string.Empty.PadRight(100,'*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[ 请求报文开始 ]").PadRight(45, '*').PadRight(45, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[yyyy-MM-dd  HH:mm:ss]").PadRight(39,'*').PadRight(39,'*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", string.Empty.PadRight(100, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("[ {0} ]\n",BitConverter.ToString(content).Replace("-"," ")));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[ 结束 ]").PadRight(47, '*').PadRight(47,'*')));
        }

        public static void WriteResponseLog(byte[] content)
        {
            //文件路径
            string FilePath = string.Format("Log//{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            System.IO.File.AppendAllText(FilePath, "\r\n");
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", string.Empty.PadRight(100, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[ 响应报文开始 ]").PadRight(45, '*').PadRight(45, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[yyyy-MM-dd  HH:mm:ss]").PadRight(39, '*').PadRight(39, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", string.Empty.PadRight(100, '*')));
            System.IO.File.AppendAllText(FilePath, string.Format("[ {0} ]\n", BitConverter.ToString(content).Replace("-", " ")));
            System.IO.File.AppendAllText(FilePath, string.Format("|{0}|\n", DateTime.Now.ToString("[ 响应报文结束 ]").PadRight(45, '*').PadRight(45, '*')));
        }
    }
}
