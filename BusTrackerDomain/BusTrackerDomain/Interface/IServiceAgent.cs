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
        IEnumerable<DepartureInfo> GetStationInfo(int exampleStationId);
    }
}
