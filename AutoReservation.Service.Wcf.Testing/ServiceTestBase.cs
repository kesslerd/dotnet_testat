using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
//using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            Assert.IsTrue(Target.GetAutos().Any());
        }

        [TestMethod]
        public void GetKundenTest()
        {
            Assert.IsTrue(Target.GetKunden().Any());
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            Assert.IsTrue(Target.GetReservations().Any());
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            int id = 1;
            var auto = Target.GetAuto(id);
            Assert.AreEqual(id, auto.Id);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            int id = 1;
            var kunde = Target.GetKunde(id);
            Assert.AreEqual(id, kunde.Id);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            int reservationsNummer = 1;
            var reservation = Target.GetReservation(reservationsNummer);
            Assert.AreEqual(reservationsNummer, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetAuto(9999));
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetKunde(9999));
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetReservation(9999));
        }

        #endregion

        #region Insert

        // TODO: Insert funktionert nochnicht so wie gewollt
        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto
            {
                Id = 4,
                Marke = "Audi",
                Tagestarif = 120,
                AutoKlasse = AutoKlasse.Mittelklasse,

            };

            Target.AddAuto(auto);

            var foundAuto = Target.GetAuto(auto.Id);

            Assert.AreEqual(auto.Id, foundAuto.Id);
        }

        // Wichtig: ID kann nicht so gesetzt werden, wird fix von System gesetzt
        [TestMethod]
        public void InsertKundeTest()
        {
            var kundeId = 5;
            KundeDto kunde = new KundeDto
            {
                Id = kundeId,
                Nachname = "Bächli",
                Vorname = "Patrick Silvio",
                Geburtsdatum = new DateTime(1994, 06, 21)
            };

            Target.AddKunde(kunde);

            var foundKunde = Target.GetKunde(kunde.Id);

            Assert.AreEqual(kunde.Id, foundKunde.Id);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                ReservationsNr = 4,
                Von = new DateTime(2018, 01, 01),
                Bis = new DateTime(2018, 01, 02),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };

            Target.AddReservation(reservation);

            var foundReservation = Target.GetReservation(reservation.ReservationsNr);

            Assert.AreEqual(reservation.ReservationsNr, foundReservation.ReservationsNr);
        }

        #endregion

        #region Delete  

        // TODO: Delete könnte nur mit ID gemacht werden
        [TestMethod]
        public void DeleteAutoTest()
        {
            int id = 1;
            Target.DeleteAuto(Target.GetAuto(id));
            Assert.IsNull(Target.GetAuto(id));
        }


        // TODO: Delete könnte nur mit ID gemacht werden
        [TestMethod]
        public void DeleteKundeTest()
        {
            int id = 1;
            Target.DeleteKunde(Target.GetKunde(id));
            Assert.IsNull(Target.GetKunde(id));
        }

        // TODO: Delete könnte nur mit ID gemacht werden
        [TestMethod]
        public void DeleteReservationTest()
        {
            int id = 1;
            var reservation = Target.GetReservation(id);
            Target.DeleteReservation(reservation);
            Assert.IsNull(Target.GetReservation(id));
        }

        #endregion

        #region Update
        
        // TODO: Update könnte neues Objekt zurückgeben
        [TestMethod]
        public void UpdateAutoTest()
        {
            int id = 1;
            string marke = "BMW M4";
            var auto = Target.GetAuto(id);
            auto.Marke = marke;
            Target.UpdateAuto(auto);
            var updatedAuto = Target.GetAuto(id);
            Assert.AreEqual(updatedAuto.Marke, marke);

        }

        // TODO: Update könnte neues Objekt zurückgeben
        [TestMethod]
        public void UpdateKundeTest()
        {
            int id = 1;
            string nachname = "Schnitzel";
            var kunde = Target.GetKunde(id);
            kunde.Nachname = nachname;
            Target.UpdateKunde(kunde);
            var updatedKunde = Target.GetKunde(id);
            Assert.AreEqual(updatedKunde.Nachname, nachname);
        }

        // TODO: Update könnte neues Objekt zurückgeben
        [TestMethod]
        public void UpdateReservationTest()
        {
            int reservationsNummer = 1;
            DateTime newBis = new DateTime(2018, 12, 31);
            var reservation = Target.GetReservation(reservationsNummer);
            reservation.Bis = newBis;
            Target.UpdateReservation(reservation);
            var updatedReservation = Target.GetReservation(reservationsNummer);
            Assert.AreEqual(updatedReservation.Bis, newBis);

        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException<DataManipulationFault>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto a1 = Target.GetAuto(1);
            AutoDto a2 = Target.GetAuto(1);

            a1.Marke = "BMW M4";
            a2.Marke = "Audi RS3";

            Target.UpdateAuto(a1);
            Target.UpdateAuto(a2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<DataManipulationFault>))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto k1 = Target.GetKunde(1);
            KundeDto k2 = Target.GetKunde(1);

            k1.Nachname = "Caesar";
            k2.Nachname = "Schnitel";

            Target.UpdateKunde(k1);
            Target.UpdateKunde(k2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<DataManipulationFault>))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            DateTime dt1 = new DateTime(2020, 01, 21);
            DateTime dt2 = new DateTime(2020, 01, 19);

            ReservationDto r1 = Target.GetReservation(1);
            ReservationDto r2 = Target.GetReservation(1);

            r1.Bis = dt1;
            r2.Bis = dt2;

            Target.UpdateReservation(r1);
            Target.UpdateReservation(r2);
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        [ExpectedException(typeof(FaultException<InvalidDateRangeFault>))]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            DateTime von = new DateTime(2018, 1, 20);
            DateTime bis = new DateTime(2018, 1, 19);

            ReservationDto reservation = new ReservationDto
            {
                ReservationsNr = 200,
                Von = von,
                Bis = bis,
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };

            Target.AddReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoUnavailableFault>))]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            DateTime von = new DateTime(2018, 1, 19);
            DateTime bis = new DateTime(2018, 1, 20);

            ReservationDto reservation1 = new ReservationDto
            {
                ReservationsNr = 200,
                Von = von,
                Bis = bis,
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };

            ReservationDto reservation2 = new ReservationDto
            {
                ReservationsNr = 201,
                Von = von,
                Bis = bis,
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(2)
            };

            Target.AddReservation(reservation1);
            Target.AddReservation(reservation2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<InvalidDateRangeFault>))]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            DateTime von = new DateTime(2018, 1, 20);
            DateTime bis = new DateTime(2018, 1, 19);

            ReservationDto reservation = Target.GetReservation(1);

            reservation.Von = von;
            reservation.Bis = bis;

            Target.UpdateReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoUnavailableFault>))]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            DateTime von = new DateTime(2018, 1, 19);
            DateTime bis = new DateTime(2018, 1, 20);

            ReservationDto reservation1 = Target.GetReservation(1);
            ReservationDto reservation2 = Target.GetReservation(2);

            reservation1.Von = von;
            reservation1.Bis = bis;
            reservation1.Auto = Target.GetAuto(1);

            reservation2.Von = von;
            reservation2.Bis = bis;
            reservation2.Auto = Target.GetAuto(1);

            Target.UpdateReservation(reservation1);
            Target.UpdateReservation(reservation2);
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            AutoDto auto = Target.GetAuto(1);
            DateTime von = new DateTime(2021, 01, 01);
            DateTime bis = new DateTime(2021, 01, 10);

            Assert.IsTrue(Target.IsAutoAvailable(auto, von, bis));
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            AutoDto auto = Target.GetAuto(1);
            DateTime von = new DateTime(2020, 01, 10);
            DateTime bis = new DateTime(2020, 01, 20);

            Assert.IsFalse(Target.IsAutoAvailable(auto, von, bis));
        }

        #endregion
    }
}
