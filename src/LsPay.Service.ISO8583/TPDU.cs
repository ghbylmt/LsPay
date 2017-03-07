using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    public class TPDU : IMessageSnippet {
        private string content;
        private IFormatter formatter;

        public TPDU() {
            formatter = new RightBcdFormatter();
        }

        #region IMessageSnippet Members

        public string Content {
            get {
                if (string.IsNullOrEmpty(content)) {
                    throw new ArgumentException("TPDU未赋值。");
                }
                return content;
            }
            set {
                if (value.Length != 10) {
                    throw new ArgumentException("TPDU长度不正确。");
                }
                content = value;
            }
        }

        public int ContentLen {
            get { return 10; }
        }

        public byte[] Pack() {
            if (string.IsNullOrEmpty(content)) {
                throw new ArgumentException("TPDU未赋值。");
            }
            return formatter.GetBytes(content);
        }

        public int PackLen {
            get { return 5; }
        }

        public int Unpack(byte[] msg, int startIndex) {
            content = formatter.GetString(msg.SubArray(startIndex, PackLen));
            return PackLen;
        }

        #endregion
    }
}
