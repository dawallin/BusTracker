using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrackerDomain.Entity
{
    public class ServiceInfo
    {
        public Service Service { get; set; }

        public List<ServiceStationInfo> ServiceStationInfos { get; set; }

        public List<JourneyInfo> JourneyInfos { get; set; } 
    }
}
