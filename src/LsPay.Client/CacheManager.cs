using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client
{
    public static class CacheManager
    {
        /// <summary>
        /// 缓存交易信息的数量
        /// </summary>
        public const int count = 5;
        /// <summary>
        /// 本地交易信息缓存
        /// </summary>
        private static Dictionary<string, CacheItem> _transactionCache = new Dictionary<string, CacheItem>(5);

        /// <summary>
        /// 交易缓存
        /// </summary>
        /// <param name="SerialNo">系统交易流水号</param>
        /// <returns></returns>
        public static byte[] GetTransactionCacheBySerialNo(string SerialNo)
        {
            return _transactionCache[SerialNo].Data;
        }

        public static void SetTransactionCache(string SerialNo)
        {
            if (_transactionCache.Count == 5)
                return;

        }


    }

    /// <summary>
    /// 缓存元素
    /// </summary>
    public class CacheItem
    {
        public int Index { get; set; }

        public byte[] Data { get; set; }
    }
}
