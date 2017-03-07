/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：自助售票设备基类
 * 文件：BaseSelfServiceEquipment.cs
 * 类名：BaseSelfServiceEquipment
 * 命名空间：LsPay.Client.Equipment
 * 
 * 创建标识：尚春城 2015/07/03
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using LsPay.Client.Interface;

namespace LsPay.Client.Equipment
{
    /// <summary>
    /// 自助售票设备基类
    /// </summary>
    public class BaseSelfServiceEquipment : IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseSelfServiceEquipment()
        {

        }
        /// <summary>
        /// 加密设备
        /// </summary>
        protected IEncryEquipment IEncry;
        /// <summary>
        /// 上一次交易预处理数据
        /// </summary>
        protected byte[] LatestPreMsg;
        /// <summary>
        /// 上一次交易附加数据
        /// </summary>
        protected string LatestExtendData;

        public void Dispose()
        {
            //CardContainer.Clear();
            LatestPreMsg = null;
        }

        
    }
}
