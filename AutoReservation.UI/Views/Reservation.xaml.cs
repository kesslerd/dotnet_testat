using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoReservation.UI.ViewModels;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        public ReservationViewModel ViewModel { get; set; }

        public Reservation(int reservationNr = -1)
        {
            InitializeComponent();

            ViewModel = new ReservationViewModel(reservationNr);
            ViewModel.OnRequestSave += (reservation, action) =>
            {
                var selectedKunde = KundeInput.SelectedItem as KundeDto;
                var selectedAuto = AutoInput.SelectedItem as AutoDto;

                reservation = reservation as ReservationDto;
                ((ReservationDto)reservation).Kunde = selectedKunde;
                ((ReservationDto)reservation).Auto = selectedAuto;

                action?.Invoke(this, true);
            };
            ViewModel.OnRequestClose += (s, e) => this.Close();
            ViewModel.OnSaveError += (s, e) => MessageBox.Show((string)Application.Current.TryFindResource("message_error_save_reservation_message"), (string)Application.Current.TryFindResource("message_error_save_reservation_title"), MessageBoxButton.OK, MessageBoxImage.Error);
            ViewModel.OnSaveErrorAutoNotAvailable += (s, e) => MessageBox.Show((string)Application.Current.TryFindResource("message_error_save_reservation_message_auto"), (string)Application.Current.TryFindResource("message_error_save_reservation_title"), MessageBoxButton.OK, MessageBoxImage.Error);
            ViewModel.OnSaveErrorDateRange += (s, e) => MessageBox.Show((string)Application.Current.TryFindResource("message_error_save_reservation_message_date"), (string)Application.Current.TryFindResource("message_error_save_reservation_title"), MessageBoxButton.OK, MessageBoxImage.Error);
            DataContext = this;
        }
    }
}
