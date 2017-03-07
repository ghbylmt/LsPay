using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    public class MessageHead : IMessageSnippet {
        private string content;
        private IFormatter formatter;

        public MessageHead() {
            formatter = new RightBcdFormatter();
        }

        #region IMessageSnippet Members

        public string Content {
            get {
                if (string.IsNullOrEmpty(content)) {
                    throw new ArgumentException("报文头未赋值。");
                }
                return content;
            }
            set {
                if (value.Length != 12) {
                    throw new ArgumentException("报文头长度不正确。");
                }
                content = value;
            }
        }

        public int ContentLen {
            get { return 12; }
        }

        public byte[] Pack() {
            if (string.IsNullOrEmpty(content)) {
                throw new ArgumentException("报文头未赋值。");
            }

            return formatter.GetBytes(content);
        }

        public int PackLen {
            get { return 6; }
        }

        public int Unpack(byte[] msg, int startIndex) {
            content = formatter.GetString(msg.SubArray(startIndex, PackLen));
            return PackLen;
        }

        #endregion
    }
}
