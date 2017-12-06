using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationDateRangeTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioOkay01Test()
        {
            Target.Add(new Reservation() { AutoId = 1, KundeId = 1, Von = new DateTime(2018, 01, 01), Bis = new DateTime(2018, 01, 02) });
        }

        [TestMethod]
        public void ScenarioOkay02Test()
        {
            Target.Add(new Reservation() { AutoId = 1, KundeId = 1, Von = new DateTime(2018, 01, 01, 0, 0, 0), Bis = new DateTime(2018, 01, 10, 23, 59, 59) });
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay01Test()
        {
            Target.Add(new Reservation() { AutoId = 1, KundeId = 1, Von = new DateTime(2018, 01, 02), Bis = new DateTime(2018, 01, 01) });
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay02Test()
        {
            Target.Add(new Reservation() { AutoId = 1, KundeId = 1, Von = new DateTime(2018, 01, 01), Bis = new DateTime(2018, 01, 01) });
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay03Test()
        {
            Assert.Inconclusive("Drittes NotOkay-Szenarion finden");
        }
    }
}
