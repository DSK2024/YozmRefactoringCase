using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Models
{
    public class XerialPort : IXerialPort
    {
        public void Open()
        {
            throw new NotImplementedException();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public string ReadExisting()
        {
            throw new NotImplementedException();
        }
    }
}
