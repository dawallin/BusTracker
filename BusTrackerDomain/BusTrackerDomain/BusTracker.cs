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

        public IEnumerable<DepartureInfo> GetStationInfo(int stationId)
        {
            return serviceAgent.GetStationInfo(stationId);
        }
    }
}
