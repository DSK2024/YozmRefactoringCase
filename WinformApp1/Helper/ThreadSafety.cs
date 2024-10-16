using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.Helper
{
    /// <summary>
    /// 스레드 안전 처리 클래스
    /// </summary>
    internal class ThreadSafety
    {
        /// <summary>
        /// 컨트롤 텍스트 셋
        /// </summary>
        /// <param name="control">적용할 컨트롤</param>
        /// <param name="text">적용할 텍스트</param>
        public static void TextSet(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => { control.Text = text; }));
            }
            else
            {
                control.Text = text;
            }
        }
    }
}
