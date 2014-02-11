using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;

namespace BusTrackerDomain
{
    public class ServiceInfoCalculator
    {
        private readonly IServiceAgent serviceAgent;

        public ServiceInfoCalculator(IServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;            
        }

        public ServiceInfo GetServiceInfo(int serviceId)
        {
            var route = this.serviceAgent.GetServiceRoute(serviceId);

            ServiceInfo serviceInfo = new ServiceInfo()
                {
                    Service = route.Service,
                    ServiceStationInfos = new List<ServiceStationInfo>()
                };

            foreach (var station in route.Stations)
            {
                var stationInfo = this.serviceAgent.GetStationInfo(station.Id);

                if (stationInfo != null)
                {
                    List<Departure> departures;

                    if (stationInfo.StationServiceInfo != null)
                    {
                        departures = stationInfo.StationServiceInfo.Where(s => s.Service != null && s.Service.ServiceId == serviceId && !s.Departure.Passed)
                                                .Select(s => s.Departure)
                                                .ToList();
                    }
                    else
                    {
                        departures = new List<Departure> {};   
                    }

                    serviceInfo.ServiceStationInfos.Add(new ServiceStationInfo()
                        {
                            Station = stationInfo.Station,
                            NextTo = departures.FirstOrDefault(d => d.Direction == 0),
                            NextReturn = departures.FirstOrDefault(d => d.Direction == 1)
                        });
                }
            }

            serviceInfo = CalculateJourneyInfo(serviceInfo);

            return serviceInfo;
        }

        private ServiceInfo CalculateJourneyInfo(ServiceInfo serviceInfo)
        {
            var toJourneys = serviceInfo.ServiceStationInfos.Where(s => s.NextTo != null).GroupBy(s => s.NextTo.RouteId).Select(group => group.Last());
            var returnJourneys = serviceInfo.ServiceStationInfos.Where(s => s.NextReturn != null).GroupBy(s => s.NextReturn.RouteId).Select(group => group.First());

            serviceInfo.JourneyInfos = new List<JourneyInfo>();

            foreach (var journey in toJourneys)
            {
                serviceInfo.JourneyInfos.Add(new JourneyInfo()
                    {
                        Journey = new Journey()
                            {
                                Direction = journey.NextTo.Direction, 
                                RouteId = journey.NextTo.RouteId, 
                                Service = serviceInfo.Service
                            },
                        NextStation = journey.Station
                    });    
            }

            foreach (var journey in returnJourneys)
            {
                serviceInfo.JourneyInfos.Add(new JourneyInfo()
                {
                    Journey = new Journey()
                    {
                        Direction = journey.NextReturn.Direction,
                        RouteId = journey.NextReturn.RouteId,
                        Service = serviceInfo.Service
                    },
                    NextStation = journey.Station
                });
            }

            return serviceInfo;
        }
    }
}
