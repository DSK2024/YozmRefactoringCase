using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinformApp1.Ports;

namespace WinformApp1.Devices
{
    public abstract class Device : IConnectWorker
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

        public abstract Action ConnectWorkerCallback();

        /// <summary>
        /// 디바이스 가동 및 연결 시작
        /// </summary>
        public void StartRun()
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
