using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    public class BarcodeScanner
    {
        IXerialPort _port;
        BarcodeScannerStatus _status;
        Thread _thread;
        public BarcodeScannerStatus Status => _status;

        public BarcodeScanner(IXerialPort port)
        {
            _port = port;
        }

        public void ConnectStart()
        {

        }
    }
}
