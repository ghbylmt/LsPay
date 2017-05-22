using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response
{
    public class RefundResponse : BaseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string fun { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public int refund_fee { get; set; }
        /// <summary>
        /// 付款账号
        /// </summary>
        public string buyer_login_id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 实际退款金额
        /// </summary>
        public int send_back_fee { get; set; }
        /// <summary>
        /// 金额变动 
        /// Y/N
        /// </summary>
        public string fund_change { get; set; }
        /// <summary>
        /// 门店名称
        /// 商家注册的名称
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 支付宝流水号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>
        public string gmt_refund_pay { get; set; }
        /// <summary>
        /// 通道
        /// </summary>
        public PayChannel channel { get; set; }
    }
}
