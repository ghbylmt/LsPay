using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LsPay.Service.ISO8583;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 消息模版接口
    /// </summary>
    public interface IMessageTemplate
    {
        /// <summary>
        /// 获取模版
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Field> GetTemplate();
    }
}
