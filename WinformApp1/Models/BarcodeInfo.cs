using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WinformApp1.Models
{
    public class BarcodeInfo
    {
        const int _COMPANY_START_STEP = 2;
        const int _DATEFORM_START_STEP = 3;
        const int _PART_NO_START_STEP = 4;
        const int _STD_WEIGHT_START_STEP = 5;
        const char _BARCODE_STEP_DELIMITER = '#';
        readonly string _barcodeData;
        public BarcodeInfo(string barcodeData)
        {
            _barcodeData = barcodeData;
        }
        string[] SplitData => _barcodeData.Split(_BARCODE_STEP_DELIMITER);
        /// <summary>
        /// 컴퍼니
        /// </summary>
        public string Company => SplitData[_COMPANY_START_STEP];
        /// <summary>
        /// 날짜형식
        /// </summary>
        public string DateForm => SplitData[_DATEFORM_START_STEP];
        /// <summary>
        /// 품번
        /// </summary>
        public string PartNo => SplitData[_PART_NO_START_STEP];
        /// <summary>
        /// 표준중량
        /// </summary>
        public float StandardWeight
        {
            get
            {
                var s = SplitData[_STD_WEIGHT_START_STEP];
                var standard = 0.0f;
                if (float.TryParse(s, out standard))
                    return standard;
                else
                    return 0.0f;
            }
        }
    }
}
