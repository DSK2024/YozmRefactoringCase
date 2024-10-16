using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WinformApp1.Models
{
    /// <summary>
    /// 바코드 정보 클래스
    /// </summary>
    /// <example>
    /// var sample = "##HY#K3A08#AB12#7.5#0001";
    /// var data = new BarcodeInfo(sample);
    /// Console.WriteLine("바코드 전체데이터값 : {0}", data.DataText);
    /// Console.WriteLine("바코드 표준중량값 추출 : {0}", data.StandardWeight);
    /// </example>
    public class BarcodeInfo
    {
        const int _COMPANY_START_STEP = 2;
        const int _DATEFORM_START_STEP = 3;
        const int _PART_NO_START_STEP = 4;
        const int _STD_WEIGHT_START_STEP = 5;
        const char _BARCODE_STEP_DELIMITER = '#';//구분자
        const int _ELEMENT_AVAIL_COUNT = 7;//요소의 수
        readonly string _barcodeData;
        public BarcodeInfo(string barcodeData)
        {
            _barcodeData = barcodeData;
        }
        public BarcodeInfo(byte[] buffer)
        {
            _barcodeData = UTF32Encoding.UTF8.GetString(buffer);
        }
        /// <summary>
        /// 바코드데이터 문자값
        /// </summary>
        public string DataText => _barcodeData;
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

        /// <summary>
        /// 유효한 바코드 데이터인지 여부를 반환
        /// </summary>
        /// <returns>유효한 바코드 여부</returns>
        public bool ValidBarcode => SplitData.Length == _ELEMENT_AVAIL_COUNT;
    }
}
