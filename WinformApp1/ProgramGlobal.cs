using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformApp1.Models;

namespace WinformApp1
{
    /// <summary>
    /// 프로그램 전역 클래스
    /// </summary>
    internal class ProgramGlobal
    {
        static SerialPort _scanner_Port = new SerialPort()
        {
            PortName = "COM3",
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };
        /// <summary>
        /// 바코드스캐너 XerialPort 객체 반환
        /// </summary>
        /// <param name="callback">스캔 수신 이벤트 시 콜백함수</param>
        /// <returns>XerialPort</returns>
        public static Ports.XerialPort GetScannerXerial(Action<byte[]> callback) => new Ports.XerialPort(_scanner_Port, callback);

        static SerialPort _scale_Port = new SerialPort()
        {
            PortName = "COM4",
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };
        /// <summary>
        /// 중량계 XerialPort 객체 반환
        /// </summary>
        /// <param name="callback">중량 수신 이벤트 시 콜백함수</param>
        /// <returns>XerialPort</returns>
        public static Ports.XerialPort GetScaleXerial(Action<byte[]> callback) => new Ports.XerialPort(_scale_Port, callback);
        
        /// <summary>
        /// 화면상태 메세지 출력 대리자
        /// </summary>
        static public Action<string> StatusMessageShow;
    }
}
