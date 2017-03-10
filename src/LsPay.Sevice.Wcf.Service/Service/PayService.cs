using System;
using System.ComponentModel;
using LsPay.Service.Wcf.Contract;
using LsPay.Service.Interface;
using LsPay.Service.Wcf.ServiceValidate.Attributes;
using LsPay.Service.Wcf.Model;
using LsPay.Service.Wcf.Service;
using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Service.Wcf.Service
{
    /// <summary>
    /// 支付服务
    /// </summary>
    [ServiceAuthorizationBehavior]
    public class PayService : IPayService
    {
        public PayResponseModel Pay(byte[] preMsg, string mac)
        {
            try
            {
                IPay PayObj = PaymentPlatFormFactory.GetPayFactory().GetPayObj(new ICCard());
                return PayObj.Pay(preMsg, mac);
            }
            catch(System.Exception ex)
            {
                //针对农行卡 62 开头的卡假如输入简单密码是 
                //调用接口出现超时的情况。
                //进行特殊的处理 返回代码 68 ：交易超时，请重试
                var w32ex = ex as Win32Exception;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as Win32Exception;
                }
                if(w32ex!=null && w32ex.ErrorCode.Equals(10060))
                {
                    return new PayResponseModel() { ResponseCode = "68" };
                }
                else 
                    throw ex;
            }
        }

        public PayResponseModel CancelPay(byte[] preMsg, string mac)
        {
            IPay PayObj = PaymentPlatFormFactory.GetPayFactory().GetPayObj(new ICCard());
            return PayObj.CancelPay(preMsg, mac);
        }

        public PayResponseModel Query(byte[] preMsg, string mac)
        {
            IPay PayObj = PaymentPlatFormFactory.GetPayFactory().GetPayObj(new ICCard());
            return PayObj.Query(preMsg, mac);
        }

        /// <summary>
        /// 虚拟自助设备信息
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        public SignResponseModel Sign(Model.VisualSelfServiceEquipment equipment)
        {
            return PaymentPlatFormFactory.GetPayUtility().Sign(equipment);
        }
    }
}
