using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LsPay.Client.Service.AliPay;
using LsPay.Client.Service.Pay;
using LsPay.Client.Service.PayPreTreatment;
using LsPay.Client.Service.Wxpay;

namespace LsPay.Client.Service
{
    /// <summary>
    /// 服务调用公共操作类
    /// </summary>
    public class ServiceUtil
    {
        private static PayPreTreatmentClient _pretreatClient = null;
        public static PayPreTreatmentClient PayPreTreatmentClient
        {
            get
            {
                // 创建Binding  
                NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
                EndpointAddress edpAddr = new EndpointAddress(Settings.PayPreTreatmentService);
                if (_pretreatClient == null ||
                   _pretreatClient.State == CommunicationState.Closed ||
                   _pretreatClient.State == CommunicationState.Faulted)
                {//当信道为出错或关闭时 重新打开连接
                    _pretreatClient = null;
                    _pretreatClient = new PayPreTreatmentClient(tcpBinding, edpAddr);
                }
                return _pretreatClient;
            }
        }


        private static PayClient _payClient = null;
        public static PayClient PayClient
        {
            get
            {
                // 创建Binding  
                NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
                EndpointAddress edpAddr = new EndpointAddress(Settings.PayService);
                if (_payClient == null ||
                    _payClient.State == CommunicationState.Closed ||
                    _payClient.State == CommunicationState.Faulted)
                {//当信道为出错或关闭时 重新打开连接
                    _payClient = null;
                    _payClient = new PayClient(tcpBinding, edpAddr);
                }
                return _payClient;
            }
        }


        private static AliPayClient _aliPayClient = null;
        public static AliPayClient AliPayClient
        {
            get
            {
                // 创建Binding  
                NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
                EndpointAddress edpAddr = new EndpointAddress(Settings.AliPayService);
                if (_aliPayClient == null ||
                    _aliPayClient.State == CommunicationState.Closed ||
                    _aliPayClient.State == CommunicationState.Faulted)
                {//当信道为出错或关闭时 重新打开连接
                    _aliPayClient = null;
                    _aliPayClient = new AliPayClient(tcpBinding, edpAddr);
                }
                return _aliPayClient;
            }
        }


        private static WxPayClient _wxPayClient = null;
        public static WxPayClient WxPayClient
        {
            get
            {
                // 创建Binding  
                NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
                EndpointAddress edpAddr = new EndpointAddress(Settings.WxPayService);
                if (_wxPayClient == null ||
                    _wxPayClient.State == CommunicationState.Closed ||
                    _wxPayClient.State == CommunicationState.Faulted)
                {//当信道为出错或关闭时 重新打开连接
                    _wxPayClient = null;
                    _wxPayClient = new WxPayClient(tcpBinding, edpAddr);
                }
                return _wxPayClient;
            }
        }
    }
}
