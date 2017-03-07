using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsPay.Service.Util.Data
{
    /// <summary>
    /// 交易类型
    /// </summary>
    public class TransactionType
    {
        /// <summary>
        /// 查询余额
        /// </summary>
        public const string Query = "31";
        /// <summary>
        /// 消费
        /// </summary>
        public const string Pay = "00";
    }


    public class PublicStaticData
    {
        /// <summary>
        /// 交易类型
        /// </summary>
        public static string TransactionType { get; set; }
        /// <summary>
        /// 不可预知数
        /// </summary>
        public static string RadomData { get; set; }
        /// <summary>
        /// 终端验证结果
        /// </summary>
        public const string TVR = "8000040800";
        /// <summary>
        /// 授权金额
        /// </summary>
        public static string Money = "000000000000";
        /// <summary>
        /// 电子现金终端指示器
        /// </summary>
        public static Dictionary<string, string> PDOL = new Dictionary<string, string> 
        {
            {"9F7A01","01"},            //电子现金终端指示器
            {"9F0206",Money},  //授权金额
            {"5F2A02","0156"},          //交易货币代码
        };


        /// <summary>
        /// 卡片风险管理数据对象列表1
        /// </summary>
        public static Dictionary<string, string> CDOL1
        {
            get
            {
                return new Dictionary<string, string> 
                {
                    {"9F0206",Money},   //授权金额
                    {"9F0306","000000000000"},  //其它金额
                    {"9F1A02","0156"},          //终端国家代码
                    {"9505",TVR},      //TVR
                    {"5F2A02",MonetaryCode.RMB},          //交易货币代码
                    {"9A03",DateTime.Now.ToString("yyMMdd")}, //交易日期
                    {"9C01",TransactionType},              //交易类型
                    {"9F3704",RadomData},      //不可预知数
                    {"9F2103",DateTime.Now.ToString("HHmmss")},//交易时间
                    {"9F4E14",BitConverter.ToString(ASCIIEncoding.ASCII.GetBytes(Settings.MerchantCode)).Replace("-","").PadLeft(40,'0')},//商户名称
                };
            }
        }
    }


    

    /// <summary>
    /// 货币代码
    /// </summary>
    public class MonetaryCode
    {
        /// <summary>
        /// 人民币
        /// </summary>
        public const string RMB = "0156";
    }

}
