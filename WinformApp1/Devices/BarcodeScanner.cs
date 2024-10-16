using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinformApp1.Ports;

namespace WinformApp1.Devices
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
    public class BarcodeScanner : Device
    {
        public BarcodeScanner(IXerialPort port) : base(port) { }

        public override Action ConnectWorkerCallback()
        {
            return new Action(() =>
            {
                while (true)
                {
                    if (!XPort.IsOpen)
                    {
                        try
                        {
                            XPort.Open();
                            if (XPort.IsOpen)
                                ProgramGlobal.StatusMessageShow("바코드스캐너 연결OK");
                        }
                        catch (IOException ex)
                        {
                            ProgramGlobal.StatusMessageShow("바코드스캐너 연결에 문제가 있습니다. 케이블 연결 여부 혹은 스캐너 상태를 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            ProgramGlobal.StatusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(4500);
                }
            });
        }
    }
}
