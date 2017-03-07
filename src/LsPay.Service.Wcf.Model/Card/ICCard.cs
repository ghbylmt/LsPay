/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：芯片卡卡片实体类
 * 文件：ICCard.cs
 * 类名：ICCard
 * 命名空间：LsPay.Service.Wcf.Model.Card
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using LsPay.Service.Util.Code;
using LsPay.Service.Util.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LsPay.Service.Wcf.Model.Card
{
    /// <summary>
    /// 芯片卡实体类
    /// </summary>
    [DataContract]
    public class ICCard : CreditCard
    {
        public ICCard()
        {
            this.GPOL = new List<string>();
            this.AFL = new List<string>();
        }
        /// <summary>
        /// IC卡类型
        /// </summary>
        [DataMember]
        public string CarType { get; set; }

        /// <summary>
        /// 应用标识符
        /// </summary>
        [DataMember]
        public string AID { get; set; }
        /// <summary>
        /// 处理选项数据对象列表
        /// </summary>
        [DataMember]
        public List<string> GPOL { get; set; }
        /// <summary>
        /// 应用交互特征
        /// </summary>
        [DataMember]
        public string AIP { get; set; }
        /// <summary>
        /// 应用文件定位器
        /// </summary>
        [DataMember]
        public List<string> AFL { get; set; }
        /// <summary>
        /// 应用文件数据列表
        /// </summary>
        //public List<TLVEntity> AppFileDataList { get; set; }

        /// <summary>
        /// 卡片风险管理数据对象列表1
        /// </summary>
        [DataMember]
        public List<string> CDOL1 { get; set; }

        /// <summary>
        /// 卡片风险管理数据对象列表2
        /// </summary>
        [DataMember]
        public List<string> CDOL2 { get; set; }


        /// <summary>
        /// 应用PAN序列号
        /// </summary>
        [DataMember]
        public string PANSerialNum { get; set; }

        /// <summary>
        /// 发卡行国家代码
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        [DataMember]
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// 密文信息 L:1
        /// </summary>
        [DataMember]
        public string CID { get; set; }
        /// <summary>
        /// 应用交易计数器 L:2
        /// </summary>
        [DataMember]
        public string ATC { get; set; }
        /// <summary>
        /// 应用密文
        /// </summary>
        [DataMember]
        public string AC { get; set; }
        /// <summary>
        /// 发卡行应用信息数据
        /// </summary>
        [DataMember]
        public string IssBankAppData { get; set; }
        /// <summary>
        /// 证件编号
        /// </summary>
        [DataMember]
        public string IDNumber { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public string DocumentType { get; set; }
        /// <summary>
        /// 专用文件名称
        /// </summary>
        [DataMember]
        public string DF { get; set; }
        /// <summary>
        /// 应用版本号
        /// </summary>
        [DataMember]
        public string AppVersionNo { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        [DataMember]
        public string ProductId { get; set; }

        /// <summary>
        /// 不可预知数
        /// </summary>
        [DataMember]
        public string RadomData { get; set; }
        public void Dispose()
        {
            //垃圾回收
            GC.Collect();
        }


        public string BuildPart55Data(string terminalNo,string TransActionType, string money, string sysTraceNum)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(terminalNo);
            terminalNo = BitConverter.ToString(bytes).Replace("-", "");
            //55域数据
            StringBuilder builder = new StringBuilder();
            builder.Append(TLVUtil.GetTLVByTv("9F26", this.AC));
            builder.Append(TLVUtil.GetTLVByTv("9F27", this.CID));
            builder.Append(TLVUtil.GetTLVByTv("9F10", this.IssBankAppData));
            builder.Append(TLVUtil.GetTLVByTv("9F37", this.RadomData));
            builder.Append(TLVUtil.GetTLVByTv("9F36", this.ATC));
            builder.Append(TLVUtil.GetTLVByTv("95", PublicStaticData.TVR));
            builder.Append(TLVUtil.GetTLVByTv("9A", DateTime.Now.ToString("yyMMdd")));
            builder.Append(TLVUtil.GetTLVByTv("9C", TransActionType));
            builder.Append(TLVUtil.GetTLVByTv("9F02", money));
            builder.Append(TLVUtil.GetTLVByTv("5F2A", MonetaryCode.RMB));
            builder.Append(TLVUtil.GetTLVByTv("82", this.AIP));
            builder.Append(TLVUtil.GetTLVByTv("9F1A", "0156"));
            builder.Append(TLVUtil.GetTLVByTv("9F03", "000000000000"));
            builder.Append(TLVUtil.GetTLVByTv("9F33", "E04820"));//终端性能
            builder.Append(TLVUtil.GetTLVByTv("9F34", "3F0300")); //持卡人验证结果
            builder.Append(TLVUtil.GetTLVByTv("9F35", "24"));//终端类型
            builder.Append(TLVUtil.GetTLVByTv("9F1E", terminalNo));//接口设备序列号
            builder.Append(TLVUtil.GetTLVByTv("84", AID));//专用文件名称
            builder.Append(TLVUtil.GetTLVByTv("9F09", this.AppVersionNo));//应用版本号
            builder.Append(TLVUtil.GetTLVByTv("9F41", sysTraceNum.PadLeft(8, '0')));//交易序列计数器
            if (!string.IsNullOrEmpty(this.ProductId))
                builder.Append(TLVUtil.GetTLVByTv("9F63", this.ProductId));//卡产品序列号
            return builder.ToString();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("卡号：{0} \r\n", this.CardNo);
            builder.AppendFormat("卡片类型：{0} \r\n", this.CarType);
            builder.AppendFormat("应用标识符：{0} \r\n", this.AID);
            builder.AppendFormat("PAN序列号：{0} \r\n", this.PANSerialNum);
            builder.AppendFormat("有效日期：{0} \r\n", this.EffectiveDate.ToString("yyyy-MM-dd"));
            builder.AppendFormat("密文信息：{0} \r\n", this.CID);
            builder.AppendFormat("应用交易计数器：{0} \r\n", this.ATC);
            builder.AppendFormat("应用密文：{0} \r\n", this.AC);
            builder.AppendFormat("发卡行应用数据：{0} \r\n", this.IssBankAppData);
            return builder.ToString();
        }
    }
}
