using System.Xml.Linq;
using BusTrackerDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;

namespace BusTrackerWeb.SkanetrafikenServiceAgent.Entity
{
    [XmlRoot("GetJourneyResult", Namespace = "http://www.etis.fskab.se/v1.0/ETISws")]
    public class GetJourneyResult : IProxyQueryResult
    {
        [XmlArray("Journeys")]
        public List<Journey> Journeys;

        public class Journey
        {
            public int NoOfChanges;

            [XmlArray("RouteLinks")]
            public List<RouteLink> RouteLinks;

            public LineInfo Line { get; set; }

            public class RouteLink
            {
                public string RouteLinkKey;
            }

            public class LineInfo
            {
                public string No { get; set; }
            }
        }
    }
}
