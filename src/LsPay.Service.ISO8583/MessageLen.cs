using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583 {
    public class MessageLen : IMessageSnippet {
        private int content;

        #region IMessageSnippet Members

        public string Content {
            get {
                return content.ToString();
            }
            set {
                content = Convert.ToInt32(value);
            }
        }

        public int ContentLen {
            get { return content.ToString().Length; }
        }

        public byte[] Pack() {
            byte[] result = new byte[2];
            result[0] = (byte)(content / 0x100);
            result[1] = (byte)(content % 0x100);
            return result;
        }

        public int PackLen {
            get { return 2; }
        }

        public int Unpack(byte[] msg, int startIndex) {
            content = msg[startIndex] * 0x100 + msg[startIndex + 1];
            return PackLen;
        }

        #endregion
    }
}
