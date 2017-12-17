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
    /// Interaction logic for AutosTab.xaml
    /// </summary>
    public partial class AutosTab : UserControl
    {

        public AutosViewModel ViewModel { get; private set; }

        public AutosTab()
        {
            InitializeComponent();

            ViewModel = new AutosViewModel();
            ViewModel.OnRequestCreate += (caller, arg) => { (new Views.Auto()).ShowDialog(); ViewModel.RefreshCommand?.Execute(null); };
            ViewModel.OnRequestEdit += (caller, id) => { (new Views.Auto(id)).ShowDialog(); ViewModel.RefreshCommand?.Execute(null); };
            ViewModel.OnRequestDelete += (caller, action) =>
            {
                var messageBoxResult = System.Windows.MessageBox.Show((string)Application.Current.TryFindResource("message_delete_confirm_message_auto"), ((string)Application.Current.TryFindResource("message_delete_confirm_title")), MessageBoxButton.YesNo);
                action?.Invoke(this, messageBoxResult == MessageBoxResult.Yes);
            };
            ViewModel.OnRequestDeleteFailed += (caller, arg) => MessageBox.Show((string)Application.Current.TryFindResource("message_error_delete_auto_message"), (string)Application.Current.TryFindResource("message_error_delete_auto_title"), MessageBoxButton.OK, MessageBoxImage.Error);

            DataContext = this;
        }
    }
}
