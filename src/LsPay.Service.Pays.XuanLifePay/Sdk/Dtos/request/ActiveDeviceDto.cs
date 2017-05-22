using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request
{
    public class ActiveDeviceDto : BaseDto
    {
        /// <summary>
        /// 激活码
        /// 由炫生活支付网关统一分配
        /// 6位
        /// </summary>
        public int activeCode { get; set; }
    }
}
