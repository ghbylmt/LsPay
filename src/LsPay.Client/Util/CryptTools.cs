using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TTS.Core.ChinaUnionPay.Util
{
    /// <summary>
    /// 加密解密工具类
    /// </summary>
    public static class CryptTools
    {
        
        /// <summary>
        /// 使用TripleDES加密
        /// </summary>
        /// <param name="source">需要加密的明文字节数组</param>
        /// <param name="key">加密密钥字节数组</param>
        /// <returns>返回加密后密文字节数组</returns>
        /// <exception cref="ArgumentException"/>
        private static byte[] Crypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateEncryptor(key, null);

            return des.TransformFinalBlock(source, 0, source.Length);
        }

        /// <summary>
        /// 解密通过TripleDES算法加密后的密文字节数组
        /// </summary>
        /// <param name="source">需要解密的密文字节数组</param>
        /// <param name="key">解密密钥字节数组</param>
        /// <returns>返回解密后明文字节数组</returns>
        /// <exception cref="ArgumentNullException"/>
        private static byte[] Decrypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentNullException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateDecryptor(key, null);

            byte[] ret = new byte[source.Length + 8];

            int num;
            num = des.TransformBlock(source, 0, source.Length, ret, 0);

            ret = des.TransformFinalBlock(source, 0, source.Length);
            ret = des.TransformFinalBlock(source, 0, source.Length);
            num = ret.Length;

            byte[] RealByte = new byte[num];
            Array.Copy(ret, RealByte, num);
            ret = RealByte;
            return ret;
        }
    }
}
