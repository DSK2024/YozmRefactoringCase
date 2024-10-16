using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WinformApp1.Devices;
using WinformApp1.Models;
using WinformApp1.Ports;

namespace UnitTestWinformApp
{
    [TestClass]
    public class BarcodeScannerTests
    {
        const string _barcode_data_sample = "##HY#K3A08#AB12#7.5#0001";
        IXerialPort GetXerialPortModk()
        {
            var xerialMock = new Mock<IXerialPort>();
            xerialMock.Setup(x => x.Open());
            xerialMock.Setup(x => x.IsOpen).Returns(true);
            return xerialMock.Object;
        }

        [TestMethod]
        public void 스캐너_연결OK()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            Assert.AreEqual(scanner.Status, DeviceStatus.Opened);
        }

        
        [TestMethod]
        public void 스캐너_가동시작OK()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            scanner.ConnectWorkerStart();
            Assert.AreEqual(scanner.Status, DeviceStatus.Started);
        }

        [TestMethod]
        public void 시리얼포트_연결OK()
        {
            var port = GetXerialPortModk();
            Assert.AreEqual(port.IsOpen, true);
        }

        [TestMethod]
        public void 바코드데이터_컴퍼니얻기()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            var info = new BarcodeInfo(_barcode_data_sample);
            Assert.AreEqual(info.Company, "HY");
        }

        [TestMethod]
        public void 바코드데이터_날짜형식얻기()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            var info = new BarcodeInfo(_barcode_data_sample);
            Assert.AreEqual(info.DateForm, "K3A08");
        }

        [TestMethod]
        public void 바코드데이터_품번얻기()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            var info = new BarcodeInfo(_barcode_data_sample);
            Assert.AreEqual(info.PartNo, "AB12");
        }

        [TestMethod]
        public void 바코드데이터_표준중량얻기()
        {
            var port = GetXerialPortModk();
            var scanner = new BarcodeScanner(port);
            var info = new BarcodeInfo(_barcode_data_sample);
            Assert.AreEqual(info.StandardWeight, 7.5f);
        }
    }
}
