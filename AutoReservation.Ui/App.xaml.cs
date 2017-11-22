using AutoReservation.Ui.ViewModels;
using Ninject;
using System.Windows;

namespace AutoReservation.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            kernel = LoadNinject();

            var viewModel = kernel.Get<MainWindowViewModel>();
            viewModel.Init();

            MainWindow = new MainWindow(viewModel);
            MainWindow.Show();
        }

        private IKernel LoadNinject()
        {
            var kernel = new StandardKernel(new AutoReservationModule());
            kernel.Load("AutoReservation.Ui.Factory.NinjectBindings.xml");
            return kernel;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            kernel.Dispose();
            base.OnExit(e);
        }
    }
}
