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
                    ServiceStationInfo = new List<ServiceStationInfo>()
                };

            foreach (var station in route.Stations)
            {
                var stationInfo = this.serviceAgent.GetStationInfo(station.Id);

                if (stationInfo != null)
                {
                    List<Departure> departures;

                    if (stationInfo.StationServiceInfo != null)
                    {
                        departures = stationInfo.StationServiceInfo.Where(s => s.Service != null && s.Service.ServiceId == serviceId)
                                                .Select(s => s.Departure)
                                                .ToList();
                    }
                    else
                    {
                        departures = new List<Departure> {};   
                    }

                    serviceInfo.ServiceStationInfo.Add(new ServiceStationInfo()
                        {
                            Station = stationInfo.Station,
                            NextTo = departures.FirstOrDefault(d => d.Direction == 0),
                            NextReturn = departures.FirstOrDefault(d => d.Direction == 1)
                        });
                }
            }

            return serviceInfo;
        }
    }
}
