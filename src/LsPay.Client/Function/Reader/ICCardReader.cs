using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LsPay.Client.Interface;
using LsPay.Client.Function.Code;
using LsPay.Client.agreements.ISO7816;
using LsPay.Client.Model.Entity;
using LsPay.Client.Data;
using LsPay.Service.Wcf.Model.Card;
using LsPay.Client.Exception;

namespace LsPay.Client.WinService.Service.FunctionReader
{
    /// <summary>
    /// IC卡读卡类
    /// </summary>
    public class ICCardReader
    {
        /// <summary>
        /// 读卡器设备
        /// </summary>
        public ICardReaderEquipment CardReader = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="m100"></param>
        public ICCardReader(ICardReaderEquipment cardReader) { CardReader = cardReader; }

        /// <summary>
        /// 选择支付环境
        /// </summary>
        /// <returns></returns>
        public List<TLVEntity> SelectPaySystem()
        {
            string result = CardReader.SendAPDU(APDUCommand.SELECT_PSE);
            var tlvList = TLVHelper.ToTLVEntityList(result);
            return tlvList;
        }

        /// <summary>
        /// 获取应用标识符
        /// </summary>
        /// <returns></returns>
        public void GetAID(ICCard card)
        {
            string result = CardReader.SendAPDU(APDUCommand.GETDATA);
            List<TLVEntity> tlvList = TLVHelper.ToTLVEntityList(result);
            var entity = TLVHelper.GetValueByTag(tlvList, EMVTag.AID);
            if (entity == null)
                throw new CardReadException("获取不到卡片的应用标识符！");
            card.AID = CodeConvert.ToHexString(entity.Value);
        }

        /// <summary>
        /// 获取处理选项数据对象列表（PDOL）
        /// </summary>
        /// <returns></returns>
        public void GetPDOL(ICCard card)
        {
            string aid = card.AID;
            APDUEntity apdu = new APDUEntity("00", APDU_INS.SELECT, "04", "00", string.Format("{0}{1}", (aid.Length / 2).ToString("x2"), aid));
            string result = CardReader.SendAPDU(apdu.ToString());
            List<TLVEntity> tlvList = TLVHelper.ToTLVEntityList(result);
            var entity = TLVHelper.GetValueByTag(tlvList, EMVTag.PDOL);
            if (entity == null)
                throw new CardReadException("获取不到卡片的处理选项数据对象列表！");
            card.GPOL = CodeConvert.ToTLStringList(entity.Value);
        }

        /// <summary>
        /// 获取处理选项
        /// </summary>
        /// <param name="card"></param>
        public void GPO(ICCard card)
        {
            StringBuilder body = new StringBuilder();
            List<string> gpoList = new List<string>();
            if (card.GPOL == null || card.GPOL.Count == 0)
                throw new CardReadException("未读取到处理选项列表");
            gpoList = card.GPOL.ToList();
            gpoList.ForEach(pdo => body.Append(PublicStaticData.PDOL[pdo]));
            body.Insert(0, (body.Length / 2).ToString("x2"));
            body.Insert(0, "83");
            body.Insert(0, (body.Length / 2).ToString("x2"));
            APDUEntity GPO = new APDUEntity("80", "A8", "00", "00", body.ToString());
            string result = CardReader.SendAPDU(GPO.ToString());
            List<TLVEntity> tlvList = TLVHelper.ToTLVEntityList(result);
            TLVEntity entity = null;
            if (tlvList != null && tlvList.Count > 0)
                entity = tlvList[0];
            card.AIP = CodeConvert.ToHexString(entity.Value.Take(2).ToArray());//前两位是AIP
            List<string> afl = new List<string>();
            for (int i = 2; i < entity.Value.Length; i = i + 4)
            {
                afl.Add(CodeConvert.ToHexString(entity.Value.Take(i + 4).Skip(i).ToArray()));
            }
            card.AFL = afl;
        }
        /// <summary>
        /// 获取所有应用文件的数据
        /// </summary>
        /// <param name="card">卡片</param>
        /// <returns></returns>
        public List<TLVEntity> GetData(ICCard card)
        {
            List<TLVEntity> tlvList = new List<TLVEntity>();
            List<string> dataList = new List<string>();
            card.AFL.ToList().ForEach(af => GetDataBySF(dataList, af));
            foreach (string data in dataList)
            {
                tlvList.AddRange(TLVHelper.ToTLVEntityList(data));
            }
            //card.AppFileDataList = tlvList;
            //卡号
            card.CardNo = CodeConvert.ToHexString(TLVHelper.GetValueByTag(tlvList, EMVTag.PAN).Value).Replace("F", "");
            //序列号 858323域使用
            card.PANSerialNum = CodeConvert.ToHexString(TLVHelper.GetValueByTag(tlvList, EMVTag.PANSerialNum).Value);
            card.AppVersionNo = CodeConvert.ToHexString(TLVHelper.GetValueByTag(tlvList, EMVTag.AppVersionNo).Value);//应用版本号
            string msg2 = CodeConvert.ToHexString(TLVHelper.GetValueByTag(tlvList, EMVTag.Msg2).Value);//等价2磁道数据
            msg2 = msg2.ToUpper().Replace("D", "=");//有些银行为对为D的情况做处理
            if (msg2.EndsWith("F"))
            {
                msg2 = msg2.Substring(0, msg2.Length - 1);
            }
            if (msg2.StartsWith(card.CardNo + "="))
            {
                string dateString = msg2.Split('=')[1].Substring(0, 4);
                dateString = string.Format("{0}-{1}-{2}", "20" + dateString.Substring(0, 2), dateString.Substring(2, 2), "01");
                card.EffectiveDate = Convert.ToDateTime(dateString);
            }
            card.Msg2 = msg2;//等价2磁道数据
            //卡风险管理数据对象列表1 用于获取应用密文
            card.CDOL1 = CodeConvert.ToTLStringList(TLVHelper.GetValueByTag(tlvList, EMVTag.CDOL1).Value);
            card.CDOL2 = CodeConvert.ToTLStringList(TLVHelper.GetValueByTag(tlvList, EMVTag.CDOL2).Value);
            var proid = TLVHelper.GetValueByTag(tlvList, EMVTag.ProductId);
            if (proid != null)
                card.ProductId = CodeConvert.ToHexString(proid.Value);
            return tlvList;
        }
        /// <summary>
        /// 获取应用文件的数据
        /// </summary>
        /// <param name="dataList">数据列表</param>
        /// <param name="appFile">应用文件名</param>
        public void GetDataBySF(List<string> dataList, string appFile)
        {
            byte[] sf = CodeConvert.HexStringToByteArray(appFile);
            int sfi = sf[0];
            int startIndex = sf[1];
            int endIndex = sf[2];
            string body = "00";
            for (int i = startIndex; i <= endIndex; i++)
            {
                string cla = "00";
                string ins = APDU_INS.READ_RECODE;
                string p1 = i.ToString("x2");
                string p2 = (sfi + 4).ToString("x2");
                APDUEntity entity = new APDUEntity(cla, ins, p1, p2, body);
                string result = CardReader.SendAPDU(entity.ToString());
                dataList.Add(result);
            }
        }
        /// <summary>
        /// 获取应用密文
        /// </summary>
        /// <param name="card">卡片信息</param>
        /// <returns></returns>
        public void GENERATEARQC(ICCard card,string transactionType = TransactionType.Pay)
        {
            PublicStaticData.TransactionType = TransactionType.Pay;
            Random random = new Random();
            PublicStaticData.RadomData = CodeConvert.ToHexString(new byte[] { (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256) });
            card.RadomData = PublicStaticData.RadomData;
            StringBuilder cdol1Builder = new StringBuilder();
            card.CDOL1.ToList().ForEach(cdo => cdol1Builder.Append(PublicStaticData.CDOL1[cdo]));
            cdol1Builder.Insert(0, (cdol1Builder.Length / 2).ToString("x2"));
            APDUEntity entity = new APDUEntity("80", "AE", "80", "00", cdol1Builder.ToString());
            string arqc = CardReader.SendAPDU(entity.ToString());

            List<TLVEntity> entityList = TLVHelper.ToTLVEntityList(arqc);
            if (entityList.Count == 0)
                throw new CardReadException("获取应用密文(ARQC)失败");
            TLVEntity arqcEntity = entityList[0];
            card.CID = arqcEntity.Value[0].ToString("x2"); //密文信息 L:1
            card.ATC = CodeConvert.ToHexString(new byte[] { arqcEntity.Value[1], arqcEntity.Value[2] });//ATC 应用交易计数器 L:2
            card.AC = CodeConvert.ToHexString(arqcEntity.Value.Take(11).Skip(3).ToArray());//AC应用密文 L:8
            card.IssBankAppData = CodeConvert.ToHexString(arqcEntity.Value.Skip(11).ToArray());//发卡行应用数据
        }

        /// <summary>
        /// 发卡行外部认证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IssueBankAuthenticate(string data)
        {
            //支付成功获取发卡行返回的55域数据
            byte[] part55Data = CodeConvert.HexStringToByteArray(data);
            List<TLVEntity> tlvList = TLVPackage.Construct(part55Data);
            TLVEntity entity = TLVHelper.GetValueByTag(tlvList, "91");//获取发卡行认证数据
            if (entity != null)
            {
                APDUEntity apdu = new APDUEntity("00", "82", "00", "00", CodeConvert.ToHexString(entity.Length) + CodeConvert.ToHexString(entity.Value));
                string result = CardReader.SendAPDU(apdu.ToString());
                return true;// result.Equals(StatusCode.Success) == result.Equals(StatusCode.Authenticated);
            }
            else
                return true;

        }
    }
}
