using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
    }
}
