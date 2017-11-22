using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class KundeUpdateTest
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void FindKundeTest()
        {
            Kunde k = Target.Find(1);
            Assert.AreEqual(k.Id, 1);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            string newNachname = "updated";
            Kunde kunde = Target.Find(1);
            kunde.Nachname = newNachname;
            Target.Update(kunde);
            Kunde kundeUpdated = Target.Find(1);
            Assert.AreEqual(kundeUpdated.Nachname, newNachname);
        }

        [TestMethod]
        public void UpdateKundeNotFoundTest()
        {
            int id = 982873;
            Target.Update(new Kunde (){ Id = id });
            Kunde kundeUpdated = Target.Find(id);
            Assert.AreEqual(kundeUpdated, null);
        }

        [TestMethod]
        public void UpdateKundeOptimisticConcurrencyTest()
        {
            string newNachname = "updated";

            Kunde k = Target.Find(1);
            Kunde k2 = Target.Find(1);
            k.Nachname = newNachname;
            k2.Nachname = newNachname+newNachname;

            Assert.AreNotEqual(k.Nachname, k2.Nachname);

            Target.Update(k);

            try
            {
                Target.Update(k2);
                Assert.Fail();
            } catch (Exceptions.OptimisticConcurrencyException<Kunde>)
            {
            }

        }
    }
}
