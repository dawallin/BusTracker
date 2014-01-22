using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;
using BusTrackerWeb.SkanetrafikenServiceAgent.Entity;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;

namespace BusTrackerWeb.SkanetrafikenServiceAgent
{
    public class ServiceAgent : IServiceAgent 
    {
        private readonly IServiceProxy serviceProxy;

        public ServiceAgent(IServiceProxy serviceProxy)
        {
            this.serviceProxy = serviceProxy;
        }

        public IEnumerable<DepartureInfo> GetStationInfo(int station)
        {
            string urlString = "http://www.labs.skanetrafiken.se/v2.2/stationresults.asp?selPointFrKey=" + station;

            var result = ApiQuery<GetDepartureArrivalResult>(urlString);

            return result.ToDepartureInfo(station);
        }

        private T ApiQuery<T>(string urlString) where T : IProxyQueryResult
        {
            string xml = this.serviceProxy.Call(urlString);

            XDocument doc = XDocument.Parse(xml);
            doc.Declaration = new XDeclaration("1.0", "utf-8", "true");

            XNamespace ns = "http://www.etis.fskab.se/v1.0/ETISws";
            var resultText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + doc.Descendants(ns + typeof(T).Name).First();

            var result = FromXmlString<T>(resultText);

            return result;
        }

        private static T FromXmlString<T>(string xmlString) where T : IProxyQueryResult
        {
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));
            var a = serializer.Deserialize(reader);
            var instance = (T)a;

            return instance;
        }
    }
}
