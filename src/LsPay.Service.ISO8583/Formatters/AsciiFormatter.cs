using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583.Formatters {
    public class AsciiFormatter : IFormatter {
        #region IFormatter Members

        public byte[] GetBytes(string value) {
            return Encoding.ASCII.GetBytes(value);
        }

        public string GetString(byte[] data) {
            return Encoding.ASCII.GetString(data);
        }

        public int GetPackedLength(int unpackedLength) {
            return unpackedLength;
        }

        #endregion
    }
}
