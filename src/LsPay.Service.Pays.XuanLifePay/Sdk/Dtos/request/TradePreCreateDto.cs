using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class TradePreCreateDto : TradePayBaseDto
    {
        /// <summary>
        /// 订单总额
        /// </summary>
        public int total_amount { get; set; }
        /// <summary>
        /// 门店号
        /// </summary>
        public int store_id { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public PayChannel channel { get; set; }
        /// <summary>
        /// 收银员编号
        /// </summary>
        public int operatore_id { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 产品金额
        /// 单位：分【翼支付需要提供】
        /// </summary>

        public int product_amount { get; set; }
        /// <summary>
        /// 附加金额
        /// 单位：分【翼支付需要提供】
        /// </summary>
        public int attach_amount { get; set; }
        /// <summary>
        /// 不打折金额
        /// 单位：分【支付宝需要提供】
        /// </summary>
        public int undiscountable_amount { get; set; }
        /// <summary>
        /// 打折金额
        /// 单位：分【支付宝需要提供】
        /// </summary>
        public int discountable_amount { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
