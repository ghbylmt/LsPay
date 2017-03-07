
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Data
{
    /// <summary>
    /// APDU通用指令
    /// </summary>
    public class APDUCommand 
    {
        /// <summary>
        /// 选择支付系统PSE-1PAY.SYS.DDF01
        /// </summary>
        public const string SELECT_PSE = "00A404000E315041592E5359532E4444463031";
        /// <summary>
        /// 获取数据
        /// </summary>
        public const string GETDATA = "00B2010C";
    }

    
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


    /// <summary>
    /// EMV标签
    /// </summary>
    public class EMVTag
    {
        /// <summary>
        /// 应用标识符(AID)
        /// </summary>
        public const string AID = "4F";
        /// <summary>
        /// 处理选项数据对象列表
        /// </summary>
        public const string PDOL = "9F38";
        /// <summary>
        /// 应用密文AC
        /// </summary>
        public const string AC = "9F26";
        /// <summary>
        /// 应用货币代码
        /// </summary>
        public const string MonetaryCode = "9F42";
        /// <summary>
        /// PAN主账号
        /// </summary>
        public const string PAN = "5A";
        /// <summary>
        /// 应用PAN序列号
        /// </summary>
        public const string PANSerialNum = "5F34";
        /// <summary>
        /// 发卡行国家代码
        /// </summary>
        public const string CountryCode = "5F28";
        /// <summary>
        /// 卡风险管理对象列表1
        /// </summary>
        public const string CDOL1 = "8C";
        /// <summary>
        /// 卡风险管理对象列表2
        /// </summary>
        public const string CDOL2 = "8D";
        /// <summary>
        /// 2磁道等价数据
        /// </summary>
        public const string Msg2 = "57";

        /// <summary>
        /// 证件号
        /// </summary>
        public const string IDNumber = "9F61";
        /// <summary>
        /// 证件类型
        /// </summary>
        public const string DocType = "9F62";
        /// <summary>
        /// 专用文件名称
        /// </summary>
        public const string DF = "84";

        /// <summary
        /// 卡产品标识
        /// </summary>
        public const string ProductId = "9F63";
        /// <summary>
        /// 应用版本号
        /// </summary>
        public const string AppVersionNo = "9F08";
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
                    {"5F2A02","0156"},          //交易货币代码
                    {"9A03",DateTime.Now.ToString("yyMMdd")}, //交易日期
                    {"9C01",TransactionType},              //交易类型
                    {"9F3704",RadomData},      //不可预知数
                    {"9F2103",DateTime.Now.ToString("HHmmss")},//交易时间
                    {"9F4E14",BitConverter.ToString(ASCIIEncoding.ASCII.GetBytes("111111111111")).Replace("-","").PadLeft(40,'0')},//商户名称
                };
            }
        }


        public static Dictionary<string, string> ResponseCode
        {
            get
            {
                Dictionary<string, string> responsecode = new Dictionary<string, string>();
                responsecode.Add("00", "交易成功");
                responsecode.Add("01", "请持卡人与发卡银行联系");
                responsecode.Add("03", "无效商户");
                responsecode.Add("04", "此卡被没收");
                responsecode.Add("05", "持卡人认证失败");
                responsecode.Add("10", "显示部分批准金额，提示操作员");
                responsecode.Add("11", "成功，VIP客户");
                responsecode.Add("12", "无效交易");
                responsecode.Add("13", "无效金额");
                responsecode.Add("14", "无效卡号");
                responsecode.Add("15", "此卡无对应发卡方");
                responsecode.Add("21", "该卡未初始化或睡眠卡");
                responsecode.Add("22", "操作有误，或超出交易允许天数");
                responsecode.Add("25", "没有原始交易，请联系发卡方");
                responsecode.Add("30", "请重试");
                responsecode.Add("34", "作弊卡，吞卡");
                responsecode.Add("38", "密码错误次数超限，请与发卡联系");
                responsecode.Add("40", "发卡方不支持的交易类型");
                responsecode.Add("41", "挂失卡，请没收");
                responsecode.Add("43", "被窃卡，请没收");
                responsecode.Add("51", "可用余额不足");
                responsecode.Add("54", "该卡已过期");
                responsecode.Add("55", "密码错");
                responsecode.Add("57", "不允许此卡交易");
                responsecode.Add("58", "发卡方不允许该卡在本终端进行此交易");
                responsecode.Add("59", "卡片校验错");
                responsecode.Add("61", "交易金额超限");
                responsecode.Add("62", "受限制的卡");
                responsecode.Add("64", "交易金额与原交易不匹配");
                responsecode.Add("65", "超出消费次数限制");
                responsecode.Add("68", "交易超时，请重试");
                responsecode.Add("75", "密码错误次数超限");
                responsecode.Add("90", "系统日切，请稍后重试");
                responsecode.Add("91", "发卡方状态不正常，请稍后重试");
                responsecode.Add("92", "发卡方线路异常，请稍后重试");
                responsecode.Add("94", "拒绝，重复交易，请稍后重试");
                responsecode.Add("96", "拒绝，交换中心异常，请稍后重试");
                responsecode.Add("97", "终端未登记");
                responsecode.Add("98", "发卡方超时");
                responsecode.Add("99", "PIN格式错，请重新签到");
                responsecode.Add("A0", "MAC校验错，请重新签到");
                responsecode.Add("A1", "转账货币不一致");
                responsecode.Add("A2", "交易成功，请向发卡行确认");
                responsecode.Add("A3", "账户不正确");
                responsecode.Add("A4", "交易成功，请向发卡行确认");
                responsecode.Add("A5", "交易成功，请向发卡行确认");
                responsecode.Add("A6", "交易成功，请向发卡行确认");
                responsecode.Add("A7", "拒绝，交换中心异常，请稍后重试");
                return responsecode;
            }
        }
    }


    
}
