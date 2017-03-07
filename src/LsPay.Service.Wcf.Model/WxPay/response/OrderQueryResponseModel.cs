using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    /// <summary>
    /// 订单查询响应数据传输类
    /// </summary>
    public class OrderQueryResponseModel : BaseBusinessResponseModel
    {
        #region 以下字段在return_code 和result_code都为SUCCESS的时候有返回
        /// <summary>
        /// 用户在商户appid 下的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户是否关注公众账号，仅在公众账号类型支付有效，
        /// 取值范围：Y或N;Y-关注;N-未关注
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 支付类型为MICROPAY(即扫码支付)
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 交易状态
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（刷卡支付）
        /// USERPAYING--用户支付中
        /// PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        public string trade_state { get; set; }
        /// <summary>
        /// 银行类型，
        /// 采用字符串类型的银行标识
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 订单现金支付金额
        /// </summary>
        public string cash_fee { get; set; }
        /// <summary>
        /// 代金券或立减优惠金额<=订单总金额，订单总金额-代金券或立减优惠金额=现金支付金额
        /// </summary>
        public string coupon_fee { get; set; }

        //public string coupon_batch_id_$n { get; set; }

        //public string coupon_id_$n { get; set; }

        /// <summary>
        /// 商户系统的订单号，与请求一致
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户系统的订单号，与请求一致.
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// 订单生成时间，格式为yyyyMMddHHmmss，
        /// 如2009年12月25日9点10分10秒表示为20091225091010。
        /// </summary>
        public string time_end { get; set; } 
        /// <summary>
        /// 对当前查询订单状态的描述和下一步操作的指引
        /// </summary>
        public string trade_state_desc { get; set; }

        #endregion
    }
}
