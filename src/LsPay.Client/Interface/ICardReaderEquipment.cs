using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Interface
{
    /// <summary>
    /// 读卡器设备接口
    /// </summary>
    public interface ICardReaderEquipment
    {
        /// <summary>
        /// 读芯片卡
        /// </summary>
        /// <param name="creditCard"></param>
        //void Read(ICCard creditCard);
        /// <summary>
        /// 读磁卡
        /// </summary>
        /// <param name="creditCard"></param>
        //void Read(MagCard creditCard);
        /// <summary>
        /// 发送APDU指令
        /// </summary>
        /// <param name="apdu"></param>
        /// <returns></returns>
        string SendAPDU(string apdu);
    }
}
