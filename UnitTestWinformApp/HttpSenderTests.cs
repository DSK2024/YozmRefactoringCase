using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using WinformApp1.Services;
using WinformApp1.Models;

namespace UnitTestWinformApp
{
    [TestClass]
    public class HttpSenderTests
    {
        IHttpSender _httpSenderMock()
        {
            var serviceMock = new Mock<IHttpSender>();
            serviceMock.Setup(s => s.Send(It.IsAny<string>(), It.IsAny<InspectResult>())).Returns(true);
            return serviceMock.Object;
        }
        [TestMethod]
        public void HTTP서버_데이터전송OK()
        {
            var result = new InspectResult()
            {
                BarcodeData = "##HY#K3A08#AB12#7.5#0001",
                PartNo = "AB12",
                Weight = "7.68",
                Result = "OK",
            };
            var mock = _httpSenderMock();

            Assert.AreEqual(mock.Send("/api/receive/inspect", result), true);
        }
        [TestMethod]
        public void HTTP서버_데이터전송NO()
        {
            var result = new InspectResult()
            {
                BarcodeData = "##HY#K3A08#AB12#7.5#0001",
                PartNo = "AB12",
                Weight = "7.68",
                Result = "OK",
            };
            var serviceMock = new Mock<IHttpSender>();
            serviceMock.Setup(s => s.Send(It.IsAny<string>(), It.IsAny<InspectResult>())).Returns(false);

            Assert.AreNotEqual(serviceMock.Object.Send("/api/receive/inspect", result), true);
        }
    }
}
