using System;
using System.Collections.Generic;
using System.Linq;
using LsPay.Service.Interface;
using LsPay.Service.ISO8583;
using LsPay.Service.ISO8583.Formatters;

namespace LsPay.Service.Pays.BankOfCangzhou
{
    /// <summary>
    /// 报文模板类
    /// </summary>
    public class MessageTemplate : IMessageTemplate
    {
        /// <summary>
        /// 获取消息模版数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, ISO8583.Field> GetTemplate()
        {
            Dictionary<int, Field> dic = new Dictionary<int, Field>();
            dic.Add(2, new Field(new FieldLenIndicator(2),new LeftBcdFormatter()));
            dic.Add(3, new Field(6, new RightBcdFormatter()));
            dic.Add(4, new Field(12, new RightBcdFormatter()));
            dic.Add(11, new Field(6, new RightBcdFormatter()));
            dic.Add(12, new Field(6, new RightBcdFormatter()));
            dic.Add(13, new Field(4, new RightBcdFormatter()));
            dic.Add(14, new Field(4, new RightBcdFormatter()));
            dic.Add(15, new Field(4, new RightBcdFormatter()));
            dic.Add(22, new Field(3, new LeftBcdFormatter()));
            dic.Add(23, new Field(3, new RightBcdFormatter()));
            dic.Add(25, new Field(2, new RightBcdFormatter()));
            dic.Add(26, new Field(2, new RightBcdFormatter()));
            dic.Add(32, new Field(new FieldLenIndicator(2), new LeftBcdFormatter()));
            dic.Add(35, new Field(new FieldLenIndicator(2), new LeftBcdFormatter()));
            dic.Add(36, new Field(new FieldLenIndicator(3), new LeftBcdFormatter()));
            dic.Add(37, new Field(12, new AsciiFormatter()));
            dic.Add(38, new Field(6, new AsciiFormatter()));
            dic.Add(39, new Field(2, new AsciiFormatter()));
            dic.Add(41, new Field(8, new AsciiFormatter()));
            dic.Add(42, new Field(15, new AsciiFormatter()));
            dic.Add(44, new Field(new FieldLenIndicator(2), new AsciiFormatter()));
            dic.Add(48, new Field(new FieldLenIndicator(3), new LeftBcdFormatter()));
            dic.Add(49, new Field(3, new AsciiFormatter()));
            dic.Add(52, new Field(8, new BinaryFormatter()));
            dic.Add(53, new Field(16, new RightBcdFormatter()));
            dic.Add(54, new Field(new FieldLenIndicator(3), new AsciiFormatter()));
            dic.Add(55, new Field(new FieldLenIndicator(3), new HexFormatter()));//IC卡数据域
            dic.Add(56, new Field(new FieldLenIndicator(3), new HexFormatter()));  //沧州银行自定义业务数据域
            dic.Add(60, new Field(new FieldLenIndicator(3), new LeftBcdFormatter()));
            dic.Add(61, new Field(new FieldLenIndicator(3), new LeftBcdFormatter()));
            dic.Add(62, new Field(new FieldLenIndicator(3), new BinaryFormatter()));
            dic.Add(63, new Field(new FieldLenIndicator(3), new AsciiFormatter()));
            dic.Add(64, new Field(8, new BinaryFormatter()));
            return dic;
        }
    }
}
