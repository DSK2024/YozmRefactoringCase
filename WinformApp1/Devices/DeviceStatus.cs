using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Devices
{
    public enum DeviceStatus
    {
        /// <summary>
        /// 포트 연결없음
        /// </summary>
        Disopen,
        /// <summary>
        /// 포트 연결되엇음
        /// </summary>
        Opened,
        /// <summary>
        /// 스캐너 시작되었음.
        /// </summary>
        Started,
    }
}
