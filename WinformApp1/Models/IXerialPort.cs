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
        /// XerialPort 개체의 열림 또는 닫힘 상태를 나타내는 값을 가져옵니다.
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// 수신 버퍼에 있는 데이터 바이트 수
        /// </summary>
        int BytesToRead { get; }
        /// <summary>
        /// 새 직렬포트를 연결한다
        /// </summary>
        void Open();
        /// <summary>
        /// 인코딩 기준으로 입력버퍼에서 모든 바이트를 읽는다.
        /// </summary>
        /// <returns>입력버퍼</returns>
        string ReadExisting();
        /// <summary>
        /// XerialPort 입력 버퍼에서 여러 바이트를 읽어온다
        /// </summary>
        /// <param name="buffer">입력내용을 쓸 바이트 배열</param>
        /// <param name="offset">바이트를 쓸 버퍼의 오프셋</param>
        /// <param name="count">읽을 최대 바이트 수</param>
        /// <returns>읽은 바이트 수</returns>
        int Read(byte[] buffer, int offset, int count);
    }
}
