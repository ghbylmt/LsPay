using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.micropay
{
    /// <summary>
    /// 刷卡支付
    /// </summary>
    [DataContract]
    public class MicropayModel
    {
        /// <summary>
        /// 商品描述
        /// 商品或支付单简要描述
        /// </summary>
        [DataMember]
        public string body { get; set; }
        /// <summary>
        /// 商品详情
        /// 商品名称明细列表
        /// </summary>
        [DataMember]
        public string detail { get; set; }
        /// <summary>
        /// 附加数据
        /// 附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        [DataMember]
        public string attach { get; set; }
        /// <summary>
        /// 货币类型
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        [DataMember]
        public string fee_type { get; set; }
        /// <summary>
        /// 总金额
        /// 订单总金额，单位为分
        /// </summary>
        [DataMember]
        public int total_fee { get; set; }
        /// <summary>
        /// 终端ip
        /// APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        [DataMember]
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 商品标记
        /// </summary>
        [DataMember]
        public string goods_tag { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>
        [DataMember]
        public string notify_url { get; set; }
        /// <summary>
        /// 交易类型
        /// SAPI--公众号支付
        /// NATIVE--原生扫码支付
        /// APP--app支付，统一下单接口trade_type的传参可参考这里
        /// MICROPAY--刷卡支付，刷卡支付有单独的支付接口，不调用统一下单接口
        /// </summary>
        [DataMember]
        public string trade_type { get; set; }
        /// <summary>
        /// 授权码
        /// 扫码支付授权码，设备读取用户微信中的条码或者二维码信息
        /// </summary>
        [DataMember]
        public string auth_code { get; set; }

        /// <summary>
        /// 终端设备号(商户自定义，如门店编号)
        /// </summary>
        [DataMember]
        public string device_info { get; set; }
    }
}
