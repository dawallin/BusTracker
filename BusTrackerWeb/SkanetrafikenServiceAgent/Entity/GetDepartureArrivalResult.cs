﻿using BusTrackerDomain.Entity;
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
        public List<Line> Lines { get; set; }

        public StopAreaDataObject StopAreaData { get; set; }

        public class Line
        {
            public string Name { get; set; }
            public int No { get; set; }

            public DateTime JourneyDateTime { get; set; }
            public TimeSpan TimeToDeparture
            {
                get { return JourneyDateTime - DateTime.Now; }
            }

            public RealTime RealTime { get; set; }

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

            public int RunNo { get; set; }
        }

        public class RealTime
        {
            public RealTimeInfo RealTimeInfo { get; set; }
        }

        public class RealTimeInfo
        {
            public int DepTimeDeviation { get; set; }

            public string DepDeviationAffect { get; set; }
        }

        public class StopAreaDataObject
        {
            public string Name { get; set; }
        }

        public StationInfo ToStationInfo(int station)
        {
            var stationInfo = new StationInfo()
                {
                    Station = new Station
                        {
                            Id = station,
                            Name = this.StopAreaData.Name,
                        },
                    StationServiceInfo = new List<StationServiceInfo>()
                };

            stationInfo.StationServiceInfo = this.Lines.Select(line => new StationServiceInfo()
            {
                Service = new Service
                    {
                        ServiceId = line.No,
                        ServiceName = line.Name
                    },
                Departure = new Departure
                    {
                        RouteId = line.RunNo,
                        Delay = TimeSpan.FromMinutes(line.RealTime != null && line.RealTime.RealTimeInfo != null ? line.RealTime.RealTimeInfo.DepTimeDeviation : 0),
                        DepartureTime = line.JourneyDateTime,
                        Direction = line.RunNo % 2,
                        Note = DepDeviationAffect(line) + ", " + line.Deviations,
                        Passed = DepDeviationAffect(line)  == "PASSED"
                    }
            }).ToList();
            return stationInfo;
        }

        private string DepDeviationAffect(Line line)
        {
            return (line.RealTime != null && line.RealTime.RealTimeInfo != null && line.RealTime.RealTimeInfo.DepDeviationAffect != null) ? line.RealTime.RealTimeInfo.DepDeviationAffect : string.Empty;
        }
    }
}
