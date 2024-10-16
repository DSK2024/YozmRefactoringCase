using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 판정 클래스
    /// </summary>
    public class Judgmenter
    {
        JudgmentType _type;
        struct MarginErrorSet
        {
            public const float allowError = 0.5f;
        }
        public Judgmenter(JudgmentType type) 
        { 
            _type = type;
        }

        public bool Condition(float compare, float mean)
        {
            switch (_type)
            {
                case JudgmentType.MarginOfError:
                    return compare + MarginErrorSet.allowError > mean && compare - MarginErrorSet.allowError < mean;
            }
            return false;
        }
    }
}
