using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    /// <summary>
    /// 域长度指示器，用于长度可变的域。
    /// </summary>
    public class FieldLenIndicator : IMessageSnippet {
        /// <summary>
        /// 压缩前的域头长度，2/3。
        /// </summary>
        private int len;
        /// <summary>
        /// 域内容长度，即域头的内容。
        /// </summary>
        private string fieldLen;
        private IFormatter formatter;

        public FieldLenIndicator(int len) {
            this.len = len;
            this.formatter = new RightBcdFormatter();
        }

        #region IMessageSnippet Members

        public string Content {
            get {
                return fieldLen;
            }
            set {
                //if (value.Length != len) {
                //    throw new ArgumentException("内容长度不正确。");
                //}
                this.fieldLen = value;
            }
        }

        public int ContentLen {
            get { return len; }
        }

        public byte[] Pack() {
            return formatter.GetBytes(Content);
        }

        public int PackLen {
            get { return (int)Math.Ceiling(len / 2.0); }
        }

        public int Unpack(byte[] msg, int startIndex) {
            fieldLen = formatter.GetString(msg.SubArray(startIndex, PackLen));
            return PackLen;
        }

        #endregion
    }
}
