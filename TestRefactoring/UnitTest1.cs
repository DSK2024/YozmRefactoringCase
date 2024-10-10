using WinformApp1.Models;

namespace TestRefactoring
{
    public class BarcodeScannerTests
    {
        [Test]
        public void 스캐너_연결안됨()
        {
            var port = new XerialPort();
            var scanner = new BarcodeScanner(port);
            scanner.ConnectStart();
            Assert.Equals(scanner.Status, BarcodeScannerStatus.Disopen);
        }

        [Test]
        public void 스캐너_데이터수신()
        {
            var port = new XerialPort();
            var scanner = new BarcodeScanner(port);
            scanner.ConnectStart();

            Assert.Equals(scanner.Status, BarcodeScannerStatus.Start);
        }
    }
}