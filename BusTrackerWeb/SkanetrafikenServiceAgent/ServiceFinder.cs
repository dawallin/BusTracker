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
            serviceData.Add(new Service() { ServiceId = 133, ServiceName = "133" }, "13852135632032101776600050800010009");
            serviceData.Add(new Service() { ServiceId = 139, ServiceName = "139" }, "13852135632032101826900055000010014");
            serviceData.Add(new Service() { ServiceId = 4, ServiceName = "4" }, "13852135632032100219000010900100014");
            serviceData.Add(new Service() { ServiceId = 3, ServiceName = "3" }, "13852135632032100155200006100060009");
            serviceData.Add(new Service() { ServiceId = 8, ServiceName = "8" }, "13852135632032100486100025000210024");
            
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
