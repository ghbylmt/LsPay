﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LsPay.Service.Pays.XuanLifePay;
using LsPay.Service.Pays.XuanLifePay.Sdk.Dtos.request;

namespace UnitTest.Client
{
    /// <summary>
    /// XuanPayTEst 的摘要说明
    /// </summary>
    [TestClass]
    public class XuanPayTest
    {
        public XuanPayTest()
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
        public void ActiveDevice()
        {
            DateTime dtime = DateTime.Now;
            PayUtil.Precreate(new ActiveDeviceDto {
                activeCode = ""
            });
        }

        [TestMethod]
        public void Precreate()
        {
            DateTime dtime = DateTime.Now;
            PayUtil.Precreate(new TradePreCreateDto {
                discountable_amount=0,
                undiscountable_amount=1,
                total_amount=1,
                channel = PayChannel.Alipay,
                store_id= 1000566,
                terminal_id="001",
                out_trade_no= dtime.ToString("yyyyMMddHHmmss")+ dtime.Millisecond.ToString().PadLeft(4,'0'),
                subject="测试商品"
            });
        }
    }
}