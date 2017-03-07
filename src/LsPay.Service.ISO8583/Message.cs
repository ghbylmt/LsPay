using System;
using System.Net.Sockets;

namespace LsPay.Service.ISO8583 {
    public class Message {
        public MessageLen MessageLen { get; private set; }
        public TPDU TPDU { get; set; }
        public MessageHead MessageHead { get; set; }
        public MessageType MessageType { get; set; }
        private Iso8583 iso8583;
        private int len;

        public Message(Iso8583 iso8583) {
            this.iso8583 = iso8583;
            MessageLen = new MessageLen();
            TPDU = new TPDU();
            MessageHead = new MessageHead();
            MessageType = new MessageType();
        }

        public Iso8583 Iso8583 { get { return iso8583; } }

        public byte[] Pack() {
            len = MessageLen.PackLen + TPDU.PackLen + MessageHead.PackLen + MessageType.PackLen + iso8583.PackLen;
            MessageLen.Content = (len - 2).ToString();
            byte[] result = new byte[len];
            int pos = 0;
            MessageLen.Pack().CopyTo(result, pos);
            pos += MessageLen.PackLen;
            TPDU.Pack().CopyTo(result, pos);
            pos += TPDU.PackLen;
            MessageHead.Pack().CopyTo(result, pos);
            pos += MessageHead.PackLen;
            MessageType.Pack().CopyTo(result, pos);
            pos += MessageType.PackLen;
            byte[] tem = iso8583.Pack();
            tem.CopyTo(result, pos);
            pos += iso8583.PackLen;
           
            return result;
        }

        public int Unpack(byte[] msg) {
            int pos = 0;
            pos += MessageLen.Unpack(msg, pos);
            pos += TPDU.Unpack(msg, pos);
            pos += MessageHead.Unpack(msg, pos);
            pos += MessageType.Unpack(msg, pos);
            pos += iso8583.Unpack(msg, pos);
            return pos;
        }

        public byte[] Send(string ip, int port) {
            try {
                TcpClient client = new TcpClient(ip, port);
                client.SendTimeout = 1000;
                NetworkStream stream = client.GetStream();
                byte[] msg = Pack();
                stream.Write(msg, 0, msg.Length);
                //LogUtil.WriteLog("签到请求", msg);
                byte[] temp = new byte[1024];
                int len = stream.Read(temp, 0, temp.Length);

                byte[] result = new byte[len];
                temp.SubArray(0, len).CopyTo(result, 0);
                //LogUtil.WriteLog("签到响应",result);
                stream.Close();
                client.Close();
                return result;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public byte[] Send(string ip, int port, byte[] msg) {
            try
            {
                //LogUtil.WriteRequestLog(msg);
                TcpClient client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();
                client.ReceiveTimeout = 150000;
                client.SendTimeout  = 150000;
                stream.Write(msg, 0, msg.Length);
                byte[] temp = new byte[1024];
                int len = stream.Read(temp, 0, temp.Length);
                byte[] result = new byte[len];
                temp.SubArray(0, len).CopyTo(result, 0);
                //File.AppendAllText("Res.txt", BitConverter.ToString(result));
                //File.AppendAllText("Res.txt", "\r\n");
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
