using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Alipay
{
    /// <summary>
    /// 预下单请求数据传输模型
    /// </summary>
    [DataContract]
    public class TradepayModel
    {
        /// <summary>
        /// 商户订单号
        /// 必填
        /// String(64)
        /// 商户订单号,64个字符以内、可包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付场景
        /// String(32)
        /// 条码支付，取值：bar_code
        /// </summary>
        [DataMember]
        public string scene { get; set; }
        /// <summary>
        /// 支付授权码
        /// String(32)
        /// 用户手机支付宝中的“付款码”信息
        /// </summary>
        [DataMember]
        public string auth_code { get; set; }
        /// <summary>
        /// 订单总金额
        /// String(9)
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]。
        /// 如果同时传入【可打折金额】和【不可打折金额】，该参数可以不用传入；
        /// 如果同时传入了【可打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【可打折金额】+【不可打折金额】
        /// </summary>
        [DataMember]
        public string total_amount { get; set; }
        /// <summary>
        /// 可打折金额
        /// String(9)
        /// 参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]。
        /// 如果该值未传入，但传入了【订单总金额】和【不可打折金额】，则该值默认为【订单总金额】-【不可打折金额】
        /// </summary>
        [DataMember]
        public string discountable_amount { get; set; }
        /// <summary>
        /// 不可打折金额
        /// String(9)
        /// 不参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]。
        /// 如果该值未传入，但传入了【订单总金额】和【可打折金额】，则该值默认为【订单总金额】-【可打折金额】
        /// </summary>
        [DataMember]
        public string undiscountable_amount { get; set; }
        /// <summary>
        /// 订单标题
        /// 必填
        /// String(256)
        /// 订单标题
        /// </summary>
        [DataMember]
        public string subject { get; set; }
        /// <summary>
        /// 订单描述
        /// String(128)
        /// 对交易或商品的描述
        /// </summary>
        [DataMember]
        public string body { get; set; }
        /// <summary>
        /// 商品明细列表
        /// </summary>
        [DataMember]
        public List<GoodsDetailModel> goods_detail { get; set; }
        /// <summary>
        /// 商户操作员编号
        /// </summary>
        [DataMember]
        public string operator_id { get; set; }
        /// <summary>
        /// 商户门店编号
        /// </summary>
        [DataMember]
        public string store_id { get; set; }
        /// <summary>
        /// 扩展参数
        /// String(512)
        /// 业务扩展参数，sys_service_provider_id：系统商编号。
        /// 注意： 该参数作为系统商返佣数据提取的依据，请填写系统商签约协议的PID
        /// </summary>
        [DataMember]
        public string terminal_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string extend_params { get; set; }
        /// <summary>
        /// 支付超时时间表达式
        /// 该笔订单允许的最晚付款时间，逾期将关闭交易。
        /// 取值范围：1m～15d。m-分钟，h-小时，d-天，1c-当天（1c-当天的情况下，无论交易何时创建，都在0点关闭）。 
        /// 该参数数值不接受小数点， 如 1.5h，可转换为 90m
        /// </summary>
        [DataMember]
        public string timeout_express { get; set; }
        /// <summary>
        /// 分账信息
        /// </summary>
        [DataMember]
        public string royalty_info { get; set; }


    }
}
