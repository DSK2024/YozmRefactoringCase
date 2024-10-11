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
        Thread _thread;
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
            _isStart = true;
            //_thread = new Thread(() => {
            //    while (true)
            //    {
            //        if (!serialPort1.IsOpen)
            //        {
            //            try
            //            {
            //                serialPort1.Open();
            //                if (serialPort1.IsOpen)
            //                    StatusMessageShow("바코드스캐너 연결OK");
            //            }
            //            catch (IOException ex)
            //            {
            //                StatusMessageShow("바코드스캐너 연결에 문제가 있습니다. 케이블 연결 여부 혹은 스캐너 상태를 확인하세요.");
            //            }
            //            catch (Exception ex)
            //            {
            //                StatusMessageShow(ex.Message);
            //            }
            //        }
            //        Thread.Sleep(4500);
            //    }
            //});
        }
    }
}
