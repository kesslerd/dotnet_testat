using AutoReservation.Ui.ViewModels;
using Ninject.Modules;

namespace AutoReservation.Ui
{
    public class AutoReservationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel>().ToSelf();
            Bind<AutoViewModel>().ToSelf();
            Bind<KundeViewModel>().ToSelf();
            Bind<ReservationViewModel>().ToSelf();
        }
    }
}
