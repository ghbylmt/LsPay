/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：银行卡卡片实体类
 * 文件：CreditCard.cs
 * 类名：CreditCard
 * 命名空间：LsPay.Service.Wcf.Model.Card
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System.Runtime.Serialization;

namespace LsPay.Service.Wcf.Model.Card
{
    /// <summary>
    /// 银行卡卡片实体类
    /// </summary>
    [DataContract]
    [KnownType(typeof(ICCard))]
    [KnownType(typeof(MagCard))]
    public class CreditCard
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CardNo { get; set; }
        /// <summary>
        /// 2磁道数据
        /// </summary>
        [DataMember]
        public string Msg2 { get; set; }
    }
}
