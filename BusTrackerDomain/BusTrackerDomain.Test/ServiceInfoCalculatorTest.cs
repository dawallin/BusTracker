using System;
using BusTrackerDomain.Entity;
using BusTrackerDomain.Interface;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BusTrackerDomain.Test
{
    [TestFixture]
    public class ServiceInfoCalculatorTest
    {
        [Test]
        public void GetServiceInfo_ForValidServiceId_ReturnsAllStations()
        {
            const int exampleServiceId = 1;
            
            Station station1 = new Station {Id = 1};
            Station station2 = new Station {Id = 2};
            Station station3 = new Station {Id = 3};

            var exampleServiceRoute = new ServiceRouteInfo()
                {
                    Service = new Service()
                        {
                            ServiceId = exampleServiceId
                        },
                    Stations = new List<Station>
                        {
                            station1, station2, station3
                        }
                };

            var serviceAgent = Substitute.For<IServiceAgent>();

            serviceAgent.GetServiceRoute(exampleServiceId).Returns(exampleServiceRoute);
            serviceAgent.GetStationInfo(station1.Id).Returns(new StationInfo {Station = station1});
            serviceAgent.GetStationInfo(station2.Id).Returns(new StationInfo {Station = station2});
            serviceAgent.GetStationInfo(station3.Id).Returns(new StationInfo {Station = station3});

            var busTracker = new BusTracker(serviceAgent);

            Assert.AreEqual(exampleServiceRoute.Stations, busTracker.GetServiceInfo(exampleServiceId).ServiceStationInfo.Select(s => s.Station).ToList());
        }

        [Test]
        public void GetServiceInfo_ForValidServiceId_ReturnsFirstNextToDeparture()
        {
            const int exampleServiceId = 1;

            Service exampleService = new Service()
                {
                    ServiceId = exampleServiceId
                };

            DateTime exampleDeparture1 = DateTime.Parse("2001-01-01 12:00");
            DateTime exampleDeparture2 = DateTime.Parse("2001-01-01 12:01");

            StationInfo exampleStationInfo = new StationInfo
                {
                    Station = new Station {Id = 1},
                    StationServiceInfo = new List<StationServiceInfo>
                        {
                            new StationServiceInfo
                                {
                                    Service = exampleService,
                                    Departure = new Departure()
                                        {
                                            Direction = 0,
                                            DepartureTime = exampleDeparture1
                                        }
                                },
                            new StationServiceInfo
                                {
                                    Service = exampleService,
                                    Departure = new Departure()
                                        {
                                            Direction = 0,
                                            DepartureTime = exampleDeparture2
                                        }
                                }
                        }
                };

            var exampleServiceRoute = new ServiceRouteInfo()
            {
                Service = exampleService,
                Stations = new List<Station>
                        {
                            exampleStationInfo.Station
                        }
            };

            var serviceAgent = Substitute.For<IServiceAgent>();

            serviceAgent.GetServiceRoute(exampleServiceId).Returns(exampleServiceRoute);
            serviceAgent.GetStationInfo(exampleStationInfo.Station.Id).Returns(exampleStationInfo);

            var busTracker = new BusTracker(serviceAgent);

            Assert.AreEqual(exampleStationInfo.StationServiceInfo.FirstOrDefault().Departure.DepartureTime, busTracker.GetServiceInfo(exampleServiceId).ServiceStationInfo.FirstOrDefault(s => s.Station == exampleStationInfo.Station).NextTo.DepartureTime);
        }
    }
}
