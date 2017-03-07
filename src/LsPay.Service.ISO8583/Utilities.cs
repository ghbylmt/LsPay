using System;
using System.Net.Sockets;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583
{
    public static class Utilities
    {
        public static byte[] SubArray(this byte[] source, int startIndex, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = source[startIndex + i];
            }
            return result;
        }

        public static byte[] PinBlock(string pin, string pan)
        {
            IFormatter formatter = new RightBcdFormatter();

            byte[] first = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            first[0] = 0x06;
            formatter.GetBytes(pin).CopyTo(first, 1);
            byte[] second = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            formatter.GetBytes(pan).CopyTo(second, 2);
            for (int i = 0; i < 8; i++)
            {
                first[i] ^= second[i];
            }
            return first;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public static byte[] Send(string ip, int port, byte[] msg)
        {
            try
            {
                //LogUtil.WriteLog("请求", msg);
                TcpClient client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();
                client.ReceiveTimeout = 15000;
                client.SendTimeout = 15000;
                stream.Write(msg, 0, msg.Length);
                byte[] temp = new byte[1024];
                int len = stream.Read(temp, 0, temp.Length);
                byte[] result = new byte[len];
                temp.SubArray(0, len).CopyTo(result, 0);
                //LogUtil.WriteLog("响应", result);
                stream.Close();
                client.Close();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
