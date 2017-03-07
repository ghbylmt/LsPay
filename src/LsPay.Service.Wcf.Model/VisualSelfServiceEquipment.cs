using LsPay.Service.Wcf.Model.Card;
using System.Runtime.Serialization;

namespace LsPay.Service.Wcf.Model
{
    /// <summary>
    /// 虚拟自助设备信息
    /// </summary>
    [DataContract]
    [KnownType(typeof(ICCard))]
    [KnownType(typeof(MagCard))]
    public class VisualSelfServiceEquipment
    {
        /// <summary>
        /// 终端号
        /// </summary>
        [DataMember]
        public string TerminalNo { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        [DataMember]
        public string PayMoney { get; set; }
        /// <summary>
        /// 加密后密码
        /// </summary>
        [DataMember]
        public string PinBlock { get; set; }
        /// <summary>
        /// 卡片信息
        /// </summary>
        [DataMember]
        public CreditCard creditCard { get; set; }
    }
}
