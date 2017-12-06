﻿using AutoReservation.UI.ViewModels;
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
    /// Interaction logic for KundenTab.xaml
    /// </summary>
    public partial class KundenTab : UserControl
    {

        public KundenViewModel ViewModel { get; private set; }

        public KundenTab()
        {
            InitializeComponent();
            ViewModel = new KundenViewModel();
            ViewModel.OnRequestCreateKunde += (caller, arg) => { (new Views.Kunde()).ShowDialog(); ViewModel.RefreshCommand?.Execute(null); };
            ViewModel.OnRequestEditKunde += (caller, id) => { (new Views.Kunde(id)).ShowDialog(); ViewModel.RefreshCommand?.Execute(null); };
            ViewModel.OnRequestDelete += (caller, action) => {
                var messageBoxResult = System.Windows.MessageBox.Show((string)Application.Current.FindResource("message_delete_confirm_title"), (string)Application.Current.FindResource("message_delete_confirm_message"), MessageBoxButton.YesNo);
                action?.Invoke(this, messageBoxResult == MessageBoxResult.Yes);
            };
            DataContext = this;
        }
    }
}
