using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Wcf.Model.WxPay.response
{
    public class ErrorCode
    {
        /// <summary>
        /// 接口返回错误
        /// 请立即调用被扫订单结果查询API，查询当前订单状态，并根据订单的状态决定下一步的操作。
        /// </summary>
        public const string SYSTEMERROR = "SYSTEMERROR";
        /// <summary>
        /// 参数错误
        /// 请根据接口返回的详细信息检查您的程序
        /// </summary>
        public const string PARAM_ERROR = "PARAM_ERROR";
        /// <summary>
        /// 订单已支付
        /// 请确认该订单号是否重复支付，如果是新单，请使用新订单号提交
        /// </summary>
        public const string ORDERPAID = "ORDERPAID";
        /// <summary>
        /// 商户无权限
        /// 请开通商户号权限。请联系产品或商务申请
        /// </summary>
        public const string NOAUTH = "NOAUTH";
        /// <summary>
        /// 二维码已过期，请用户在微信上刷新后再试
        /// 请收银员提示用户，请用户在微信上刷新条码，然后请收银员重新扫码。 直接将错误展示给收银员
        /// </summary>
        public const string AUTHCODEEXPIRE = "AUTHCODEEXPIRE";
        /// <summary>
        /// 用户的零钱余额不足
        /// 请收银员提示用户更换当前支付的卡，然后请收银员重新扫码。建议：商户系统返回给收银台的提示为“用户余额不足.提示用户换卡支付”
        /// </summary>
        public const string NOTENOUGH = "NOTENOUGH";
        /// <summary>
        /// 不支持卡类型
        /// 请用户重新选择卡种 建议：商户系统返回给收银台的提示为“该卡不支持当前支付，提示用户换卡支付或绑新卡支付”
        /// </summary>
        public const string NOTSUPORTCARD = "NOTSUPORTCARD";
        /// <summary>
        /// 订单已关闭
        /// 商户订单号异常，请重新下单支付
        /// </summary>
        public const string ORDERCLOSED = "ORDERCLOSED";
        /// <summary>
        /// 订单已撤销
        /// 当前订单状态为“订单已撤销”，请提示用户重新支付
        /// </summary>
        public const string ORDERREVERSED = "ORDERREVERSED";
        /// <summary>
        /// 银行系统异常
        /// 请立即调用被扫订单结果查询API，查询当前订单的不同状态，决定下一步的操作。
        /// </summary>
        public const string BANKERROR = "BANKERROR";
        /// <summary>
        /// 用户支付中，需要输入密码
        /// 等待5秒，然后调用被扫订单结果查询API，查询当前订单的不同状态，决定下一步的操作。
        /// </summary>
        public const string USERPAYING = "USERPAYING";
        /// <summary>
        /// 授权码参数错误
        /// 每个二维码仅限使用一次，请刷新再试
        /// </summary>
        public const string AUTH_CODE_ERROR = "AUTH_CODE_ERROR";
        /// <summary>
        /// 授权码检验错误
        /// 请扫描微信支付被扫条码/二维码
        /// </summary>
        public const string AUTH_CODE_INVALID = "AUTH_CODE_INVALID";
        /// <summary>
        /// XML格式错误
        /// 请检查XML参数格式是否正确
        /// </summary>
        public const string XML_FORMAT_ERROR = "REQUIRE_POST_METHOD";
        /// <summary>
        /// 请使用post方法
        /// 请检查请求参数是否通过post方法提交
        /// </summary>
        public const string REQUIRE_POST_METHOD = "REQUIRE_POST_METHOD";
        /// <summary>
        /// 签名错误
        /// 请检查签名参数和方法是否都符合签名算法要求
        /// </summary>
        public const string SIGNERROR = "SIGNERROR";
        /// <summary>
        /// 缺少参数
        /// 请检查参数是否齐全
        /// </summary>
        public const string LACK_PARAMS = "LACK_PARAMS";
        /// <summary>
        /// 编码格式错误
        /// 请使用UTF-8编码格式
        /// </summary>
        public const string NOT_UTF8 = "NOT_UTF8";
        /// <summary>
        /// 支付帐号错误
        /// 请确认支付方是否相同
        /// </summary>
        public const string BUYER_MISMATCH = "BUYER_MISMATCH";
        /// <summary>
        /// APPID不存在
        /// 请检查APPID是否正确
        /// </summary>
        public const string APPID_NOT_EXIST = "APPID_NOT_EXIST";
        /// <summary>
        /// MCHID不存在
        /// 请检查MCHID是否正确
        /// </summary>
        public const string MCHID_NOT_EXIST = "MCHID_NOT_EXIST";
        /// <summary>
        /// 商户订单号重复
        /// 请核实商户订单号是否重复提交
        /// </summary>
        public const string OUT_TRADE_NO_USED = "OUT_TRADE_NO_USED";
        /// <summary>
        /// appid和mch_id不匹配
        /// 请确认appid和mch_id是否匹配
        /// </summary>
        public const string APPID_MCHID_NOT_MATCH = "APPID_MCHID_NOT_MATCH";
    }
}
