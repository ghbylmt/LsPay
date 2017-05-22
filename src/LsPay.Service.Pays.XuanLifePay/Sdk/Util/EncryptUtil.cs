using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LsPay.Service.Pays.XuanLifePay.Sdk.Util
{
    public class EncryptUtil
    {
        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        public static string GetMD5_32(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string GetSign(object request)
        {
            List<PropertyInfo> properties = request.GetType().GetProperties().OrderBy(p=>p.Name).ToList();
            StringBuilder sb = new StringBuilder();
            properties.ForEach(p=>{
                sb.AppendFormat("{0}={1}&", p.Name, p.PropertyType.IsEnum ? p.GetValue(request, null).GetHashCode(): p.GetValue(request,null));
            });
            sb.AppendFormat("key={0}",Config.Key);
            return GetMD5_32(sb.ToString());
        }
    }
}
