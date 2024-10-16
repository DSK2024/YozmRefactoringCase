using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Devices
{
    internal interface IConnectWorker
    {
        /// <summary>
        /// 연결모니터링 시 구체적인 행위 메소드를 반환하도록 구현된 메소드
        /// </summary>
        /// <returns></returns>
        Action ConnectWorkerCallback();
    }
}
