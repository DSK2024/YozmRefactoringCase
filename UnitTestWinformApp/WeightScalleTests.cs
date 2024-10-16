﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Runtime.InteropServices;
using WinformApp1.Models;

namespace UnitTestWinformApp
{
    [TestClass]
    public class WeightScalleTests
    {
        IXerialPort GetXerialPortModk()
        {
            var xerialMock = new Mock<IXerialPort>();
            xerialMock.Setup(x => x.Open());
            xerialMock.Setup(x => x.IsOpen).Returns(true);
            return xerialMock.Object;
        }

        [TestMethod]
        public void 중량계_연결OK()
        {
            var port = GetXerialPortModk();
            var scale = new WeightScaler(port);
            Assert.AreEqual(scale.Status, WeightScalerStatus.Opened);
        }

        [TestMethod]
        public void 중량계_시작OK()
        {
            var port = GetXerialPortModk();
            var scale = new WeightScaler(port);
            scale.ConnectStart();
            Assert.AreEqual(scale.Status, WeightScalerStatus.Started);
        }

        [TestMethod]
        public void 판정결과_OK()
        {
            var compare = 7.5f;
            var err = 0.5f;
            var val = 7.75f;
            var condition = new ConditionMarginError(compare, err, val);
            var judg = new Judgmenter();
            var result = judg.Judgment(condition);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void 판정결과_NG()
        {
            var compare = 7.5f;
            var err = 0.5f;
            var val = 8.9f;
            var condition = new ConditionMarginError(compare, err, val);
            var judg = new Judgmenter();
            var result = judg.Judgment(condition);

            Assert.AreEqual(result, false);
        }
    }
}
