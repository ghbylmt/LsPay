using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583.Formatters {
    public class RightBcdFormatter : IFormatter {
        #region IFormatter Members

        public byte[] GetBytes(string value) {
            if (value.Length % 2 == 1) {
               value = value.PadLeft(value.Length + 1, '0');
            }
            byte[] bs = Encoding.ASCII.GetBytes(value);
            int len = bs.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++) {
                byte high = (byte)(bs[i * 2] % 16);
                byte low = (byte)(bs[i * 2 + 1] % 16);
                bytes[i] = (byte)((byte)(high << 4) | low);
            }
            //var chars = value.ToCharArray();
            //var len = chars.Length / 2;
            //var bytes = new byte[len];

            //for (var i = 0; i < len; i++) {
            //    var highNibble = byte.Parse(chars[2 * i].ToString());
            //    var lowNibble = byte.Parse(chars[2 * i + 1].ToString());
            //    bytes[i] = (byte)((byte)(highNibble << 4) | lowNibble);
            //}

            return bytes;
        }

        public string GetString(byte[] data) {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public int GetPackedLength(int unpackedLength) {
            double len = unpackedLength;
            return (int)Math.Ceiling(len / 2);
        }

        #endregion
    }
}
