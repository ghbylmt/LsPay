using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class CancelResponse : BaseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string fun { get; set; }
       
    }
}
