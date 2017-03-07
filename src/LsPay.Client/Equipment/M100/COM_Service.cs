/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：串口访问服务类
 * 文件：COM_Receive.cs
 * 类名：COM_Receive
 * 命名空间：LsPay.Client.Equipment
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using LsPay.Client.Function.Extension;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// 串口访问服务
    /// </summary>
    public class COM_Receive
    {
        [ThreadStatic]
        private SerialPort port;
        private byte[] execResult;
        private byte[] cache;
        private int pos;
        private bool IsOver;
        private bool Listening = false;
        private bool Closing = false;
        private object locker = new object();

        public COM_Receive(string COM, int baudRate = 9600)
        {
            port = new SerialPort(COM, baudRate);
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.ReceivedBytesThreshold = 1;
            cache = new byte[1024];
            pos = 0;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Listening = true;
                if (port == null || !port.IsOpen)
                {
                    throw new ArgumentException("操作执行失败！串口未打开！");
                }
                int receivedBytesCount = port.BytesToRead;
                byte[] data = new byte[receivedBytesCount];
                port.Read(data, 0, receivedBytesCount);
                if (data.Length == 1 && data[0] == 0x06 && pos == 0)
                {
                    port.Write(new byte[] { 0x05 }, 0, 1);
                    return;
                }

                data.CopyTo(cache, pos);
                pos += data.Length;

                IsOver = pos > 1 &&
                    cache[0] == 0x02
                    && cache[pos - 2] == 0x03
                    && cache[1] * 16 * 16 + cache[2] == pos - 5
                    && cache[pos - 1] == cache.GetSubArray(0, pos - 1).Aggregate((curr, next) => curr ^= next);

            }
            finally
            {
                Listening = false;//可以关闭串口了。  
            }
        }

        public byte[] SendCmd(byte[] cmd)
        {
            int timeout = 0;
            pos = 0;
            IsOver = false;
            if (port == null || !port.IsOpen)
            {
                throw new ArgumentException("操作执行失败！串口未打开！");
            }
            port.Write(cmd, 0, cmd.Count());
            while (!IsOver)
            {
                timeout += 100;
                Thread.Sleep(100);
                if (timeout >= 5000)
                {
                    throw new ArgumentException("接收数据超时！");
                }
            }
            return cache.GetSubArray(0, pos);
        }

        public void OpenPort()
        {
            while (Closing) { }//正在关闭等待关闭
            if (!port.IsOpen)
            {
                port.Open();
            }
        }

        public void ClosePort()
        {
            while (Listening || !IsOver) { } //串口正在监听或数据未接收完成，继续等待结束后关闭
            Closing = true;
            port.Close();
            Closing = false;
        }

        public bool IsOpen { get { return port.IsOpen; } }
    }
}
