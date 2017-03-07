using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Interface
{
    /// <summary>
    /// 加密设备接口
    /// </summary>
    public interface IEncryEquipment
    {
        /// <summary>
        /// 打开加密设备
        /// </summary>
        /// <param name="cardNum">卡号</param>
        void Open(string cardNum);
        /// <summary>
        /// 关闭加密设备
        /// </summary>
        void Close();
        /// <summary>
        /// 获取加密后的密码
        /// </summary>
        /// <returns></returns>
        string GetPinBlock();
        /// <summary>
        /// 核对PIN码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CheckPIN(StringBuilder source);
        /// <summary>
        /// 计算PIN码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CalculatePIN(StringBuilder source);

        /// <summary>
        /// 计算MAC码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CalculateMAC(StringBuilder source);
        /// <summary>
        /// 核对MAC码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CheckMAK(StringBuilder source);

        /// <summary>
        /// 核对Track密钥
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CheckTRK(StringBuilder source);
        /// <summary>
        /// 计算Track码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string CalculateTRK(StringBuilder source);

        /// <summary>
        /// 下载密钥
        /// </summary>
        void LoadWorkKeySign(StringBuilder sbWorkKey, StringBuilder sbMak, StringBuilder sbTrk = null);
    }
}
