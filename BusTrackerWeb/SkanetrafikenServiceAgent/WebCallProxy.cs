using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;
using System.Net;

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
