/*-----------------------------------
 * Copyright (C) 2015 xxxxx
 * 版权所有。
 * 功能描述：支付接口
 * 文件：IPay.cs
 * 类名：IPay
 * 命名空间：LsPay.Service.Interface
 * 
 * 创建标识：尚春城 2015/07/02  
 * 
 * 修改标识：
 * 
 *----------------------------------*/

using LsPay.Service.Wcf.Model;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPay
    {

        /// <summary>
        /// 插卡消费
        /// </summary>
        /// <param name="payMoney">消费金额</param>
        /// <param name="pinBlock">交易密码</param>
        /// <returns></returns>
        PayResponseModel Pay(byte[] preMsg, string mac);
        /// <summary>
        /// 冲正
        /// </summary>
        /// <param name="pinBlock">交易密码</param>
        /// <returns></returns>
        PayResponseModel CancelPay(byte[] preMsg, string mac);
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="pinBlock">交易密码</param>
        /// <returns></returns>
        PayResponseModel Query(byte[] preMsg, string mac);
    }
}
