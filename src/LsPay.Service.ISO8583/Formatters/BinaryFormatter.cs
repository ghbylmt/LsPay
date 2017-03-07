using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583.Formatters {
    public class BinaryFormatter : IFormatter {
        #region IFormatter Members

        public byte[] GetBytes(string value) {
            int numberChars = value.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }

        public string GetString(byte[] data) {
            string hex = BitConverter.ToString(data);
            return hex.Replace("-", string.Empty);
        }

        public int GetPackedLength(int unpackedLength) {
            //if (unpackedLength % 2 != 0) {
            //    unpackedLength++;
            //}
            return unpackedLength;
        }

        #endregion
    }
}
