using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusTrackerDomain.Test
{
    [TestFixture]
    public class BusTrackerTest
    {
        [Test]
        public void GetStationInfo_ForValidStationId_ReturnsStationTimeTable()
        {
            const int exampleStationId = 1;
            var exampleStationTimeTable = new List<DepartureInfo> { };

            var serviceAgent = Substitute.For<IServiceAgent>();

            serviceAgent.GetStationInfo(exampleStationId).Returns(exampleStationTimeTable);

            var busTracker = new BusTracker(serviceAgent);

            Assert.AreEqual(exampleStationTimeTable, busTracker.GetStationInfo(exampleStationId));
        }
    }
}
