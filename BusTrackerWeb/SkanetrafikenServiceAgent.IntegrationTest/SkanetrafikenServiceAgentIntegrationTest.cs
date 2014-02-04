using System;
using System.Linq;
using BusTrackerDomain.Entity;
using BusTrackerWeb.SkanetrafikenServiceAgent.Interface;
using NSubstitute;
using NUnit.Framework;
using BusTrackerWeb.SkanetrafikenServiceAgent;

namespace SkanetrafikenServiceAgent.IntegrationTest
{
    [TestFixture]
    public class SkanetrafikenServiceAgentIntegrationTest
    {
        private static ServiceRouteInfo serviceRouteCached = null;

        private static ServiceRouteInfo ServiceRouteResult
        {
            get
            {
                if (serviceRouteCached != null)
                {
                    return serviceRouteCached;
                }

                var serviceProxy = Substitute.For<IServiceProxy>();
                ServiceAgent agent = new ServiceAgent(serviceProxy);
                serviceRouteCached = agent.GetServiceRoute(133);

                return serviceRouteCached;
            }
        }

        [Test]
        public void CallAndParseLineResult_Given133AsRouteKey_ServiceRouteStartsInLomma()
        {
            Assert.AreEqual("Lomma busstn", ServiceRouteResult.Stations.First().Name);
        }

        [Test]
        public void CallAndParseLineResult_Given133AsRouteKey_ServiceRouteEndsAtSodervarn()
        {
            Assert.AreEqual("Malmö Södervärn", ServiceRouteResult.Stations.Last().Name);
        }

        [Test]
        public void CallAndParseLineResult_Given133AsRouteKey_ServiceRouteContains15Stops()
        {
            Assert.AreEqual(13, ServiceRouteResult.Stations.Count());
        }
    }
}
