using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerDomain.Entity;

namespace BusTrackerDomain.Interface
{
    public interface IServiceAgent
    {
        //Journey GetJourney(int serviceId, int fromStationId, int toStationId, DateTime time);

        StationInfo GetStationInfo(int stationId);

        ServiceRouteInfo GetServiceRoute(int serviceId);

        List<Service> GetServices();
    }
}
