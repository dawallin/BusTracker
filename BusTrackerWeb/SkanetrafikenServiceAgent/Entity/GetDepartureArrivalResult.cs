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
    [XmlRoot("GetDepartureArrivalResult", Namespace = "http://www.etis.fskab.se/v1.0/ETISws")]
    public class GetDepartureArrivalResult : IProxyQueryResult
    {
        [XmlArray("Lines")]
        public List<Line> Lines;

        public class Line
        {
            public string Name;
            public int No;

            public DateTime JourneyDateTime;
            public TimeSpan TimeToDeparture
            {
                get { return JourneyDateTime - DateTime.Now; }
            }

            public RealTime RealTime;

            private string deviations;

            [XmlAnyElement]
            public XmlElement[] DeviationsNodes { get; set; }

            [XmlIgnore]
            public string Deviations
            {
                get
                {
                    if (this.deviations == null && this.DeviationsNodes != null)
                    {
                        var sb = new StringBuilder();

                        foreach (var node in this.DeviationsNodes)
                        {
                            if (node.Name == "Deviations" && node.InnerXml != string.Empty)
                            {
                                sb.Append(node.OuterXml);
                            }
                        }

                        this.deviations = sb.ToString();
                    }

                    return this.deviations;
                }
            }

            public int RunNo;
        }

        public class RealTime
        {
            public RealTimeInfo RealTimeInfo;
        }

        public class RealTimeInfo
        {
            public int DepTimeDeviation;

            public string DepDeviationAffect;
        }

        public List<DepartureInfo> ToDepartureInfo(int station)
        {
            return this.Lines.Select(line => new DepartureInfo()
            {
                Delay = TimeSpan.FromMinutes(line.RealTime != null && line.RealTime.RealTimeInfo != null ? line.RealTime.RealTimeInfo.DepTimeDeviation : 0),
                DepartureTime = line.JourneyDateTime,
                Direction = line.RunNo % 2,
                Note = (line.RealTime != null && line.RealTime.RealTimeInfo != null && line.RealTime.RealTimeInfo.DepDeviationAffect != null ? line.RealTime.RealTimeInfo.DepDeviationAffect : "") + ", " + line.Deviations,
                RouteId = line.RunNo,
                ServiceId = line.No,
                StationId = station
            }).ToList();
        }
    }
}
