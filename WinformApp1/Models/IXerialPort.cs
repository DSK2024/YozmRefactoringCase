using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.Models
{
    public interface IXerialPort
    {
        void Open();
        string ReadExisting();
        int Read(byte[] buffer, int offset, int count);
    }
}
