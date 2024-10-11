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
        /// <summary>
        /// 연결 여부
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// 연결 오픈
        /// </summary>
        void Open();
        /// <summary>
        /// 데이터 읽기
        /// </summary>
        /// <returns></returns>
        string ReadExisting();
        /// <summary>
        /// 데이터 읽기
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int Read(byte[] buffer, int offset, int count);
    }
}
