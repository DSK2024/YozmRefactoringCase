using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WinformApp1.Models;
using WinformApp1.UserControls;

namespace WinformApp1.Services
{
    public class InspectSender : IHttpSender
    {
        string _url;
        public bool IsSend;
        public InspectSender(string url)
        {
            IsSend = false;
            _url = url;
        }

        public bool Send(string path, InspectResult result)
        {
            IsSend = false;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(_url);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("barcode", result.BarcodeData),
                    new KeyValuePair<string, string>("model", result.PartNo),
                    new KeyValuePair<string, string>("weight", result.Weight),
                    new KeyValuePair<string, string>("ok_ng", result.Result)
                });
                var response = client.PostAsync(path, content);
                return IsSend = response.IsCompleted;
            }
            catch (Exception ex)
            {
                //통신 실패시 처리로직
                ProgramGlobal.StatusMessageShow(ex.Message);
            }
            return IsSend = false;
        }
    }
}
