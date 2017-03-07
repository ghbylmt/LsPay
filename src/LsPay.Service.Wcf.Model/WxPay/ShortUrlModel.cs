using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.WxPay
{
    /// <summary>
    /// 转换短连接接口
    /// </summary>
    [DataContract]
    public class ShortUrlModel
    {
        /// <summary>
        /// URL链接
        /// 需要转换的URL，签名用原串，传输需URLencode,连接长度最多512位
        /// </summary>
        [DataMember]
        public string long_url { get; set; }
       
    }
}
