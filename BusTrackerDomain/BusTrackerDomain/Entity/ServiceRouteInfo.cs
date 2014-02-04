using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrackerDomain.Entity
{
    public class ServiceRouteInfo
    {
        public Service Service { get; set; }

        public List<Station> Stations { get; set; } 
    }
}
