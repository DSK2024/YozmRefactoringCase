using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformApp1.Models;

namespace WinformApp1
{
    internal class Global
    {
        static SerialPort _scanner_Port = new SerialPort()
        {
            PortName = "COM3",
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };
        public static XerialPort ScannerXPort(Action<byte[]> callback) => new XerialPort(_scanner_Port, callback);

        static SerialPort _scale_Port = new SerialPort()
        {
            PortName = "COM4",
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };
        public static XerialPort ScaleXPort(Action<byte[]> callback) => new XerialPort(_scale_Port, callback);
    }
}
