using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.agreements.ISO7816
{
    /// <summary>
    /// APDU报文
    /// </summary>
    public class APDUEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cla"></param>
        /// <param name="ins"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="body"></param>
        public APDUEntity(string cla, string ins, string p1, string p2, string body)
        {
            this.CLA = cla;
            this.INS = ins;
            this.P1 = p1;
            this.P2 = p2;
            this.Body = body;
        }

        #region Head

        public string CLA { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        public string INS { get; set; }
        /// <summary>
        /// P1参数
        /// </summary>

        public string P1 { get; set; }
        /// <summary>
        /// P2参数
        /// </summary>
        public string P2 { get; set; }
        #endregion

        /// <summary>
        /// 报文
        /// </summary>
        public string Body { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}", CLA, INS, P1, P2, Body);
        }
    }

}
