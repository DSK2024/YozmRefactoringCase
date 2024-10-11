using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 바코드스캐너 연결상태 열거자
    /// </summary>
    public enum BarcodeScannerStatus
    {
        Disopen,
        Opened,
        Start,
    }
}
