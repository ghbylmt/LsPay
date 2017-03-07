using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Exception
{
    /// <summary>
    /// 卡片读取异常
    /// </summary>
    public class CardReadException : ApplicationException
    {
        public CardReadException(string Message):
            base(Message)
        {
        }
    }
}
