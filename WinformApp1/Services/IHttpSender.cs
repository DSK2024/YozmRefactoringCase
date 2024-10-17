using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformApp1.Services
{
    public interface IHttpSender
    {
        bool Send(string path, Models.InspectResult result);
    }
}
