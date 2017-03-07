/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：TLV实体操作帮助类
 * 文件：TLVHelper.cs
 * 类名：TLVHelper
 * 命名空间：LsPay.Client.Function.Code
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
using LsPay.Client.Model.Entity;

namespace LsPay.Client.Function.Code
{
    public class TLVHelper
    {

        /// <summary>
        /// 根据tag获取tlv的值
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static TLVEntity GetValueByTag(List<TLVEntity> entities, string tag)
        {
            TLVEntity resultEntity = null;
            var query = entities.SingleOrDefault(e => CodeConvert.ToHexString(e.Tag).ToUpper() == tag);
            if (query == null)
            {
                foreach (var tlv in entities)
                {
                    if (tlv.SubTLVEntity != null)
                    {
                        TLVEntity result = GetValueByTag(tlv.SubTLVEntity, tag);

                        if (result !=null && result.Length.Length > 0)
                            return result;
                    }
                }
            }
            else
                resultEntity = query;
            return resultEntity;
        }
        /// <summary>
        /// 16进制数据转化为TVL实体
        /// </summary>
        /// <param name="resultData"></param>
        /// <returns></returns>
        public static List<TLVEntity> ToTLVEntityList(string data)
        {
            byte[] dataBytes = CodeConvert.HexStringToByteArray(data);
            var tlvList = TLVPackage.Construct(dataBytes);
            return tlvList;
        }
        
    }
}
