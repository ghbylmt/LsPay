using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class QueryResponse
    {
        #region 基础响应
        /// <summary>
        /// 是否查询到订单
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 响应码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 响应描述
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 退款标识
        /// 6代表为退款，4 已退款5部分退款 7已冲正
        /// </summary>
        public int refundFlag { get; set; }
        #endregion

        #region success:true
        /// <summary>
        /// 商户号
        /// </summary>
        public decimal merchant_id { get; set; }
        public decimal out_trade_no { get; set; }
        public int channel { get; set; }
        public string orderDate { get; set; }
        /// <summary>
        /// 网关平台流水号
        /// </summary>
        public string trade_no { get; set; }
        public string terminal_id { get; set; }
        public string operator_id { get; set; }
        public string gmt_payment { get; set; }
        public string buyer_logon_id { get; set; }
        public string total_amount { get; set; }
        public string receipt_amount { get; set; }
        public string fund_bill_list { get; set; }
        public string trans_status { get; set; }
        public string sign { get; set; }
        //public int refundFlag { get; set; }
        public string fun { get; set; }
        #endregion
    }
}
