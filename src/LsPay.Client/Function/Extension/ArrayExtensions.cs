using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Function.Extension
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// 获取字节数组的子数组。
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>子数组。</returns>
        public static byte[] GetSubArray(this byte[] source, Int32 startIndex, Int32 length = 0)
        {
            if (startIndex < 0 || startIndex > source.Length || length < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (length == 0)
            {
                length = source.Length - startIndex;
            }
            byte[] result;
            if (startIndex + length <= source.Length)
            {
                result = new byte[length];
                Array.Copy(source, startIndex, result, 0, length);
            }
            else
            {
                result = new byte[source.Length - length];
                Array.Copy(source, startIndex, result, 0, source.Length - startIndex);
            }
            return result;
        }

        public static int IndexOf(this byte[] source, byte b, int th)
        {
            if (source == null)
            {
                throw new ArgumentException("数据源不能为null。");
            }
            int result = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == b)
                {
                    result++;
                    if (result == th)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pin">123456</param>
        /// <param name="pan">305001225570</param>
        /// <returns></returns>
        public static byte[] PinBlock(string pin, string pan)
        {
            byte[] first = new byte[8];

            byte[] second = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                first[i] ^= second[i];
            }
            return first;
        }
    }
}
