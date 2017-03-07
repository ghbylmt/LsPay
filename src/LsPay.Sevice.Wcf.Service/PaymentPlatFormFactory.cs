using LsPay.Service.Interface;
using LsPay.Service.Pays.AllInPay.Pay;
using LsPay.Service.Pays.BankOfCangzhou.Pay;
using LsPay.Service.Pays.ChinaUnionPay.Pay;
using LsPay.Service.Util;
using LsPay.Service.Util.Data;
using ChinaUnionPay = LsPay.Service.Pays.ChinaUnionPay;
using AllInPay = LsPay.Service.Pays.AllInPay;
using BankOfCangzhou = LsPay.Service.Pays.BankOfCangzhou;
using System;
using LsPay.Service.ISO8583;

namespace LsPay.Service.Wcf.Service
{
    /// <summary>
    /// 支付平台工厂
    /// </summary>
    public class PaymentPlatFormFactory
    {
        public static IPayFactory GetPayFactory()
        {
            InitTempLate();//初始化模板数据
            IPayFactory payFactory = null;
            switch (Settings.PaymentPlatForm)
            {
                case PaymentPlatFormMapping.CHINA_UNION_PAY:
                    payFactory = new ChinaUnionPayFactory();
                    break;
                case PaymentPlatFormMapping.ALL_IN_PAY:
                    payFactory = new AllInPayFactory();
                    break;
                case PaymentPlatFormMapping.BANK_OF_CANGZHOU:
                    payFactory = new BankOfCangzhouFactory();
                    break;
                default:
                    throw new ArgumentException(string.Format("未配置支付平台或配置的支付平台无效 Platform:{0}", Settings.PaymentPlatForm));
            }
            return payFactory;
        }


        public static IPayPreTreatmentFactory GetPayPreTreatmentFactory()
        {
            InitTempLate();//初始化模板数据
            IPayPreTreatmentFactory payPreTreatmentFactory = null;
            switch (Settings.PaymentPlatForm)
            {
                case PaymentPlatFormMapping.CHINA_UNION_PAY:
                    payPreTreatmentFactory = new ChinaUnionPay.Pay.PreTreatment.PayPreTreatmentFactory();
                    break;
                case PaymentPlatFormMapping.ALL_IN_PAY:
                    payPreTreatmentFactory = new AllInPay.Pay.PreTreatment.PayPreTreatmentFactory();
                    break;
                case PaymentPlatFormMapping.BANK_OF_CANGZHOU:
                    payPreTreatmentFactory = new BankOfCangzhou.Pay.PreTreatment.PayPreTreatmentFactory();
                    break;
                default:
                    throw new ArgumentException(string.Format("未配置支付平台或配置的支付平台无效 Platform:{0}", Settings.PaymentPlatForm));
            }
            return payPreTreatmentFactory;
        }

        public static IPayUtility GetPayUtility()
        {
            InitTempLate();//初始化模板数据
            IPayUtility payUtility = null;
            switch (Settings.PaymentPlatForm)
            {
                case PaymentPlatFormMapping.CHINA_UNION_PAY:
                    payUtility = new ChinaUnionPay.Pay.PayUntity();
                    break;
                case PaymentPlatFormMapping.ALL_IN_PAY:
                    payUtility = new AllInPay.Pay.PayUntity();
                    break;
                case PaymentPlatFormMapping.BANK_OF_CANGZHOU:
                    payUtility = new BankOfCangzhou.Pay.PayUntity();
                    break;
                default:
                    throw new ArgumentException(string.Format("未配置支付平台或配置的支付平台无效 Platform:{0}", Settings.PaymentPlatForm));
            }
            return payUtility;
        }

        /// <summary>
        /// 初始化报文格式模板
        /// </summary>
        private static void InitTempLate()
        {
            if (Template.Instance != null)
                return;
            IMessageTemplate msgTemplate = null;
            switch (Settings.PaymentPlatForm)
            {
                case PaymentPlatFormMapping.CHINA_UNION_PAY:
                    msgTemplate = new ChinaUnionPay.MessageTemplate();
                    break;
                case PaymentPlatFormMapping.ALL_IN_PAY:
                    msgTemplate = new AllInPay.MessageTemplate();
                    break;
                case PaymentPlatFormMapping.BANK_OF_CANGZHOU:
                    msgTemplate = new BankOfCangzhou.MessageTemplate();
                    break;
                default:
                    throw new ArgumentException(string.Format("未配置支付平台或配置的支付平台无效 Platform:{0}", Settings.PaymentPlatForm));
            }
            Template.Instance = msgTemplate.GetTemplate();
        }
    }
}
