using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583.Formatters {
    public interface IFormatter {
        byte[] GetBytes(string value);
        string GetString(byte[] data);
        int GetPackedLength(int unpackedLength);
    }
}
