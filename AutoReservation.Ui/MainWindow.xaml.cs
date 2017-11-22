using System.Windows;
using AutoReservation.Ui.ViewModels;

namespace AutoReservation.Ui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(object viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
