using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Util.Code
{
    /// <summary>
    /// TLV格式功能类
    /// </summary>
    public class TLVUtil
    {
        /// <summary>
        /// 根据Tag和value 拼接 TLV格式
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static string GetTLVByTv(string tag, string value)
        {
            string length = (value.Length / 2).ToString("x2");
            return tag + length + value;
        }
    }
}
