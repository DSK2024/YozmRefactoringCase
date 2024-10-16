using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinformApp1.Ports;

namespace WinformApp1.Devices
{
    public abstract class Device
    {
        protected bool IsConnectWorkerRun;
        protected IXerialPort XPort;
        protected Thread ConnectWorker;
        public Device(IXerialPort port)
        {
            XPort = port;
        }

        /// <summary>
        /// 중량계 연결상태
        /// </summary>
        public DeviceStatus Status
        {
            get
            {
                if (XPort.IsOpen && IsConnectWorkerRun == false)
                    return DeviceStatus.Opened;
                else if (XPort.IsOpen && IsConnectWorkerRun == true)
                    return DeviceStatus.Started;
                else
                    return DeviceStatus.Disopen;
            }
        }

        /// <summary>
        /// 연결모니터링 시 구체적인 행위 메소드를 반환하도록 구현된 메소드
        /// </summary>
        /// <returns></returns>
        public abstract Action ConnectWorkerCallback();

        /// <summary>
        /// 디바이스 연결모니터링 백그라운드 실행
        /// </summary>
        public void ConnectWorkerStart()
        {
            var callback = ConnectWorkerCallback();
            ConnectWorker = new Thread(new ThreadStart(callback));
            if (ConnectWorker != null)
            {
                try
                {
                    ConnectWorker.IsBackground = true;
                    ConnectWorker.Start();
                    IsConnectWorkerRun = true;
                }
                catch (Exception ex)
                {
                    ProgramGlobal.StatusMessageShow(ex.Message);
                }
            }
        }
    }
}
