using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 허용오차 포함 판정조건 클래스
    /// </summary>
    public class ConditionMarginError : ICondition
    {
        float _compareVal;
        float _allowError;
        float _testValue;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compareVal">기준값</param>
        /// <param name="allowError">허용오차</param>
        /// <param name="testValue">검정값</param>
        public ConditionMarginError(float compareVal, float allowError, float testValue)
        {
            _compareVal = compareVal;
            _allowError = allowError;
            _testValue = testValue;
        }

        public bool ConditionExecution()
        {
            return _compareVal + _allowError > _testValue && _compareVal - _allowError < _testValue;
        }
    }
}
