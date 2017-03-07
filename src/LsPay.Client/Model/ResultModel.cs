using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Model
{
    public class ResultModel
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public object Content { get; set; }
    }

    public class ResultMapping
    {
        ///public const Dictionary<>
    }
}
