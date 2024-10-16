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
    /// <summary>
    /// 바코드스캐너 클래스
    /// </summary>
    /// <example>
    /// //생성 방법
    /// var barcodeReadingCallback = new Action<byte[]>((buffer) => Console.WriteLine("Read : {0}", buffer));
    /// var serial = new SerialPort();
    /// var port = new XerialPort(serial, barcodeReadingCallback);
    /// var scanner = new BarcodeScanner(port);
    /// //바코드 연결 백그라운드 실행
    /// scanner.ConnectStart();
    /// </example>
    public class BarcodeScanner
    {
        IXerialPort _port;
        bool _isStart;
        Thread _tConnCheck;
        /// <summary>
        /// 중량계 연결상태
        /// </summary>
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

        /// <summary>
        /// 바코드스캐너 연속 연결 백그라운드 실행
        /// </summary>
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
