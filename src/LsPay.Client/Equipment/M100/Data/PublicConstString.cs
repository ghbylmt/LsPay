using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Equipment.M100.Data
{
    /// <summary>
    /// 卡片类型
    /// </summary>
    public class CardType
    {
        /// <summary>
        /// 卡机内无卡
        /// </summary>
        public const string NoCard = "N0";
        /// <summary>
        /// 非接触式射频卡
        /// </summary>
        public const string ContactlessRFCard = "00";
        /// <summary>
        ///  T=0 接触式 CPU 卡 
        /// </summary>
        public const string CPU_T_0 = "10";

        /// <summary>
        ///  T=1 接触式 CPU 卡 
        /// </summary>
        public const string CPU_T_1 = "11";
        /// <summary>
        /// ISO1443 TYPE A CPU卡
        /// </summary>

        public const string CPU_TYPE_A = "04";
        /// <summary>
        /// ISO1443 TYPE B CPU卡
        /// </summary>
        public const string CPU_TYPE_B = "05";
    }
}
