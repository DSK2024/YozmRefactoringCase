using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    public class WeightScaler
    {
        IXerialPort _port;
        bool _is_start = false;
        Thread tConnChecker;
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
