using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    /// <summary>
    /// 统一下单数据传输类
    /// </summary>
    public class UnifiedOrderResponseModel : BaseBusinessResponseModel
    {
        /// <summary>
        /// 设备号
        /// 调用接口提交的终端设备号
        /// </summary>
        [DataMember]
        
        public string device_info { get; set; }
        /// <summary>
        /// 商户订单号【自定义，用于回调】
        /// 商户系统内部的订单号,32个字符内、可包含字母
        /// </summary>
        [DataMember]
        public string out_trade_no { get; set; }

        #region 以下字段在return_code 和result_code都为SUCCESS的时候有返回
        /// <summary>
        /// 交易类型
        /// JSAPI--公众号支付
        /// NATIVE--原生扫码支付
        /// APP--app支付
        /// 统一下单接口trade_type的传参可参考这里
        /// </summary>
        [DataMember]
        
        public string trade_type { get; set; }
        /// <summary>
        /// 预支付交易会话标识
        /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        [DataMember]        
        public string prepay_id { get; set; }
        /// <summary>
        /// 二维码链接
        /// trade_type为NATIVE是有返回，可将该参数值生成二维码展示出来进行扫码支付
        /// </summary>
        [DataMember]        
        public string code_url { get; set; }

        #endregion
    }
}
