/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：日志数据模型类
 * 文件：LogModel.cs
 * 类名：LogModel
 * 命名空间：LsPay.Service.Util.Log
 * 
 * 创建标识：尚春城 2016/03/09  
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Util.Log
{
    /// <summary>
    /// 日志数据模型类
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedTime { get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
