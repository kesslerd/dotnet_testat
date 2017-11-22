namespace AutoReservation.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly AutoViewModel autoViewModel;
        private readonly KundeViewModel kundeViewModel;
        private readonly ReservationViewModel reservationViewModel;

        public MainWindowViewModel(AutoViewModel autoViewModel, KundeViewModel kundeViewModel,
            ReservationViewModel reservationViewModel)
        {
            this.autoViewModel = autoViewModel;
            this.kundeViewModel = kundeViewModel;
            this.reservationViewModel = reservationViewModel;
        }

        public void Init()
        {
            autoViewModel.Init();
            kundeViewModel.Init();
            reservationViewModel.Init();
        }

        public AutoViewModel AutoViewModel
        {
            get { return autoViewModel; }
        }

        public KundeViewModel KundeViewModel
        {
            get { return kundeViewModel; }
        }

        public ReservationViewModel ReservationViewModel
        {
            get { return reservationViewModel; }
        }
    }
}
