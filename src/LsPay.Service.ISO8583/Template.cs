using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.ISO8583
{
    /// <summary>
    /// 8583报文模板
    /// </summary>
    public class Template
    {
        private static Dictionary<int, Field> _instance;
        public static Dictionary<int, Field> Instance
        {
            get
            {
                //if (_instance == null)
                    //throw new ApplicationException("未定义ISO8583协议的模板数据");
                return _instance;
            }
            set { _instance = value; }
        }
    }
}
