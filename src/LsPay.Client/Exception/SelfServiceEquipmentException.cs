using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Exception
{
    /// <summary>
    /// 自助设备异常
    /// </summary>
    public class SelfServiceEquipmentException : ApplicationException
    {
        public SelfServiceEquipmentException(string Message) :
            base(Message)
        {
        }
    }
}
