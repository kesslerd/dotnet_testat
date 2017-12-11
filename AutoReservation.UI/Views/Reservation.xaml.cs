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
            ViewModel.OnRequestClose += (s, e) => this.Close();

            DataContext = this;
        }
    }
}
