/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：自助设备操作功能类
 * 文件：SelfServiceEquipment.cs
 * 类名：SelfServiceEquipment
 * 命名空间：LsPay.Client.Equipment
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LsPay.Client.Interface;
using LsPay.Client.Equipment.M100.Data;
using LsPay.Client.Equipment.M100.Reader;
using m100 = LsPay.Client.Equipment.M100;
using LsPay.Client.Util;
using LsPay.Client.Function.Extension;
using System.ServiceModel;
using LsPay.Client.Data;
using LsPay.Client.Service;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Model.Alipay.response;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Client.Equipment.RC532;
using LsPay.Service.Wcf.Model.WxPay.response;
using LsPay.Service.Wcf.Model.WxPay;
using LsPay.Service.Wcf.Model.WxPay.micropay;
using AlipayModel = LsPay.Service.Wcf.Model.Alipay;
using WxpayModel = LsPay.Service.Wcf.Model.WxPay;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// 自助设备操作功能类
    /// </summary>
    public class SelfServiceEquipment : BaseSelfServiceEquipment, ISelfServiceEquipment
    {
        private CreditCardPayType _creditCardPayType;

        #region 构造函数
        public SelfServiceEquipment(CreditCardPayType creditCardPayType = CreditCardPayType.AllInPay)
            : base()
        {
            _creditCardPayType = creditCardPayType;
            this.M100 = new m100.M100(Settings.CardReader_COM);
            this.F10 = new F10();
            this.IEncry = new EncryEquipment_F10();
        }
        #endregion

        #region 设备属性
        /// <summary>
        /// 密码键盘
        /// </summary>
        public F10 F10 { get; set; }
        /// <summary>
        /// M100读卡器
        /// </summary>
        public m100.M100 M100 { get; set; }
        #endregion

        #region 01. 签到
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public bool Sign()
        {
            ServiceUtil.PayClient.Open();
            SignResponseModel responseModel = ServiceUtil.PayClient.Sign(new VisualSelfServiceEquipment { TerminalNo = Settings.TerminalNo });
            ServiceUtil.PayClient.Close();

            if (responseModel.ResponseCode != "00") return false;
            string key = responseModel.Content;
            StringBuilder sbWorkKey = new StringBuilder();
            bool result = false;
            switch (_creditCardPayType)
            {
                case CreditCardPayType.AllInPay:
                    #region 通联验证逻辑
                    sbWorkKey.Append(key.Substring(6, 32));
                    StringBuilder sbMak = new StringBuilder();
                    sbMak.Append(key.Substring(46, 16));
                    StringBuilder sbTrk = new StringBuilder();
                    sbTrk.Append(key.Substring(86, 32));

                    IEncry.LoadWorkKeySign(sbWorkKey, sbMak, sbTrk);

                    result = IEncry.CheckPIN(new StringBuilder("0000000000000000")).Substring(0, 8) == key.Substring(38, 8)
                           && IEncry.CheckMAK(new StringBuilder("0000000000000000")).Substring(0, 8) == key.Substring(78, 8)
                    && IEncry.CheckTRK(new StringBuilder("0000000000000000")).Substring(0, 8) == key.Substring(118, 8);
                    #endregion
                    break;
                case CreditCardPayType.ChinaUnionPay:
                    #region 银联加密
                    sbWorkKey.Append(key.Substring(4, 32));
                    sbMak = new StringBuilder();
                    sbMak.Append(key.Substring(44, 16));
                    IEncry.LoadWorkKeySign(sbWorkKey, sbMak);
                    result = IEncry.CheckPIN(new StringBuilder("0000000000000000")).Substring(0, 8) == key.Substring(36, 8)
                           && IEncry.CheckMAK(new StringBuilder("0000000000000000")).Substring(0, 8) == key.Substring(76, 8);
                    #endregion
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion

        #region 02. 进卡
        /// <summary>
        /// 进卡
        /// </summary>
        public void CardIn()
        {
            //调用读卡器的进卡操作
            M100.CreditCardIn();
        }
        #endregion

        #region 03. 读卡

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="money">交易金额</param>
        public void ReadCard(string money)
        {
            ///授权金额
            PublicStaticData.Money = money; //(money * 100).ToString("f0").PadLeft(12, '0');
            //调用读卡器进行读卡操作
            M100.ReadCreditCard();
        }
        #endregion

        #region 04. 支付
        /// <summary>
        /// 支付
        /// </summary>
        /// <returns></returns>
        public PayResponseModel Pay(string pin = "")
        {
            try
            {
                if (M100.CardContainer.Count < 1)
                    throw new ApplicationException("支付失败，卡机内无卡！");
                string pinBlock = "";
                if (string.IsNullOrEmpty(pin))
                    pinBlock = this.IEncry.GetPinBlock();
                else
                    pinBlock = CalculatePinBlock(M100.CacheCard.CardNo, pin);
                //string paymoney = (money * 100).ToString("f0").PadLeft(12, '0');
                switch (_creditCardPayType)
                {
                    case CreditCardPayType.AllInPay:
                        //2磁道数据加密
                        M100.CardContainer[0].Msg2 = CalculateTrackBlock(M100.CacheCard.Msg2);
                        //主账号数据加密
                        M100.CardContainer[0].CardNo = CalculateTrackBlock(M100.CacheCard.CardNo);
                        break;
                    case CreditCardPayType.ChinaUnionPay:
                        M100.CardContainer[0].Msg2 = M100.CacheCard.Msg2;
                        M100.CardContainer[0].CardNo = M100.CacheCard.CardNo;
                        break;
                    default:
                        break;
                }
                //虚拟设备对象
                VisualSelfServiceEquipment equipment = new VisualSelfServiceEquipment()
                {
                    creditCard = M100.CardContainer[0],
                    TerminalNo = Settings.TerminalNo,
                    PinBlock = pinBlock,
                    PayMoney = PublicStaticData.Money
                };

                ServiceUtil.PayPreTreatmentClient.Open();
                //保存支付信息用于撤销
                this.LatestPreMsg = ServiceUtil.PayPreTreatmentClient.Pay(equipment);
                ServiceUtil.PayPreTreatmentClient.Close();

                ServiceUtil.PayClient.Open();
                PayResponseModel result = ServiceUtil.PayClient.Pay(LatestPreMsg, CalculateMac(LatestPreMsg));
                ServiceUtil.PayClient.Close();//及时关闭信道
                return ReBuildResultModel(result);
            }
            catch (FaultException faultEx)
            {//WCF异常
                //释放信道
                ServiceUtil.PayPreTreatmentClient.Abort();
                ServiceUtil.PayClient.Abort();
                throw new ApplicationException(string.Format("WCF服务调用出错：{0}", faultEx.Message));
            }
        }
        #endregion

        #region 05. 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public PayResponseModel Query(string pin)
        {
            try
            {
                if (M100.CardContainer.Count < 1)
                    throw new ApplicationException("查询失败，卡机内无卡！");
                string pinBlock = "";
                if (string.IsNullOrEmpty(pin))
                    pinBlock = this.IEncry.GetPinBlock();
                else
                    pinBlock = CalculatePinBlock(M100.CacheCard.CardNo, pin);
                //2磁道数据加密
                M100.CardContainer[0].Msg2 = CalculateTrackBlock(M100.CacheCard.Msg2);
                //主账号数据加密
                M100.CardContainer[0].CardNo = CalculateTrackBlock(M100.CacheCard.CardNo);
                //虚拟设备对象
                VisualSelfServiceEquipment equipment = new VisualSelfServiceEquipment()
                {
                    creditCard = M100.CardContainer[0],
                    TerminalNo = Settings.TerminalNo,
                    PinBlock = pinBlock,
                    PayMoney = "000000000000"
                };
                //针对查询交易存储临时交易预处理信息，无需冲正
                var tempPreMsg = ServiceUtil.PayPreTreatmentClient.Query(equipment);
                PayResponseModel result = ServiceUtil.PayClient.Query(tempPreMsg, CalculateMac(tempPreMsg));
                return ReBuildResultModel(result);
            }
            catch (FaultException faultEx)
            {//WCF异常
                //释放信道
                ServiceUtil.PayPreTreatmentClient.Abort();
                ServiceUtil.PayClient.Abort();
                throw new ApplicationException(string.Format("WCF服务调用出错：{0}", faultEx.Message));
            }
        }
        #endregion

        #region 06. 撤销
        /// <summary>
        /// 撤销
        /// </summary>
        /// <returns></returns>
        public PayResponseModel Cancel()
        {
            try
            {
                if (this.LatestPreMsg == null)
                    throw new ApplicationException("无撤销可用数据!");
                if (M100.CardContainer.Count < 1)
                    throw new ApplicationException("撤销失败，卡机内无卡！");
                //预处理
                ServiceUtil.PayPreTreatmentClient.Open();
                var tempPreMsg = ServiceUtil.PayPreTreatmentClient.CancelPay(M100.CardContainer[0], LatestPreMsg, this.LatestExtendData);
                ServiceUtil.PayPreTreatmentClient.Close();//及时关闭信道

                ServiceUtil.PayClient.Open();
                PayResponseModel result = ServiceUtil.PayClient.CancelPay(tempPreMsg, CalculateMac(tempPreMsg));
                ServiceUtil.PayClient.Close();//及时关闭信道

                return ReBuildResultModel(result);
            }
            catch (FaultException faultEx)
            {//WCF异常
                //释放信道
                ServiceUtil.PayPreTreatmentClient.Abort();
                ServiceUtil.PayClient.Abort();
                throw new ApplicationException(string.Format("WCF服务调用出错：{0}", faultEx.Message));
            }
        }
        #endregion

        #region 07. 退卡
        /// <summary>
        /// 退卡
        /// </summary>
        public void CardOut()
        {
            PublicStaticData.Money = "000000000000";//交易结束授权金额归零
            //M100.SetCardStopPositionBehaviorCatchCard();
            M100.ResetAndCardOut();
            //M100.SetFeedingMode_NoCardIn();
            base.Dispose();
        }
        #endregion

        #region 支付宝支付相关
        /// <summary>
        /// 支付宝预下单请求
        /// </summary>
        /// <param name="precreateModel">求情数据</param>
        /// <returns></returns>
        public PrecreateResponseModel PreCreate(PrecreateModel precreateModel)
        {
            PrecreateResponseModel response = ServiceUtil.AliPayClient.PreCreate(precreateModel);
            ServiceUtil.AliPayClient.Close();
            return response;
        }
        /// <summary>
        /// 条码支付
        /// </summary>
        /// <param name="tradepayModel">条码支付请求参数</param>
        /// <param name="payOverCallBack">支付结束回调函数</param>
        /// <param name="payStateChangeCallBack">支付状态改变回调函数</param>
        public void TradePay(TradepayModel tradepayModel, Action<bool, TradepayResponseModel> payOverCallBack, Action<bool, QueryResponseModel> payStateChangeCallBack)
        {
            TradepayResponseModel response = ServiceUtil.AliPayClient.TradePay(tradepayModel);
            ServiceUtil.AliPayClient.Close();
            #region 1.交易成功
            if (response.code == TradepayResultCode.TRADE_SUCCESS)
            {
                payOverCallBack(true, response);
                return;
            }
            #endregion
            #region 2.交易失败
            if (response.code == TradepayResultCode.TRADE_FAILURE)
            {
                payOverCallBack(false, response);
                return;
            }
            #endregion
            #region 3.交易结果未知
            if (response.code == TradepayResultCode.EXCEPTION || response.code == TradepayResultCode.PROCESSING)
            {
                string out_trade_no = response.out_trade_no;
                QueryResponseModel queryResponse = new QueryResponseModel();
                Task queryOrderTask = new Task(new Action(() =>
                {
                    queryResponse = Query(new QueryModel() { out_trade_no = out_trade_no }, payStateChangeCallBack);
                }));
                queryOrderTask.Start();
            }
            #endregion
        }
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="queryModel">订单查询请求参数</param>
        /// <param name="payStateChangeCallBack">订单状态改变回调函数</param>
        /// <returns></returns>
        public QueryResponseModel Query(QueryModel queryModel, Action<bool, QueryResponseModel> payStateChangeCallBack)
        {
            QueryResponseModel response = ServiceUtil.AliPayClient.Query(queryModel);
            ServiceUtil.AliPayClient.Close();
            if (response.code == TradepayResultCode.TRADE_FAILURE)
            {
                payStateChangeCallBack(false, response);
                Thread.Sleep(3000); //间隔3s请求一次      
                response = Query(queryModel, payStateChangeCallBack);
            }
            else
            {
                switch (response.trade_status)
                {
                    case TradeStatus.WAIT_BUYER_PAY:
                        payStateChangeCallBack(false, response);
                        Thread.Sleep(3000); //间隔3s请求一次      
                        response = Query(queryModel, payStateChangeCallBack);
                        break;
                    case TradeStatus.TRADE_SUCCESS:
                        payStateChangeCallBack(true, response);
                        break;
                    case TradeStatus.TRADE_FINISHED:
                    case TradeStatus.TRADE_CLOSED:
                    default:
                        payStateChangeCallBack(false, response);
                        break;
                }
            }
            return response;
        }
        /// <summary>
        /// 撤销订单
        /// </summary>
        /// <param name="cancelModel">请求参数</param>
        /// <returns></returns>
        public void AlipayCancel(CancelModel cancelModel, Action<bool, CancelResponseModel> aliPayCancelCallBack)
        {
            CancelResponseModel response = ServiceUtil.AliPayClient.Cancel(cancelModel);
            ServiceUtil.AliPayClient.Close();
            if (response.code == TradepayResultCode.TRADE_SUCCESS)
                aliPayCancelCallBack(true, response);
            else
            {
                if (response.retry_flag == "Y")
                {
                    Thread.Sleep(3000); //间隔3s请求一次 
                    AlipayCancel(cancelModel, aliPayCancelCallBack);
                }

                else
                    aliPayCancelCallBack(false, response);
            }
        }
        /// <summary>
        /// 支付宝支付申请退款接口
        /// </summary>
        /// <param name="requestModel"></param>
        public void AlipayRefund(AlipayModel.RefundModel requestModel, Action<bool, AlipayModel.response.RefundResponseModel> refundCallback)
        {
            AlipayModel.response.RefundResponseModel response = ServiceUtil.AliPayClient.Refund(requestModel);
            ServiceUtil.AliPayClient.Close();
            bool success = response.code.Equals(TradepayResultCode.TRADE_SUCCESS);
            refundCallback(success, response);
        }
        #endregion

        #region 微信支付相关
        /// <summary>
        /// 微信统一下单
        /// </summary>
        /// <param name="requestModel">请求数据</param>
        /// <returns></returns>
        public UnifiedOrderResponseModel UnifiedOrder(UnifiedOrderModel requestModel)
        {
            UnifiedOrderResponseModel response = ServiceUtil.WxPayClient.UnifiedOrder(requestModel);
            ServiceUtil.AliPayClient.Close();
            return response;
        }
        /// <summary>
        /// 微信刷卡支付
        /// </summary>
        /// <param name="micropayModel">刷卡支付请求</param>
        /// <param name="payOverCallBack">支付结束回调函数</param>
        /// <param name="payStateChangeCallBack">支付状态改变回调函数</param>
        public void Micropay(MicropayModel micropayModel, Action<bool, MicropayResponseModel> payOverCallBack, Action<bool, OrderQueryResponseModel> payStateChangeCallBack)
        {
            MicropayResponseModel response = ServiceUtil.WxPayClient.Micropay(micropayModel);
            ServiceUtil.WxPayClient.Close();
            if (response.result_code == ResultCode.SUCCESS)
            {//接口调用成功，业务成功
                payOverCallBack(true, response);
                return;
            }
            #region 1.业务结果确定处理失败
            if (response.err_code != ErrorCode.SYSTEMERROR && response.err_code != ErrorCode.USERPAYING)
            {
                payOverCallBack(false, response);
                return;
            }
            #endregion

            #region 2.不能确定是否失败，需查单
            //用商户订单号去查单
            string out_trade_no = response.out_trade_no;
            OrderQueryResponseModel orderQueryResponse = new OrderQueryResponseModel();
            orderQueryResponse = OrderQuery(new WxpayModel.OrderQueryModel() { out_trade_no = out_trade_no }, payStateChangeCallBack);
            #endregion
        }
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="queryModel">订单查询请求参数</param>
        /// <param name="payStateChangeCallBack">支付状态改变回调函数</param>
        /// <returns></returns>
        public OrderQueryResponseModel OrderQuery(WxpayModel.OrderQueryModel queryModel, Action<bool, OrderQueryResponseModel> payStateChangeCallBack)
        {
            OrderQueryResponseModel response = ServiceUtil.WxPayClient.OrderQuery(queryModel);
            ServiceUtil.AliPayClient.Close();
            if (response.return_code == ReturnCode.SUCCESS)
            {
                switch (response.trade_state)
                {
                    case TradeState.NOTPAY:
                    case TradeState.USERPAYING:
                        payStateChangeCallBack(false, response);
                        Thread.Sleep(3000); //间隔3s请求一次      
                        response = OrderQuery(queryModel, payStateChangeCallBack);
                        break;
                    case TradeState.SUCCESS:
                        payStateChangeCallBack(true, response);
                        break;
                    case TradeState.CLOSED:
                    case TradeState.REFUND:
                    case TradeState.REVOKED:
                    default:
                        payStateChangeCallBack(false, response);
                        break;
                }
            }
            else
            {
                switch (response.err_code)
                {
                    case ErrorCode.ORDERPAID:
                        payStateChangeCallBack(true, response);
                        break;
                    case ErrorCode.SYSTEMERROR:
                        payStateChangeCallBack(false, response);
                        Thread.Sleep(3000); //间隔3s请求一次      
                        response = OrderQuery(queryModel, payStateChangeCallBack);
                        break;
                    default:
                        payStateChangeCallBack(false, response);
                        break;

                }
            }
            return response;
        }
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="closeOrderChangeCallBack"></param>
        public void CloseOrder(WxpayModel.CloseOrderModel requestModel, Action<bool, CloseOrderResponseModel> closeOrderChangeCallBack)
        {
            CloseOrderResponseModel response = ServiceUtil.WxPayClient.CloseOrder(requestModel);
            if (response.result_code == ResultCode.SUCCESS)
            {
                closeOrderChangeCallBack(true, response);
            }
            else
            {
                switch (response.err_code)
                {
                    case ErrorCode.ORDERCLOSED:
                        closeOrderChangeCallBack(true, response);
                        break;
                    case ErrorCode.SYSTEMERROR:
                        Thread.Sleep(1000); //间隔1s请求一次 
                        CloseOrder(requestModel, closeOrderChangeCallBack);
                        break;
                    case ErrorCode.ORDERPAID:
                    case ErrorCode.REQUIRE_POST_METHOD:
                    case ErrorCode.SIGNERROR:
                    default:
                        closeOrderChangeCallBack(false, response);
                        break;
                }
            }
        }

        /// <summary>
        /// 微信支付申请退款接口
        /// </summary>
        /// <param name="requestModel">请求参数</param>
        /// <param name="refundCallBack">申请退款回调函数</param>
        /// <returns></returns>
        public void WxpayRefund(WxpayModel.RefundModel requestModel, Action<bool, WxpayModel.response.RefundResponseModel> refundCallBack)
        {
            WxpayModel.response.RefundResponseModel response = ServiceUtil.WxPayClient.Refund(requestModel);
            ServiceUtil.WxPayClient.Close();
            if (response.result_code == ResultCode.SUCCESS)
            {
                refundCallBack(true, response);
            }
            else
            {
                refundCallBack(false, response);
            }
        }
        #endregion

        /// <summary>
        /// 打开F10密码键盘
        /// </summary>
        public void OpenF10()
        {
            //打开F10密码键盘
            IEncry.Open(M100.CacheCard.CardNo);
            //F10.Open(M100.CacheCard.CardNo);
        }
        /// <summary>
        /// 关闭密码键盘
        /// </summary>
        public void CloseF10()
        {
            IEncry.Close();
        }

        #region 加密计算方法
        /// <summary>
        /// mac值计算
        /// </summary>
        /// <param name="preMsg"></param>
        /// <returns></returns>
        private string CalculateMac(byte[] preMsg)
        {
            StringBuilder mac = new StringBuilder();
            mac.Append(BitConverter.ToString(preMsg.GetSubArray(13, preMsg.Length - 21)).Replace("-", ""));
            return IEncry.CalculateMAC(mac);
        }

        /// <summary>
        /// 根据输入的明文密码和卡号计算加密后的PIN值
        /// </summary>
        /// <param name="CardNo">卡号</param>
        /// <param name="pin">明文密码</param>
        /// <returns></returns>
        private string CalculatePinBlock(string CardNo, string pin)
        {
            string pinStr = BitConverter.ToString(Utilities.PinBlock(pin, CardNo.Substring(CardNo.Length - 13, 12))).Replace("-", "");
            return IEncry.CalculatePIN(new StringBuilder(pinStr));
        }
        /// <summary>
        /// 根据读取的磁道数据的明文计算加密后的磁道信息
        /// </summary>
        /// <param name="trackInfo">磁道信息</param>
        /// <returns></returns>
        private string CalculateTrackBlock(string trackInfo)
        {
            string trackStr = BitConverter.ToString(Utilities.TrackBlock(trackInfo)).Replace("-", "");
            return IEncry.CalculateTRK(new StringBuilder(trackStr));
        }
        #endregion

        /// <summary>
        /// 重构返回的对象
        /// </summary>
        private PayResponseModel ReBuildResultModel(PayResponseModel result)
        {
            if (_creditCardPayType == CreditCardPayType.AllInPay)
                result.Pan = this.M100.CacheCard.CardNo;//通联支付返回正常卡号非加密卡号
            if (result.ResponseCode == "00")
            {
                string[] extendData = result.ExtendInfo.Split(',');
                this.LatestExtendData = extendData.Length > 1 ? string.Format("{0},{1}", extendData[0], extendData[1]) : extendData[0];//交易流水号
                result.ExtendInfo = extendData[0];
            }
            return result;
        }
    }
}
