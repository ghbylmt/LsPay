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
        public string total_amount { get; set; }
        /// <summary>
        /// 门店号
        /// </summary>
        public string store_id { get { return Config.Store_id; } }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string channel { get; set; }
        /// <summary>
        /// 收银员编号
        /// </summary>
        public string operatore_id { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string subject { get; set; }
        ///// <summary>
        ///// 产品金额
        ///// 单位：分【翼支付需要提供】
        ///// </summary>

        //public int product_amount { get; set; }
        ///// <summary>
        ///// 附加金额
        ///// 单位：分【翼支付需要提供】
        ///// </summary>
        //public int attach_amount { get; set; }
        /// <summary>
        /// 不打折金额
        /// 单位：分【支付宝需要提供】
        /// </summary>
        public string undiscountable_amount { get; set; }
        /// <summary>
        /// 打折金额
        /// 单位：分【支付宝需要提供】
        /// </summary>
        public string discountable_amount { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
