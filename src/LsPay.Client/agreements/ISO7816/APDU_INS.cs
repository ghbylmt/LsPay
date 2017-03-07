using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.agreements.ISO7816
{

    /// <summary>
    /// APDU INS 指令
    /// </summary>
    public class APDU_INS
    {
        /// <summary>
        /// 选择文件
        /// </summary>
        public const string SELECT = "A4";
        /// <summary>
        /// 读取
        /// </summary>
        public const string READ_RECODE = "B2";
        /// <summary>
        /// 获取数据
        /// </summary>
        public const string GET_DATA = "CA";

    }
}
