using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    public class Field : IMessageSnippet {
        private int fieldLen;
        private IFormatter formatter;
        private FieldLenIndicator fieldLenIndicator;
        private string content;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldLen">域内容长度</param>
        /// <param name="formatter"></param>
        public Field(int fieldLen, IFormatter formatter) {
            this.fieldLen = fieldLen;
            this.formatter = formatter;
        }

        public Field(FieldLenIndicator fieldLenIndicator, IFormatter formatter) {
            this.fieldLenIndicator = fieldLenIndicator;
            this.formatter = formatter;
        }

        public FieldLenIndicator FieldLenIndicator {
            get { return fieldLenIndicator; }
            set { this.fieldLenIndicator = value; }
        }

        #region IMessageSnippet Members

        public string Content {
            get {
                if (formatter is RightBcdFormatter && fieldLenIndicator != null) {
                    return content.Substring(content.Length - Convert.ToInt32(fieldLenIndicator.Content), Convert.ToInt32(fieldLenIndicator.Content));
                }
                if (formatter is LeftBcdFormatter && fieldLenIndicator != null) {
                    return content.Substring(0, Convert.ToInt32(fieldLenIndicator.Content));
                }
                return content;
            }
            set {
                this.content = value;
            }
        }

        public int ContentLen {
            get { return fieldLenIndicator == null ? fieldLen : Convert.ToInt32(fieldLenIndicator.Content); }
        }

        public byte[] Pack() {
            try {
                byte[] result = new byte[PackLen];
                int pos = 0;
                if (fieldLenIndicator != null) {
                    fieldLenIndicator.Pack().CopyTo(result, pos);
                    pos += fieldLenIndicator.PackLen;
                }
                formatter.GetBytes(content).CopyTo(result, pos);
                return result;
            } catch (Exception ex) { throw new ArgumentException(PackLen.ToString() + " " + content); }
        }

        public int PackLen {
            get { return fieldLenIndicator == null ? formatter.GetPackedLength(ContentLen) : formatter.GetPackedLength(ContentLen) + fieldLenIndicator.PackLen; }
        }

        public int Unpack(byte[] msg, int startIndex) {
            if (fieldLenIndicator == null) {
                content = formatter.GetString(msg.SubArray(startIndex, formatter.GetPackedLength(fieldLen)));
                return PackLen;
            }
            fieldLenIndicator.Unpack(msg, startIndex);
            startIndex += fieldLenIndicator.PackLen;
            content = formatter.GetString(msg.SubArray(startIndex, PackLen - fieldLenIndicator.PackLen));
            return PackLen;
        }

        #endregion
    }
}
