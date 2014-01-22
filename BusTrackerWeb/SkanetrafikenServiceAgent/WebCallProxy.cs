using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;

namespace BusTrackerWeb.SkanetrafikenServiceAgent
{
    public class WebCallProxy : IServiceProxy
    {
        public string Call(string urlString)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString(urlString);
            }
        }
    }
}
