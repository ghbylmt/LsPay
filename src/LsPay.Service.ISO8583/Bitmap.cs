using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583 
{
    public class Bitmap : IMessageSnippet {
        private bool[] bitmap;
        private int len;

        public Bitmap(bool includeExtension = false) {
            len = includeExtension ? 128 : 64;
            bitmap = new bool[len];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field">索引值为位图序号。</param>
        /// <returns></returns>
        public bool this[int field] {
            get { return bitmap[field - 1]; }
            set { bitmap[field - 1] = value; }
        }


        #region IMessageSnippet Members

        public string Content {
            get {
                StringBuilder s = new StringBuilder();
                for (int i = 0; i < len; i++) {
                    s.Append(bitmap[i] ? "1" : "0");
                }
                return s.ToString();
            }
            set {
                throw new ArgumentException("不能直接对位图赋值。");
            }
        }

        public int ContentLen {
            get { return len; }
        }

        public byte[] Pack() {
            byte[] data = new byte[PackLen];
            for (int i = 0; i < data.Length; i++) {
                for (int j = 0; j < 8; j++) {
                    data[i] += (byte)(bitmap[i * 8 + j] ? Math.Pow(2, 7 - j) : 0);
                }
            }
            return data;
        }

        public int PackLen {
            get { return len / 8; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public int Unpack(byte[] msg, int startIndex) {
            for (int i = 0; i < PackLen; i++) {
                for (int j = 0; j < 8; j++) {
                    if (msg[startIndex + i] >= Math.Pow(2, 7 - j)) {
                        bitmap[i * 8 + j] = true;
                        msg[startIndex + i] -= (byte)Math.Pow(2, 7 - j);
                    }
                }
            }
            return PackLen;
        }

        #endregion
    }
}
