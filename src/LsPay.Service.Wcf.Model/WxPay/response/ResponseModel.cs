using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    /// <summary>
    /// 返回数据传输类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel
    {
        /// <summary>
        /// 返回状态码
        /// SUCCESS/FAIL 
        /// 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }
    }
}
