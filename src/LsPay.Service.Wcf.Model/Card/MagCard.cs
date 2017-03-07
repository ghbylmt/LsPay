/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：磁条卡卡片实体类
 * 文件：MagCard.cs
 * 类名：MagCard
 * 命名空间：LsPay.Service.Wcf.Model.Card
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Wcf.Model.Card
{
    /// <summary>
    /// 磁条卡
    /// </summary>
    [DataContract]
    public class MagCard : CreditCard
    {
        /// <summary>
        /// 1磁道信息
        /// </summary>
        [DataMember]
        public string Msg1 { get; set; }
        /// <summary>
        /// 3磁道信息
        /// </summary>
       [DataMember]
        public string Msg3 { get; set; }
    }
}
