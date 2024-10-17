using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    /// <summary>
    /// 검사실적 클래스
    /// </summary>
    public class InspectResult
    {
        public string PartNo { get; set; }
        public string Weight { get; set; }
        public string Result { get; set; }
        public string BarcodeData { get; set; }
        public InspectResult() { }
        public InspectResult(string partNo, string weight, string result, string barcodeData)
        {
            PartNo = partNo;
            Weight = weight;
            Result = result;
            BarcodeData = barcodeData;
        }
    }
}
