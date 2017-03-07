/*-----------------------------------
 * Copyright (C) 2015 xxxxxx
 * 版权所有。
 * 功能描述：M100读卡器操作功能类
 * 文件：M100.cs
 * 类名：M100
 * 命名空间：LsPay.Client.Equipment
 * 
 * 创建标识：尚春城 2015/07/02
 * 
 * 修改标识：
 * 
 *----------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LsPay.Client.Equipment;
using LsPay.Client.Function.Code;
using LsPay.Client.Function.Extension;
using LsPay.Client.Interface;
using LsPay.Client.Equipment.M100.Reader;
using LsPay.Client.WinService.Service.FunctionReader;
using LsPay.Client.Model.Entity;
using LsPay.Client.Equipment.M100.Data;
using System.Threading;
using LsPay.Client.Exception;
using LsPay.Service.Wcf.Model.Card;

namespace LsPay.Client.Equipment.M100
{
    /// <summary>
    /// M100读卡器操作功能类
    /// </summary>
    public class M100 : BaseCardReaderEquipment, ICardReaderEquipment
    {
        /// <summary>
        /// COM口访问服务
        /// </summary>
        private COM_Receive com;

        /// <summary>
        /// 卡槽
        /// </summary>
        public List<CreditCard> CardContainer { get; private set; }
        /// <summary>
        /// 读卡器缓存数据
        /// </summary>
        public CreditCardCacheData CacheCard { get; set; }
        /// <summary>
        /// 是否取消进卡
        /// </summary>
        [ThreadStatic]
        private bool IsCancelCardIn = false;

        public M100(string comName)
        {
            com = new COM_Receive(comName);
            CardContainer = new List<CreditCard>();
        }

        #region 串口操作
        /// <summary>
        /// 读卡器进卡方式控制
        /// 磁卡方式进卡(磁信号+开关同时有效)，只允许磁卡从从前端开闸门进卡
        /// </summary>
        /// <returns>成功返回0，失败返回错误代码。</returns>
        public string SetFeedingMode()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x03, 0x2f, 0x32, 0x31, 0x03, 0x2e };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result2 = com.SendCmd(cmd);//返回 0x4E:设置成功  0x59:设置失败
            com.ClosePort();
            return result2[3] == 0x4E ? result2[result2.Length - 3].ToString() : "0";
        }
        /// <summary>
        /// 读卡器进卡方式控制
        /// 禁止前端进卡
        /// </summary>
        /// <returns>成功返回0，失败返回错误代码。</returns>
        public string SetFeedingMode_NoCardIn()
        {
            byte bcc = 0x02 ^ 0x00 ^ 0x03 ^ 0x2f ^ 0x31 ^ 0x31 ^ 0x03;
            byte[] cmd = new byte[] { 0x02, 0x00, 0x03, 0x2f, 0x31, 0x31, 0x03, bcc };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result2 = com.SendCmd(cmd);//返回 0x4E:设置成功  0x59:设置失败
            com.ClosePort();
            return result2[3] == 0x4E ? result2[result2.Length - 3].ToString() : "0";
        }
        /// <summary>
        /// 停卡位置设置(当卡机进卡读完磁卡后所停的位置进行设置)
        /// 进卡后停卡在卡机内位置，同时将IC卡座触点与卡接触，直接可进行IC卡操作和M1射频卡操作。
        /// </summary>
        /// <returns></returns>
        public bool SetCarInPosition()
        {
            byte bbc = 0x02 ^ 0x00 ^ 0x02 ^ 0x2e ^ 0x33 ^ 0x03;
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x2e, 0x33, 0x03, bbc };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            return result[5] == 0x59;
        }
        /// <summary>
        /// 判断卡机是否有卡。
        /// </summary>
        /// <returns></returns>
        public bool GetStatus()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x31, 0x30, 0x03, 0x02 };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            return result[5] == 0x4b;// 0x4E;
        }
        /// <summary>
        /// 卡机复位并弹卡。
        /// </summary>
        /// <returns>成功返回0，失败返回错误代码。</returns>
        public string ResetAndCardOut()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x30, 0x31, 0x03, 0x02 };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            //退卡
            this.CardContainer.Clear();
            return result[3] == 0x4E ? result[result.Length - 3].ToString() : "0";
        }

        /// <summary>
        /// 获取IC卡片类型
        /// </summary>
        /// <returns></returns>
        public string GetCardType()
        {
            byte bbc = 0x02 ^ 0x00 ^ 0x02 ^ 0x31 ^ 0x31 ^ 0x03;
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x31, 0x31, 0x03, bbc };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            string s1 = ASCIIEncoding.ASCII.GetString(new byte[] { result[5] });
            string s2 = ASCIIEncoding.ASCII.GetString(new byte[] { result[6] });
            return s1 + s2;
        }

        /// <summary>
        /// 走卡位，走在IC卡操作位
        /// </summary>
        /// <returns></returns>
        public string SetCardICCardPosition()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x32, 0x2f, 0x03, new byte() };
            AppendBcc(cmd);
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            return string.Format("设置结果:{0}",
                ASCIIEncoding.ASCII.GetString(new byte[] { result[5] })
                );
        }

        /// <summary>
        /// 走卡位停在前端前端不持卡
        /// [清除读取的磁道信息]
        /// </summary>
        /// <returns></returns>
        public string SetCardStopPositionBehaviorCatchCard()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x32, 0x30, 0x03, new byte() };
            AppendBcc(cmd);
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            //清除缓存卡片数据
            CacheCard = null;
            return string.Format("设置结果:{0}",
                ASCIIEncoding.ASCII.GetString(new byte[] { result[5] })
                );
        }

        #region 磁卡读取操作
        /// <summary>
        /// 读磁卡。
        /// </summary>
        /// <returns>成功返回2磁道数据，失败返回错误代码。</returns>
        public string ReadMsg2()
        {
            byte[] cmd = new byte[] { 0x02, 0x00, 0x04, 0x45, 0x30, 0x30, 0x32, 0x03, 0x72 };
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            com.ClosePort();
            return result[11] == 0x4E ? result[12].ToString() : Encoding.ASCII.GetString(result.GetSubArray(12, result.IndexOf(0x1f, 3) - 12));
        }
        #endregion

        #region IC卡操作

        /// <summary>
        /// Ic卡冷复位
        /// </summary>
        /// <returns></returns>
        public string ICCardReset()
        {
            #region cmd build
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x37, 0x30, 0x03, new byte() };
            byte bss = cmd[0];
            int end = cmd.Length - 1;
            for (int i = 1; i < end; i++)
            {
                bss = (byte)(bss ^ cmd[i]);
            }
            cmd[end] = bss;
            #endregion

            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            int resetDataLength = result[7];
            byte[] resetData = result.Skip(8).Take(resetDataLength).ToArray();
            com.ClosePort();
            return string.Format("操作状态:{0} \r\n复位数据:{1}", ASCIIEncoding.ASCII.GetString(new byte[] { result[5] })
                , CodeConvert.ToHexString(resetData)
            );
        }

        /// <summary>
        /// Ic卡热冷复位
        /// </summary>
        /// <returns></returns>
        public string ICCardHotReset()
        {
            #region cmd build
            byte[] cmd = new byte[] { 0x02, 0x00, 0x02, 0x37, 0x2f, 0x03, new byte() };
            byte bss = cmd[0];
            int end = cmd.Length - 1;
            for (int i = 1; i < end; i++)
            {
                bss = (byte)(bss ^ cmd[i]);
            }
            cmd[end] = bss;
            #endregion

            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            int resetDataLength = result[7];
            byte[] resetData = result.Skip(8).Take(resetDataLength).ToArray();
            com.ClosePort();
            return string.Format("操作状态:{0} \r\n复位数据:{1}",
                ASCIIEncoding.ASCII.GetString(new byte[] { result[5] })
                , CodeConvert.ToHexString(resetData)
            );
        }

        /// <summary>
        /// IC卡APDU操作
        /// </summary>
        /// <param name="apdu">apdu指令</param>
        /// <returns></returns>
        public string SendAPDU(string apdu)
        {
            int apduLength = apdu.Length / 2;
            int dataLength = apduLength + 4;//通讯包长度=4+ C-APDU 包长度 n  (n 最大值为 262byte
            string apduLengthString = apduLength.ToString("x4");//通讯包长度 2 byte 
            string dataLengthString = dataLength.ToString("x4");

            #region Build Cmd
            List<byte> bytes = new List<byte>();
            bytes.Add(0x02);
            bytes.AddRange(CodeConvert.HexStringToByteArray(dataLengthString));
            bytes.Add(0x37);
            bytes.Add(0x31);
            bytes.AddRange(CodeConvert.HexStringToByteArray(apduLengthString));
            bytes.AddRange(CodeConvert.HexStringToByteArray(apdu));
            bytes.Add(0x03);
            bytes.Add(new byte());
            #endregion

            byte[] cmd = bytes.ToArray();
            AppendBcc(cmd);
            if (!com.IsOpen)
                com.OpenPort();
            byte[] result = com.SendCmd(cmd);
            int apduDataLength = result[6] * 16 * 16 + result[7];
            byte[] apduData = result.Skip(8).Take(apduDataLength).ToArray();
            com.ClosePort();
            string data = CodeConvert.ToHexString(apduData);
            if (!data.EndsWith("9000"))
                throw new CardReadException(string.Format("读卡错误,{0}！", data));
            data = data.Substring(0, data.Length - 4).Trim();
            return data;
        }
        #endregion

        #region Functions
        /// <summary>
        /// 构建指令增加BCC
        /// </summary>
        /// <param name="cmd"></param>
        public void AppendBcc(byte[] cmd)
        {
            byte bss = cmd[0];
            int end = cmd.Length - 1;
            for (int i = 1; i < end; i++)
            {
                bss = (byte)(bss ^ cmd[i]);
            }
            cmd[end] = bss;
        }
        #endregion

        #endregion

        #region 设备操作
        /// <summary>
        /// IC卡读取
        /// </summary>
        /// <param name="icCard"></param>
        private void Read(ICCard icCard)
        {
            ICCardHotReset();
            ICCardReader reader = new ICCardReader(this);
            reader.SelectPaySystem();
            reader.GetAID(icCard);
            reader.GetPDOL(icCard);
            reader.GPO(icCard);//应用初始化
            reader.GetData(icCard);//卡片数据读取
            reader.GENERATEARQC(icCard);//生成应用密文
        }
        /// <summary>
        /// 磁条卡读取
        /// </summary>
        /// <param name="magCard"></param>
        private void Read(MagCard magCard)
        {
            //读取2磁道数据
            magCard.Msg2 = ReadMsg2();
            //卡号
            magCard.CardNo = magCard.Msg2.Split('=')[0];

            //magCard.Msg2 = magCard.Msg2.Replace('=', 'D');
        }

        /// <summary>
        /// 读卡
        /// </summary>
        public void ReadCreditCard()
        {
            if (CardContainer.Count == 0)
                return;
            //卡机内有卡进行读卡操作
            string cardType = GetCardType();
            if (cardType.Equals(CardType.NoCard))
                throw new ApplicationException("卡机内无卡或卡片无法识别!");
            if (cardType.Equals(CardType.CPU_T_0)
                ||
                cardType.Equals(CardType.CPU_T_1)
                ||
                cardType.Equals(CardType.CPU_TYPE_A)
                ||
                cardType.Equals(CardType.CPU_TYPE_B)
                )
            {//IC卡操作
                ICCard creditCard = new ICCard();
                Read(creditCard);
                this.CardContainer[0] = creditCard;
                //缓存卡片数据
                this.CacheCard = new CreditCardCacheData() { CardNo = creditCard.CardNo, Msg2 = creditCard.Msg2 };
            }
            else
            {//磁卡操作
                MagCard creditCard = new MagCard();
                Read(creditCard);
                this.CardContainer[0] = creditCard;
                //缓存卡片数据
                this.CacheCard = new CreditCardCacheData() { CardNo = creditCard.CardNo, Msg2 = creditCard.Msg2 };
            }
        }

        /// <summary>
        /// 进卡操作
        /// </summary>
        public void CreditCardIn()
        {
            //设置进卡方式（磁卡方式进卡）
            SetFeedingMode();
            SetCarInPosition();//设置停卡位置
            while (!GetStatus())
            {//卡机内无卡时等待进卡
                if (IsCancelCardIn)//取消进卡
                {
                    SetFeedingMode_NoCardIn();//禁止进卡
                    return;
                }

                Thread.Sleep(500);
            }
            if (this.CardContainer.Count > 0)
                throw new ApplicationException("卡机内有卡");
            if (this.CardContainer.Count == 0)
            {
                this.CardContainer.Add(new CreditCard());//进卡操作
            }
        }
        /// <summary>
        /// 取消进卡
        /// </summary>
        public void CancelCardIn()
        {
            IsCancelCardIn = true;
            Thread.Sleep(600);
        }
        #endregion
    }
}
