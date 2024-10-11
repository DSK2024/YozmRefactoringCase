using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.Models
{
    public class XerialPort : IXerialPort
    {
        public bool IsOpen => _serialPort.IsOpen;
        SerialPort _serialPort;
        public XerialPort(SerialPort serialport)
        {
            _serialPort = serialport;
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public string ReadExisting()
        {
            throw new NotImplementedException();
        }
    }
}
