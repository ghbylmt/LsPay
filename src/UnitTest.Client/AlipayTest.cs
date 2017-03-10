using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LsPay.Service.Wcf.Model.Alipay;
using LsPay.Service.Wcf.Model.Alipay.response;
using LsPay.Client.Equipment;

namespace UnitTest.Client
{
    /// <summary>
    /// AlipayTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AlipayTest
    {
        public AlipayTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Precreate()
        {
            using (SelfServiceEquipment _equipment = new SelfServiceEquipment())
            {
                string _trade_no = Guid.NewGuid().ToString().Replace("-", "");
                PrecreateModel precreateModel = new PrecreateModel()
                {
                    out_trade_no = _trade_no,
                    timeout_express = "5m",
                    total_amount = "0.01",
                    store_id = "TEST_001",
                    subject = "支付测试",
                    undiscountable_amount = "0.01",
                    terminal_id = "t_0001",
                    operator_id = "top_0001",
                    body = "支付测试",
                    goods_detail = new List<GoodsDetailModel> { new GoodsDetailModel() { alipay_goods_id = "001", body = "汽车票", quantity = "1", price = "0.01", goods_id = "1", goods_name = "天津-北京", goods_category = "" } }
                };
                PrecreateResponseModel responseModel = _equipment.PreCreate(precreateModel);
            }   
        }
    }
}
