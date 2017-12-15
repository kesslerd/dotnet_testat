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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for Auto.xaml
    /// </summary>
    public partial class Auto : Window
    {
        public AutoViewModel ViewModel { get; private set; }

        public Auto(int autoId = -1)
        {
            InitializeComponent();

            ViewModel = new AutoViewModel(autoId);
            ViewModel.OnRequestClose += (s, e) => this.Close();
            ViewModel.OnSaveError += (s, e) => MessageBox.Show((string)Application.Current.TryFindResource("message_error_save_auto_message"), (string)Application.Current.TryFindResource("message_error_save_auto_title"), MessageBoxButton.OK, MessageBoxImage.Error);

            DataContext = this;
        }
    }
}
