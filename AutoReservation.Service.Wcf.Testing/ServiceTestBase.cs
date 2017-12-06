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
            Assert.Inconclusive("Test not implemented.");
            int id = 1;
            var auto = Target.GetAuto(id);
            Assert.Equals(id, auto.Id);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            int id = 1;
            var kunde = Target.GetKunde(id);
            Assert.Equals(id, kunde.Id);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            int reservationsNummer = 1;
            var reservation = Target.GetReservation(reservationsNummer);
            Assert.Equals(reservationsNummer, reservation.ReservationsNr);
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

        [TestMethod]
        public void InsertAutoTest()
        {
            var auto = new AutoDto
            {
                Id = 200,
                Marke = "Audi",
                Tagestarif = 120,
                AutoKlasse = AutoKlasse.Mittelklasse,

            };

            Target.AddAuto(auto);

            var foundAuto = Target.GetAuto(auto.Id);

            Assert.Equals(foundAuto, auto);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var kunde = new KundeDto
            {
                Id = 200,
                Nachname = "Bächli",
                Vorname = "Patrick Silvio",
                Geburtsdatum = new DateTime(1994, 06, 21)
            };

            Target.AddKunde(kunde);

            var foundKunde = Target.GetKunde(kunde.Id);

            Assert.Equals(foundKunde, kunde);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var reservation = new ReservationDto
            {
                ReservationsNr = 200,
                Von = new DateTime(2018, 01, 01),
                Bis = new DateTime(2018, 01, 02),
                Auto = Target.GetAuto(1),
                Kunde = Target.GetKunde(1)
            };

            Target.AddReservation(reservation);

            var foundReservation = Target.GetReservation(reservation.ReservationsNr);

            Assert.Equals(foundReservation, reservation);
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
            Target.DeleteReservation(Target.GetReservation(id));
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
            Assert.Equals(updatedAuto.Marke, marke);

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
            Assert.Equals(updatedKunde.Nachname, nachname);
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
            Assert.Equals(updatedReservation.Bis, newBis);

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
            DateTime dt1 = new DateTime(2018, 3, 12);
            DateTime dt2 = new DateTime(2018, 4, 12);

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
        public void InsertReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
