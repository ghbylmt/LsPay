using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay.response
{
    /// <summary>
    /// 请求返回基类
    /// </summary>
    [DataContract]
    public class BaseResponseModel
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [DataMember]
        public string code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public string subcode { get; set; }
        [DataMember]
        public string submsg { get; set; }
    }
}
