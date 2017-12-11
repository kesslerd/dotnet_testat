using AutoReservation.UI.ViewModels;
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

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for ReservationTab.xaml
    /// </summary>
    public partial class ReservationenTab : UserControl
    {
        public ReservationenViewModel ViewModel { get; private set; }

        public ReservationenTab()
        {
            InitializeComponent();

            ViewModel = new ReservationenViewModel();
            ViewModel.OnRequestCreate += (caller, arg) => { (new Views.Reservation()).ShowDialog(); };
            ViewModel.OnRequestEdit += (caller, reservationsNr) => { (new Views.Reservation(reservationsNr)).ShowDialog(); };
            ViewModel.OnRequestDelete += (caller, action) => {
                var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                action?.Invoke(this, messageBoxResult == MessageBoxResult.Yes);
            };

            DataContext = this;
        }
    }
}
