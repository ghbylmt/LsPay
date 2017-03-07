/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：字符转换操作类
 * 文件：CodeConvert.cs
 * 类名：CodeConvert
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

namespace LsPay.Client.Function.Code
{
    /// <summary>
    /// 字符转换操作类
    /// </summary>
    public class CodeConvert
    {
        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
        }
        public static string ToHexString(byte b)
        {
            return BitConverter.ToString(new byte[] { b }).Replace("-", "");
        }
        /// <summary>
        /// 16进制字符串转byte数组
        /// </summary>
        /// <param name="hexstr">16机制字符串</param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexstr)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < hexstr.Length / 2; i++)
            {
                bytes.Add(Convert.ToByte(Int32.Parse(hexstr.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber)));
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 转化成TL格式的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> ToTLStringList(string str) 
        {
           byte[] bytes = HexStringToByteArray(str);
           return ToTLStringList(bytes);
        }
        /// <summary>
        /// 转化成TL格式的字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static List<string> ToTLStringList(byte[] bytes) 
        {
            List<string> list = new List<string>();
            for (int i = 0; i < bytes.Length; i++)
            {
                List<byte> pdo = new List<byte>();
                if ((bytes[i] & 0xF) == 0xF)//判断tag是否占两位
                    pdo.AddRange(new byte[] { bytes[i++], bytes[i++] });
                else
                    pdo.Add(bytes[i++]);
                if ((bytes[i] & 0x80) == 0x80)//判读length是否占两位
                    pdo.AddRange(new byte[] { bytes[i++], bytes[i++] });
                else
                    pdo.Add(bytes[i]);
                list.Add(CodeConvert.ToHexString(pdo.ToArray()));
            }
            return list;
        }
    }
}
