/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：TLV格式报文实体类
 * 文件：TLVEntity.cs
 * 类名：TLVEntity
 * 命名空间：LsPay.Client.Model.Entity
 * 
 * 创建标识：尚春城 2015/07/03
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Model.Entity
{
    /// <summary>
    /// TLV格式报文实体类
    /// </summary>
    public class TLVEntity
    {
        /// <summary>
        /// 标记
        /// </summary>
        public byte[] Tag { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public byte[] Length { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// 标记占用字节数
        /// </summary>
        public int TagSize { get { return this.Tag.Length; } }

        /// <summary>
        /// 数据长度占用字节数
        /// </summary>
        public int LengthSize { get { return this.Length.Length; } }

        /// <summary>
        /// 子嵌套TLV实体列表
        /// </summary>
        public List<TLVEntity> SubTLVEntity { get; set; }
    }
}
