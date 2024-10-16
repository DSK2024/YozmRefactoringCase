using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinformApp1.Models;

namespace WinformApp1.Devices
{
    /// <summary>
    /// 중량계 클래스
    /// </summary>
    /// <example>
    /// //생성 방법
    /// var ScaleReadingCallback = new Action<byte[]>((buffer) => Console.WriteLine("Read : {0}", buffer));
    /// var serial = new SerialPort();
    /// var port = new XerialPort(serial, ScaleReadingCallback);
    /// var scale = new BarcodeScanner(port);
    /// //바코드 연결 백그라운드 실행
    /// scale.ConnectStart();
    /// </example>
    public class WeightScaler : Device
    {
        Thread tConnChecker;
        public WeightScaler(IXerialPort port) : base(port) { }

        /// <summary>
        /// 중량계 연속연결 백그라운드 실행
        /// </summary>
        public override void ConnectStart()
        {
            tConnChecker = new Thread(() =>
            {
                while (true)
                {
                    if (!XPort.IsOpen)
                    {
                        try
                        {
                            XPort.Open();
                            if (XPort.IsOpen)
                                ProgramGlobal.StatusMessageShow("중량계 연결OK");
                        }
                        catch (IOException ex)
                        {
                            ProgramGlobal.StatusMessageShow("중량계 연결에 문제가 있습니다. 케이블 연결 여부 혹은 중량계 전원을 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            ProgramGlobal.StatusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(5500);
                }
            });

            tConnChecker.IsBackground = true;
            tConnChecker.Start();
            IsStart = true;
        }
    }
}
