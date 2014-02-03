using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerDomain.Entity;

namespace BusTrackerWeb.SkanetrafikenServiceAgent
{
    internal class ServiceFinder
    {
        private readonly Dictionary<Service, string> serviceData = new Dictionary<Service, string>();

        public ServiceFinder()
        {
            serviceData.Add(new Service() { ServiceId = 133, ServiceName = "133" }, "17095110733025101771900050800010009");
        }

        public List<Service> GetServices()
        {
            return serviceData.Keys.ToList();
        }

        public string GetRouteKey(int serviceId)
        {
            return serviceData.First(r => r.Key.ServiceId == serviceId).Value;
        }

        public Service GetRouteService(int serviceId)
        {
            return serviceData.First(r => r.Key.ServiceId == serviceId).Key;
        }
    }
}
