using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model
{
    /// <summary>
    /// 支付响应实体
    /// </summary>
    [DataContract] 
    public class PayResponseModel
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [DataMember]
        public string ResponseCode { get; set; }
        /// <summary>
        /// 主账号
        /// </summary>
        [DataMember]
        public string Pan { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        [DataMember]
        public string Money { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        [DataMember]
        public string TransactionSerialNum { get; set; }
        /// <summary>
        /// 交易时间[yyyy-MM-dd HH:mm:ss]
        /// </summary>
        [DataMember]
        public string TransactionTime { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        [DataMember]
        public string ExtendInfo { get; set; }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", ResponseCode, Pan, Money, TransactionSerialNum, TransactionTime,ExtendInfo);
        }
    }
}
