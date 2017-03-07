using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    public class FieldIndicator {
        private int indicatorLen;
        private IFormatter formatter;
        private string content;
        private FieldLenIndicator indicator;
        private byte[] msg;
        private int packLen;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indicatorLen">指示器压缩前的长度，2/3</param>
        public FieldIndicator(int indicatorLen) {
            this.indicatorLen = indicatorLen;
            packLen = (indicatorLen % 2) + 1;
            this.msg = new byte[packLen];
            this.formatter = new RightBcdFormatter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldLen">压缩前域的长度。</param>
        public void SetValue(int fieldLen) {
            this.content = fieldLen.ToString();
            byte[] temp = formatter.GetBytes(content);
            temp.CopyTo(msg, msg.Length - temp.Length);
        }

        public void SetValue(byte[] source, int start) {
            msg = source.SubArray(start, packLen);
            content = formatter.GetString(msg);
        }
    }
}
