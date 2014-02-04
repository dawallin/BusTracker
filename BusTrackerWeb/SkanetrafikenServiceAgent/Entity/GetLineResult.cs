using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusTrackerDomain.Entity;
using HtmlAgilityPack;

namespace BusTrackerWeb.SkanetrafikenServiceAgent
{
    public class GetLineResult
    {
        public ServiceRouteInfo ToServiceRoute(string routeKey)
        {
            string urlString = "http://www.reseplaneraren.skanetrafiken.se/lineResults.aspx?key=" + routeKey;

            string xml;

            using (var webClient = new WebClient())
            {
                xml = webClient.DownloadString(urlString);
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(xml);

            var routeTable = doc.DocumentNode.SelectNodes("//*[@id=\"spot-results-table\"]").First().ChildNodes;

            ServiceRouteInfo serviceRoute = new ServiceRouteInfo { Stations = new List<Station>()};

            foreach (var stationRow in routeTable.Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.StartsWith("spot-row")))
            {
                var stationOverview = stationRow
                    .Descendants()
                    .SingleOrDefault(c =>
                                     c.Attributes.Contains("class")
                                     && c.Attributes["class"].Value == "details-col-3");

                if (stationOverview != null)
                {
                    var stationInfo = stationOverview
                                       .Descendants()
                                       .Single(c => c.Name == "a");
                    var stationData =
                        stationInfo.GetAttributeValue("href", "queryStation('Not Found|0','2001-01-01 00:01',0)")
                                   .Split(new[] {'\'', '|'});
                    var stationId = int.Parse(stationData[2]);
                    var departureTime = DateTime.Parse(stationData[4]);
                    var stationName = stationInfo.InnerText;

                    serviceRoute.Stations.Add(new Station() { Id = stationId, Name = stationName });
                }
            }

            return serviceRoute;
        }
    }
}
