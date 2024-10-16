using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 판정자 클래스
    /// </summary>
    public class Judgmenter
    {
        public bool Judgment(ICondition condition)
        {
            return condition.Judgment();
        }
    }
}
