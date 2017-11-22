using AutoReservation.TestEnvironment;
using AutoReservation.Ui.Factory;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Windows.Input;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private IKernel kernel;

        [TestInitialize]
        public void InitializeTestData()
        {
            kernel = new StandardKernel();
            kernel.Load("AutoReservation.Ui.Factory.NinjectBindings.xml");

            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void AutosLoadTest()
        {
            AutoViewModel vm = new AutoViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, vm.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            KundeViewModel vm = new KundeViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(4, vm.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            ReservationViewModel vm = new ReservationViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, vm.Reservationen.Count);
        }
    }
}