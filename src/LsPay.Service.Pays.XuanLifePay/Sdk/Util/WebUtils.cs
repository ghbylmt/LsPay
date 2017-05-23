using System;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Util
{
    /// <summary>
    /// 网络工具类。
    /// </summary>
    public sealed class WebUtils
    {
        public static string HttpGet(string url, string postData)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            //request.Headers.Set("Content-Type", "text/xml; charset=GBK");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";// "application/x-www-form-urlencoded";
            
            string result = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            responseStream.Close();
            return result;
        }
        /// <summary>
        /// POST传输
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        public static string HttpPost(string url, string postData)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json";// "application/x-www-form-urlencoded";
            Stream stream = request.GetRequestStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("gb2312"));
            writer.Write(postData);
            writer.Close();

            string result = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            responseStream.Close();
            return result;
        }

        private static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static TRes HttpPost<TReq,TRes>(string url, TReq request)
        {
            string reuslt = WebUtils.HttpPost(url, JsonConvert.SerializeObject(request));
            TRes response = JsonConvert.DeserializeObject<TRes>(reuslt);
            return response;
        }
        public static TRes HttpGet<TReq, TRes>(string url, TReq request)
        {
            string reuslt = WebUtils.HttpGet(url, JsonConvert.SerializeObject(request));
            TRes response = JsonConvert.DeserializeObject<TRes>(reuslt);
            return response;
        }
    }
}
