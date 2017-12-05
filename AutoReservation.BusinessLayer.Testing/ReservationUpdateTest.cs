using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationUpdateTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        private int STANDARD_RESERVATION_ID = 1;
        private int NOT_EXISTING_RESERVATION_ID = 99;

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void FindReservationTest()
        {
            Reservation reservation = Target.Find(STANDARD_RESERVATION_ID);
            Assert.AreEqual(STANDARD_RESERVATION_ID, reservation.ReservationsNr);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.Find(STANDARD_RESERVATION_ID);
            DateTime newBisDate = reservation.Bis.AddDays(1);
            reservation.Bis = newBisDate;
            Target.Update(reservation);
            Reservation updatedReservation = Target.Find(STANDARD_RESERVATION_ID);
            Assert.AreEqual(newBisDate, updatedReservation.Bis);
        }

        //[TestMethod]
        // TODO Fix test
        public void UpdateNonExistingReservation()
        {
            Target.Update(new Reservation() { ReservationsNr = NOT_EXISTING_RESERVATION_ID, Von = DateTime.Now, Bis = DateTime.Now.AddDays(1) });
            Reservation updatedReservation = Target.Find(NOT_EXISTING_RESERVATION_ID);
            Assert.AreEqual(null, updatedReservation);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.OptimisticConcurrencyException<Reservation>))]
        public void UpdateAutoOptimisticConcurrencyTest()
        {
            Reservation r1 = Target.Find(STANDARD_RESERVATION_ID);
            Reservation r2 = Target.Find(STANDARD_RESERVATION_ID);

            r1.Bis = r1.Bis.AddDays(1);
            r2.Bis = r2.Bis.AddDays(1);

            Target.Update(r1);
            Target.Update(r2);
        }

    }
}
