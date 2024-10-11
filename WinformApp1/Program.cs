using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1
{
    internal static class Program
    {
        static public Action<string> statusMessageShow;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var f = new ReceiveInspection();
            Program.statusMessageShow = f.StatusMessageShow;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(f);
        }
    }
}
