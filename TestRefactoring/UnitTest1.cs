using WinformApp1.Models;

namespace TestRefactoring
{
    public class BarcodeScannerTests
    {
        [Test]
        public void ��ĳ��_����ȵ�()
        {
            var port = new XerialPort();
            var scanner = new BarcodeScanner(port);
            scanner.ConnectStart();
            Assert.Equals(scanner.Status, BarcodeScannerStatus.Disopen);
        }

        [Test]
        public void ��ĳ��_�����ͼ���()
        {
            var port = new XerialPort();
            var scanner = new BarcodeScanner(port);
            scanner.ConnectStart();

            Assert.Equals(scanner.Status, BarcodeScannerStatus.Start);
        }
    }
}