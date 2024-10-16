using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 판정조건 마스터 인터페이스
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// 최종 판정
        /// </summary>
        /// <returns></returns>
        bool Judgment();
    }
}
