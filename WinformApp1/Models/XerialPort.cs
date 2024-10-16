using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.Models
{
    /// <summary>
    /// 시리얼포트 클래스
    /// </summary>
    public class XerialPort : IXerialPort
    {
        SerialPort _serialPort;
        public bool IsOpen => _serialPort.IsOpen;
        public int BytesToRead => _serialPort.BytesToRead;
        Action<byte[]> _Readcallback;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serialport">SerialPort 개체</param>
        /// <param name="readAction">수신 이벤트 시 호출될 콜백메소드</param>
        public XerialPort(SerialPort serialport, Action<byte[]> readCallback)
        {
            serialport.DataReceived += DataReadEvent;
            _serialPort = serialport;
            _Readcallback = readCallback;
        }

        void DataReadEvent(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                var bytes = _serialPort.BytesToRead;
                var buffer = new byte[bytes];
                Read(buffer, 0, bytes);
                _Readcallback(buffer);
            }
            catch (IOException ex)
            {
                Program.statusMessageShow(ex.Message);
            }
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        public string ReadExisting()
        {
            return _serialPort.ReadExisting();
        }
    }
}
