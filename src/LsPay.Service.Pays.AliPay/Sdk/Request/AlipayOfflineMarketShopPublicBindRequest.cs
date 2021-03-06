using System;
using System.Collections.Generic;
using Aop.Api.Response;

namespace Aop.Api.Request
{
    /// <summary>
    /// AOP API: alipay.offline.market.shop.public.bind
    /// </summary>
    public class AlipayOfflineMarketShopPublicBindRequest : IAopRequest<AlipayOfflineMarketShopPublicBindResponse>
    {
        /// <summary>
        /// 是否绑定所有门店，T表示绑定所有门店，F表示绑定指定shop_ids的门店
        /// </summary>
        public string IsAll { get; set; }

        /// <summary>
        /// 门店ID列表，一次最多绑定100个门店，is_all为T时表示绑定本商家下所有门店，即门店列表无需通过本参数shop_ids传入，由系统自动查询
        /// </summary>
        public string ShopIds { get; set; }

        #region IAopRequest Members
        private string apiVersion = "1.0";
		private string terminalType;
		private string terminalInfo;
        private string prodCode;
		private string notifyUrl;

		public void SetNotifyUrl(string notifyUrl){
            this.notifyUrl = notifyUrl;
        }

        public string GetNotifyUrl(){
            return this.notifyUrl;
        }

        public void SetTerminalType(String terminalType){
			this.terminalType=terminalType;
		}

    	public string GetTerminalType(){
    		return this.terminalType;
    	}

    	public void SetTerminalInfo(String terminalInfo){
    		this.terminalInfo=terminalInfo;
    	}

    	public string GetTerminalInfo(){
    		return this.terminalInfo;
    	}

        public void SetProdCode(String prodCode){
            this.prodCode=prodCode;
        }

        public string GetProdCode(){
            return this.prodCode;
        }

        public string GetApiName()
        {
            return "alipay.offline.market.shop.public.bind";
        }

        public void SetApiVersion(string apiVersion){
            this.apiVersion=apiVersion;
        }

        public string GetApiVersion(){
            return this.apiVersion;
        }

        public IDictionary<string, string> GetParameters()
        {
            AopDictionary parameters = new AopDictionary();
            parameters.Add("is_all", this.IsAll);
            parameters.Add("shop_ids", this.ShopIds);
            return parameters;
        }

        #endregion
    }
}
