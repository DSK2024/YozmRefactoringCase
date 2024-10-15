using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 중량계 연결상태 열거자
    /// </summary>
    public enum WeightScalerStatus
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
