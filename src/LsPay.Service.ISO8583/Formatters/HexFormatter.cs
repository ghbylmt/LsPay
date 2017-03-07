using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583.Formatters {
    public class HexFormatter : IFormatter {
        #region IFormatter Members

        public byte[] GetBytes(string value) {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < value.Length / 2; i++)
            {
                bytes.Add(Convert.ToByte(Int32.Parse(value.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber)));
            }
            return bytes.ToArray();
        }

        public string GetString(byte[] data) {
            string resultString = "";
            for (int i = 0; i < data.Length; i++)
            {
                resultString += ((int)data[i]).ToString("x2").ToUpper();
            }
            return resultString;
        }

        public int GetPackedLength(int unpackedLength) {
            return unpackedLength;
        }

        #endregion
    }
}
