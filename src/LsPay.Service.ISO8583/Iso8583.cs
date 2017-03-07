using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LsPay.Service.ISO8583 {
    public class Iso8583 : SortedDictionary<int, Field>, IMessageSnippet {
        private Bitmap map;
        private int len;

        public Iso8583() {

        }

        public Iso8583(Bitmap map) {
            this.map = map;
            len = map.PackLen;
            for (int i = 0; i < map.ContentLen; i++) {
                if (map[i + 1]) {
                    this.Add(i + 1, Template.Instance[i + 1]);
                }
            }
        }

        public void SetField(int fieldNum, Field field) {
            if (!this.Keys.Contains(fieldNum)) {
                throw new ArgumentException(string.Format("模板中不包含第{0}域。", fieldNum));
            }
            this[fieldNum] = field;
        }

        #region IMessageSnippet Members

        public string Content {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public int ContentLen {
            get { throw new NotImplementedException(); }
        }

        public byte[] Pack() {
            if (this.Values.Contains(null)) {
                throw new ArgumentException("模板中有未赋值的域。");
            }
            foreach (var item in this) {
                len += item.Value.PackLen;
            }
            byte[] result = new byte[len];
            int pos = 0;
            map.Pack().CopyTo(result, pos);
            pos += map.PackLen;
            foreach (var item in this) {
                byte[] b = item.Value.Pack();
                b.CopyTo(result, pos);
                pos += b.Length;
            }
            return result;
        }

        public int PackLen {
            get {
                int result = 0;
                result += map.PackLen;
                foreach (var item in this.Values) {
                    result += item.PackLen;
                }
                return result;
            }
        }

        public int Unpack(byte[] msg, int startIndex) {
            int pos = startIndex;
            map = new Bitmap(msg[pos] >= Math.Pow(2, 7));
            pos += map.Unpack(msg, pos);
            for (int i = 1; i < map.ContentLen; i++) {
                if (map[i + 1]) {
                    this.Add(i + 1, Template.Instance[i + 1]);
                    pos += this[i + 1].Unpack(msg, pos);
                }
            }
            return pos - startIndex;
        }

        #endregion
    }
}
