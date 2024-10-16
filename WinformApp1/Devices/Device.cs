using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformApp1.Models;

namespace WinformApp1.Devices
{
    public abstract class Device
    {
        protected bool IsStart;
        protected IXerialPort XPort;
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
                if (XPort.IsOpen && IsStart == false)
                    return DeviceStatus.Opened;
                else if (XPort.IsOpen && IsStart == true)
                    return DeviceStatus.Started;
                else
                    return DeviceStatus.Disopen;
            }
        }

        public abstract void ConnectStart();
    }
}
