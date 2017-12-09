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
    /// Interaction logic for Kunde.xaml
    /// </summary>
    public partial class Kunde : Window
    {
        public KundeViewModel ViewModel { get; set; }

        public Kunde(int kundeId = -1)
        {
            InitializeComponent();

            ViewModel = new KundeViewModel(kundeId);
            ViewModel.OnRequestClose += (s, e) => this.Close();
            DataContext = this;
        }
    }
}
