using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class AutoUpdateTests
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        private int STANDARD_AUTO_ID = 1;
        private int NOT_EXISTING_AUTO_ID = 99;


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void FindAutoTest()
        {
            Auto auto = Target.Find(STANDARD_AUTO_ID);
            Assert.AreEqual(auto.Id, STANDARD_AUTO_ID);

        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            String marke = "BMW";
            Auto auto = Target.Find(STANDARD_AUTO_ID);
            auto.Marke = marke;
            Target.Update(auto);
            Auto updatedAuto = Target.Find(STANDARD_AUTO_ID);
            Assert.AreEqual(updatedAuto.Marke, marke);
        }

        [TestMethod]
        public void UpdateNonExistingAuto()
        {
            Target.Update(new Auto() { Id = NOT_EXISTING_AUTO_ID, Marke = "Hunde-Ei", Tagestarif = 42 });
            Auto updatedAuto = Target.Find(NOT_EXISTING_AUTO_ID);
            Assert.AreEqual(updatedAuto, null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.OptimisticConcurrencyException<Auto>))]
        public void UpdateAutoOptimisticConcurrencyTest()
        {
            Auto a1 = Target.Find(STANDARD_AUTO_ID);
            Auto a2 = Target.Find(STANDARD_AUTO_ID);

            a1.Marke = "BMW";
            a2.Marke = "Audi";

            Target.Update(a1);
            Target.Update(a2);
        }
    }
}
