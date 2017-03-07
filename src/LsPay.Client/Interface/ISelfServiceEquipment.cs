/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：自助设备操作功能接口
 * 文件：ISelfServiceEquipment.cs
 * 类名：ISelfServiceEquipment
 * 命名空间：LsPay.Client.Interface
 * 
 * 创建标识：尚春城 2015/07/03
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Service.Wcf.Model;

namespace LsPay.Client.Interface
{
    /// <summary>
    /// 自助设备操作功能接口
    /// </summary>
    public interface ISelfServiceEquipment
    {
        /// <summary>
        /// 进卡
        /// </summary>
        void CardIn();
        /// <summary>
        /// 读卡
        /// </summary>
        void ReadCard(string money);
        /// <summary>
        /// 支付
        /// </summary>
        /// <returns></returns>
        PayResponseModel Pay(string pin);
        /// <summary>
        /// 冲正
        /// </summary>
        /// <returns></returns>
        PayResponseModel Cancel();
        /// <summary>
        /// 出卡
        /// </summary>
        void CardOut();
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        bool Sign();
    }
}
