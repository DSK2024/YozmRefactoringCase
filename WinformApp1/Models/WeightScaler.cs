using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinformApp1.Models
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
    public class WeightScaler
    {
        IXerialPort _port;
        bool _is_start = false;
        Thread tConnChecker;
        /// <summary>
        /// 중량계 연결상태
        /// </summary>
        public WeightScalerStatus Status
        {
            get
            {
                if (!_is_start && _port.IsOpen)
                {
                    return WeightScalerStatus.Opened;
                }
                else if (_is_start && _port.IsOpen)
                {
                    return WeightScalerStatus.Started;
                }
                else
                {
                    return WeightScalerStatus.Disopen;
                }
            }
        }
        public WeightScaler(IXerialPort port)
        {
            _port = port;
        }

        /// <summary>
        /// 중량계 연속연결 백그라운드 실행
        /// </summary>
        public void ConnectStart()
        {
            tConnChecker = new Thread(() =>
            {
                while (true)
                {
                    if (!_port.IsOpen)
                    {
                        try
                        {
                            _port.Open();
                            if (_port.IsOpen)
                                Program.statusMessageShow("중량계 연결OK");
                        }
                        catch (IOException ex)
                        {
                            Program.statusMessageShow("중량계 연결에 문제가 있습니다. 케이블 연결 여부 혹은 중량계 전원을 확인하세요.");
                        }
                        catch (Exception ex)
                        {
                            Program.statusMessageShow(ex.Message);
                        }
                    }
                    Thread.Sleep(5500);
                }
            });

            tConnChecker.IsBackground = true;
            tConnChecker.Start();
            _is_start = true;
        }
    }
}
