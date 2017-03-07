using log4net;
using Newtonsoft.Json;
using System.IO;

namespace LsPay.Service.Util.Log
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class Log
    {
        static Log()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        }

        #region INFO
        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="logmessage">信息内容</param>
        /// <param name="name">名称</param>
        public static void Info(LogModel logModel, string name = "INFOLogger")
        {
            ILog log = LogManager.GetLogger(name);
            logModel.LogLevel = LogLevel.Info;
            log.Info(string.Format("{0},", JsonConvert.SerializeObject(logModel)));
        }

        #endregion

        #region DEBUG
        /// <summary>
        /// 记录调试日志信息
        /// </summary>
        /// <param name="logmessage">信息内容</param>
        /// <param name="name">名称</param>
        public static void Debug(LogModel logModel, string name = "INFOLogger")
        {
            ILog log = LogManager.GetLogger(name);
            logModel.LogLevel = LogLevel.Debug;
            log.Debug(string.Format("{0},", JsonConvert.SerializeObject(logModel)));
        }

        #endregion



    }
}
