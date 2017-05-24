using LsPay.Service.Pays.XuanLifePay;
using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;
using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.response;
using System;
using System.Web;
using System.Web.Services;

namespace LsPay.XuanLifePay.WebService
{
    /// <summary>
    /// XuanLifePay 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class XuanLifePay : System.Web.Services.WebService
    {
        [WebMethod(Description = "一键激活")]
        public ActiveResponse ActiveDevice(string activeCode)
        {
            return PayUtil.ActiveDevice(new ActiveDeviceDto { activeCode = activeCode });
        }
        [WebMethod(Description = "注册_")]
        public CasherOpersResponse CasherOperate(string casherName, string casherPwd)
        {
            return PayUtil.CasherOper(new CasherOpersDto
            {
                casher_name = casherName,
                casher_pwd = casherPwd,
                operatore_type = OperType.Create.GetHashCode().ToString()
            });
        }
        [WebMethod(Description = "预下单")]
        public TradePreCreateResponse Precreate(int totalamount, int paychannel, string operid, string subject, string terminalid,string out_tradeNo)
        {
            return PayUtil.Precreate(new TradePreCreateDto
            {
                discountable_amount = "0",
                undiscountable_amount = totalamount.ToString(),
                total_amount = totalamount.ToString(),
                channel = paychannel.ToString(),
                terminal_id = terminalid,
                operatore_id = operid,
                out_trade_no = out_tradeNo,
                subject =HttpUtility.UrlEncode(subject).ToUpper()
            });
        }
        [WebMethod(Description = "查询")]
        public QueryResponse QueryOrder(string out_trade_no,string terminal_id)
        {
            return PayUtil.Query(new QueryDto
            {
                out_trade_no = out_trade_no,
                terminal_id = terminal_id
            });
        }
        [WebMethod(Description = "退款")]
        public RefundResponse RefundOrder(string out_trade_no,string terminal_id,int refundAmount)
        {
            return PayUtil.Refund(new RefundDto
            {
                out_trade_no = out_trade_no,
                terminal_id = terminal_id,
                refund_amount = refundAmount.ToString()
            });
        }

        [WebMethod(Description = "撤销")]
        public CancelResponse CancelOrder(string out_trade_no, string terminal_id)
        {
            return PayUtil.Cancel(new CancelDto
            {
                out_trade_no = out_trade_no,
                terminal_id = terminal_id,
            });
        }
        private string CreateTradeNo()
        {
            DateTime dtime = DateTime.Now;
            return dtime.ToString("yyyyMMddHHmmss") + dtime.Millisecond.ToString().PadLeft(4, '0');
        }
    }
}
