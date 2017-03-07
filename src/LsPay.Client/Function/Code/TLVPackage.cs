/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：TLV格式报文打包解析
 * 文件：TLVPackage.cs
 * 类名：TLVPackage
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
    /// <summary>
    /// TLV格式报文打包解析
    /// </summary>
    public class TLVPackage
    {
        #region TLV 打包
		
        /// <summary>
        /// TLV报文打包
        /// </summary>
        /// <param name="buffer">字节数据</param>
        /// <returns></returns>
        public static List<TLVEntity> Construct(byte[] buffer)
        {
            List<TLVEntity> resultList = new List<TLVEntity>();
            int currentIndex = 0;
            while (currentIndex < buffer.Length)
            {
                TLVEntity entity = new TLVEntity();
                //1. 根据Tag判断数据是否是嵌套的TLV
                bool hasSubEntity = HasSubEntity(buffer, currentIndex);

                #region Tag解析
                entity.Tag = GetTag(buffer, currentIndex);
                currentIndex += entity.Tag.Length;
                #endregion

                #region Length解析
                entity.Length = GetLength(buffer, currentIndex);
                currentIndex += entity.Length.Length;
                #endregion

                #region Value解析
                int valueLength = GetValueLengthByLengthByteValue(entity.Length);
                entity.Value = buffer.Take(currentIndex + valueLength).Skip(currentIndex).ToArray();
                if (hasSubEntity)//判断是否是嵌套结构
                    entity.SubTLVEntity = Construct(entity.Value);//嵌套结构递归解析
                currentIndex += entity.Value.Length;
                #endregion

                resultList.Add(entity);
            }
            return resultList;
        }

        /// <summary>
        /// 是否存在嵌套实体
        /// </summary>
        /// <returns></returns>
        private static bool HasSubEntity(byte[] bytes, int index)
        {
            if (bytes.Length < index + 1)
                throw new ArgumentException("无效的索引值");
            return (bytes[index] & 0x20) == 0x20;
        }

        /// <summary>
        /// 获取Tag字节数据
        /// </summary>
        /// <param name="bytes">长度</param>
        /// <param name="index">索引位置</param>
        /// <returns></returns>
        private static byte[] GetTag(byte[] bytes, int index)
        {
            if (bytes.Length < index + 1)
                throw new ArgumentException("无效的索引值");
            //判断Tag所占字节长度
            if ((bytes[index] & 0x1f) == 0x1f)
            {//占2字节
                return new byte[] { bytes[index], bytes[index + 1] };
            }
            else
            {//占1字节
                return new byte[] { bytes[index] };
            }
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="bytes">长度</param>
        /// <param name="index">索引位置</param>
        /// <returns></returns>
        private static byte[] GetLength(byte[] bytes, int index)
        {
            if (bytes.Length < index + 1)
                throw new ArgumentException("无效的索引值");
            //判断Length部分所占字节 是1个字节还是多个字节
            if ((bytes[index] & 0x80) == 0x80)
            {//占多个字节
                int lengthSize = (bytes[index] & 0x7f) + 1;//获取Length所占字节数
                return bytes.Take(index + lengthSize).Skip(index).ToArray();
            }
            else
            {//占单个字节
                return new byte[] { bytes[index] };
            }
        }
        /// <summary>
        /// 根据Length部分的值获取到value部分的值
        /// </summary>
        /// <param name="bytes">Length部分的值</param>
        /// <returns></returns>
        private static int GetValueLengthByLengthByteValue(byte[] bytes)
        {
            int length = 0;
            if (bytes.Length == 1)
                length = bytes[0];
            else
            {
                //从下一个字节开始算Length域
                for (int index = 1; index < bytes.Length; index++)
                {
                    length += bytes[index] << ((index-1) * 8); //计算Length域的长度
                }
            }
            return length;
        }
 
	#endregion

        #region TLV 解析
        /// <summary>
        /// 解析TLV
        /// </summary>
        /// <param name="list">
        /// <returns></returns>
        public static byte[] Parse(List<TLVEntity> list)
        {
            byte[] buffer = new byte[4096];
            int currentIndex = 0;
            int currentTLVIndex = 0;
            int valueSize = 0;

            while (currentTLVIndex < list.Count())
            {
                valueSize = 0;
                TLVEntity entity = list[currentTLVIndex];

                Array.Copy(entity.Tag, 0, buffer, currentIndex, entity.TagSize);    //解析Tag

                currentIndex += entity.TagSize;

                for (int index = 0; index < entity.LengthSize; index++)
                {
                    valueSize += entity.Length[index] << (index * 8); //计算Length域的长度
                }
                if (valueSize > 127)
                {
                    buffer[currentIndex] = Convert.ToByte(0x80 | entity.LengthSize);
                    currentIndex += 1;
                }

                Array.Copy(entity.Length, 0, buffer, currentIndex, entity.LengthSize);  //解析Length

                currentIndex += entity.LengthSize;
                //判断是否包含子嵌套TLV
                if (entity.SubTLVEntity == null)
                {
                    Array.Copy(entity.Value, 0, buffer, currentIndex, valueSize);   //解析Value
                    currentIndex += valueSize;
                }
                else
                {
                    byte[] tempBuffer = Parse(entity.SubTLVEntity);
                    Array.Copy(tempBuffer, 0, buffer, currentIndex, tempBuffer.Length); //解析子嵌套TLV
                    currentIndex += tempBuffer.Length;
                }

                currentTLVIndex++;
            }

            byte[] resultBuffer = new byte[currentIndex];
            Array.Copy(buffer, 0, resultBuffer, 0, currentIndex);

            return resultBuffer;
        } 
        #endregion
    }
}
