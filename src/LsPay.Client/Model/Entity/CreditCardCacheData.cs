using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Model.Entity
{
    /// <summary>
    /// 银行卡缓存数据
    /// </summary>
    public class CreditCardCacheData
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 2磁道数据
        /// </summary>
        public string Msg2 { get; set; }
    }
}
