using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model
{
    public class SignResponseModel
    {
        [DataMember]
        public string ResponseCode { get; set; }
        /// <summary>
        /// 签到响应内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }
    }
}
