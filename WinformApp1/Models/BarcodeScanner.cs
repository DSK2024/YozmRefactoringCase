using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    public class BarcodeScanner
    {
        IXerialPort _port;
        bool _isStart;
        Thread _tConnCheck;
        public BarcodeScannerStatus Status
        {
            get {
                if (_port.IsOpen && _isStart == false)
                    return BarcodeScannerStatus.Opened;
                else if (_port.IsOpen && _isStart == true)
                    return BarcodeScannerStatus.Started;
                else
                    return BarcodeScannerStatus.Disopen;
            }
        }

        public BarcodeScanner(IXerialPort port)
        {
            _port = port;
        }

        public void ConnectStart()
        {
            _tConnCheck = new Thread(() =>
            {
                while (true)
                {
                    if (!_port.IsOpen)
                    {
                        try
                        {
                            _port.Open();
                            if (_port.IsOpen)
                                Program.statusMessageShow("바코드스캐너 연결OK");
                        }
                        catch (IOException ex)
                        {
                            Program.statusMessageShow("바코드스캐너 연결에 문제가 있습니다. 케이블 연결 여부 혹은 스캐너 상태를 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            Program.statusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(4500);
                }
            });
            _tConnCheck.IsBackground = true;
            _tConnCheck.Start();
            _isStart = true;
        }
    }
}
