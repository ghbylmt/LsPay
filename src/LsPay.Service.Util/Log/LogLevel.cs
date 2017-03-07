using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Util.Log
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        Debug = 0,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 1,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 2,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 3,
        /// <summary>
        /// 致命错误
        /// </summary>
        FATAL = 4
    }
}
