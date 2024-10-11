﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.Models
{
    public class XerialPort : IXerialPort
    {
        public bool _isOpen;
        public bool IsOpen => _isOpen;
        public void Open()
        {
            _isOpen = true;
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
