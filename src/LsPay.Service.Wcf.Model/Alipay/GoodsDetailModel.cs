using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay
{
    /// <summary>
    /// 商品明细数据传输模型
    /// </summary>
    [DataContract]
    public class GoodsDetailModel
    {
        
        /// <summary>
        /// 商品编号
        /// String(32)
        /// </summary>
        [DataMember]
        public string goods_id { get; set; }
        /// <summary>
        /// 支付宝统一的商品编号
        /// </summary>
        [DataMember]
        public string alipay_goods_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string goods_name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember]
        public string quantity { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        [DataMember]
        public string price { get; set; }
        /// <summary>
        /// 商品类目
        /// </summary>
        [DataMember]
        public string goods_category { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [DataMember]
        public string body { get; set; }
    }
}
