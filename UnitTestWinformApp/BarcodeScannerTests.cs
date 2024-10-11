using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WinformApp1.Models;

namespace UnitTestWinformApp
{
    [TestClass]
    public class BarcodeScannerTests
    {
        [TestMethod]
        public void 스캐너_연결OK()
        {
            var xerialMock = new Mock<IXerialPort>();
            xerialMock.Setup(x => x.Open());
            xerialMock.Setup(x => x.IsOpen).Returns(true);
            var port = xerialMock.Object;

            var scanner = new BarcodeScanner(port);
            Assert.AreEqual(scanner.Status, BarcodeScannerStatus.Opened);
        }


        [TestMethod]
        public void 스캐너_연결시작OK()
        {
            var xerialMock = new Mock<IXerialPort>();
            xerialMock.Setup(x => x.Open());
            xerialMock.Setup(x => x.IsOpen).Returns(true);
            var port = xerialMock.Object;

            var scanner = new BarcodeScanner(port);
            scanner.ConnectStart();
            Assert.AreEqual(scanner.Status, BarcodeScannerStatus.Start);
        }
    }
}
