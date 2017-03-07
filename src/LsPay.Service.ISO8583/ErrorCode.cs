using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Service.ISO8583 
{
    public class ErrorCode : Dictionary<string, string> {
        public ErrorCode() {
            this.Add("00", "交易成功");
            this.Add("01", "请持卡人与发卡银行联系");
            this.Add("03", "无效商户");
            this.Add("04", "此卡被没收");
            this.Add("05", "持卡人认证失败");
            this.Add("10", "显示部分批准金额，提示操作员");
            this.Add("11", "成功，VIP客户");
            this.Add("12", "无效交易");
            this.Add("13", "无效金额");
            this.Add("14", "无效卡号");
            this.Add("15", "此卡无对应发卡方");
            this.Add("21", "该卡未初始化或睡眠卡");
            this.Add("22", "操作有误，或超出交易允许天数");
            this.Add("25", "没有原始交易，请联系发卡方");
            this.Add("30", "请重试");
            this.Add("34", "作弊卡，吞卡");
            this.Add("38", "密码错误次数超限，请与发卡联系");
            this.Add("40", "发卡方不支持的交易类型");
            this.Add("41", "挂失卡，请没收");
            this.Add("43", "被窃卡，请没收");
            this.Add("51", "可用余额不足");
            this.Add("54", "该卡已过期");
            this.Add("55", "密码错");
            this.Add("57", "不允许此卡交易");
            this.Add("58", "发卡方不允许该卡在本终端进行此交易");
            this.Add("59", "卡片校验错");
            this.Add("61", "交易金额超限");
            this.Add("62", "受限制的卡");
            this.Add("64", "交易金额与原交易不匹配");
            this.Add("65", "超出消费次数限制");
            this.Add("68", "交易超时，请重试");
            this.Add("75", "密码错误次数超限");
            this.Add("90", "系统日切，请稍后重试");
            this.Add("91", "发卡方状态不正常，请稍后重试");
            this.Add("92", "发卡方线路异常，请稍后重试");
            this.Add("94", "拒绝，重复交易，请稍后重试");
            this.Add("96", "拒绝，交换中心异常，请稍后重试");
            this.Add("97", "终端未登记");
            this.Add("98", "发卡方超时");
            this.Add("99", "PIN格式错，请重新签到");
            this.Add("A0", "MAC校验错，请重新签到");
            this.Add("A1", "转账货币不一致");
            this.Add("A2", "交易成功，请向发卡行确认");
            this.Add("A3", "账户不正确");
            this.Add("A4", "交易成功，请向发卡行确认");
            this.Add("A5", "交易成功，请向发卡行确认");
            this.Add("A6", "交易成功，请向发卡行确认");
            this.Add("A7", "拒绝，交换中心异常，请稍后重试");
        }
    }
}
