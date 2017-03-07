using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583 {
    public class MsgField {
        private int contentLen;
        private IFormatter formatter;
        private string content;
        private FieldLenIndicator indicator;

        public MsgField(int contentLen, IFormatter formatter) {
            this.contentLen = contentLen;
            this.formatter = formatter;
        }

        public MsgField(FieldLenIndicator indicator, IFormatter formatter) {
            this.contentLen = Convert.ToInt32(indicator.Content);
            this.formatter = formatter;
            this.indicator = indicator;
        }

        public void SetValue(string content) {
            if (content.Length != contentLen) {
                throw new ArgumentException(string.Format("内容长度不正确：设定值->{0}，传入值->{1}。", contentLen, content.Length));
            }
            this.content = content;
        }

        public void SetValue(byte[] msg) {
            if (indicator == null) {
                content = formatter.GetString(msg);
            } else {
                
            }
        }
    }
}
