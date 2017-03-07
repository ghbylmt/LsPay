using LsPay.Service.Wcf.Service;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;

namespace LsPay.Service.WindowsService
{
    partial class LsPayService : ServiceBase
    {

        /// <summary>
        /// LsPay预处理WCF服务
        /// </summary>
        private ServiceHost _ttsPayPreTreatPayHost = null;
        /// <summary>
        /// LsPayWCF服务
        /// </summary>
        private ServiceHost _ttsPayPayHost = null;
        /// <summary>
        /// LsPay 支付宝支付 WCF服务
        /// </summary>
        private ServiceHost _ttsPayAliPayHost = null;

        /// <summary>
        /// LsPay 微信支付 WCF服务
        /// </summary>
        private ServiceHost _ttsPayWxPayHost = null;
        public LsPayService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                #region 预处理服务
                if (_ttsPayPreTreatPayHost != null) _ttsPayPreTreatPayHost.Close();
                _ttsPayPreTreatPayHost = new ServiceHost(typeof(PayPreTreatmentService));
                _ttsPayPreTreatPayHost.Open();
                #endregion

                #region 支付服务
                if (_ttsPayPayHost != null) _ttsPayPayHost.Close();
                _ttsPayPayHost = new ServiceHost(typeof(PayService));
                _ttsPayPayHost.Open();
                #endregion

                #region 支付宝支付服务
                if (_ttsPayAliPayHost != null) _ttsPayAliPayHost.Close();
                _ttsPayAliPayHost = new ServiceHost(typeof(AliPayService));
                _ttsPayAliPayHost.Open();
                #endregion

                #region 微信支付服务
                if (_ttsPayWxPayHost != null) _ttsPayWxPayHost.Close();
                _ttsPayWxPayHost = new ServiceHost(typeof(WxPayService));
                _ttsPayWxPayHost.Open();
                #endregion
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("启动LsPay服务异常:" + ex.Message + ex.Source + ex.StackTrace, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            #region 预处理服务
            try
            {
                //释放WCF资源
                if (_ttsPayPreTreatPayHost != null && _ttsPayPreTreatPayHost.State != CommunicationState.Closed)
                {
                    _ttsPayPreTreatPayHost.Close();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("停止LsPay预处理服务出现异常:" + ex.Message + ex.Source, EventLogEntryType.Error);
            }
            #endregion

            #region 处理服务
            try
            {
                //释放WCF资源
                if (_ttsPayPayHost != null && _ttsPayPayHost.State != CommunicationState.Closed)
                {
                    _ttsPayPayHost.Close();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("停止LsPay处理服务出现异常:" + ex.Message + ex.Source, EventLogEntryType.Error);
            }
            #endregion
        }
    }
}
