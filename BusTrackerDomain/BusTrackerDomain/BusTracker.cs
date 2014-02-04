using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;

namespace BusTrackerDomain
{
    public class BusTracker
    {
        private readonly IServiceAgent serviceAgent;

        public BusTracker(IServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;
        }

        public StationInfo GetStationInfo(int stationId)
        {
            return serviceAgent.GetStationInfo(stationId);
        }

        public IEnumerable<Service> GetServices()
        {
            return serviceAgent.GetServices();
        }

        public ServiceInfo GetServiceInfo(int serviceId)
        {
            return new ServiceInfoCalculator(this.serviceAgent).GetServiceInfo(serviceId);
        }
    }
}
