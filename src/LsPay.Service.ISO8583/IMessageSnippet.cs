using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583 {
    /// <summary>
    /// 消息片段。
    /// </summary>
    public interface IMessageSnippet {
        string Content { get; set; }
        int ContentLen { get; }
        byte[] Pack();
        int PackLen { get; }
        int Unpack(byte[] msg, int startIndex);
    }
}
