using Moq;
using WinformApp1.Models;

namespace TestRefactoring
{
    public class BarcodeScannerTests
    {
        [Test]
        public void 스캐너_연결안됨()
        {
            var mock = new Mock<BarcodeScanner>();
            mock.Setup(b => b.ConnectStart()).Callback(() => 
            {
                mock.Setup(b => b.Status == BarcodeScannerStatus.Disopen);
            });
            var scanner = mock.Object;
            Assert.Equals(scanner.Status, BarcodeScannerStatus.Disopen);
        }
    }
}