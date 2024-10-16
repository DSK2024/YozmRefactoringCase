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
    public class Judge
    {
        ICondition _condition;
        public Judge(ICondition condition)
        {
            _condition = condition;
        }

        /// <summary>
        /// 판정
        /// </summary>
        /// <returns></returns>
        public bool Judgment()
        {
            return _condition.ConditionExecution();
        }
    }
}
