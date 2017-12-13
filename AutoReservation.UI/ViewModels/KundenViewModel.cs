using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;

namespace AutoReservation.UI.ViewModels
{
    public class KundenViewModel : BaseViewModel
    {
        public KundenViewModel()
        {
            executeRefreshCommand();
        }

        private List<KundeDto> _kunden;
        public List<KundeDto> Kunden
        {
            get
            {
                return _kunden ?? (_kunden = new List<KundeDto>());
            }
            private set
            {
                _kunden = value;
                OnPropertyChanged(nameof(Kunden));
            }
        }

        public event EventHandler<int> OnRequestEditKunde;
        public event EventHandler<object> OnRequestCreateKunde;
        public event EventHandler<EventHandler<bool>> OnRequestDelete;
        public event EventHandler<object> OnDeleteKundeFailed;


        #region commands
        RelayCommand<object> _refreshCommand;
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(param => this.executeRefreshCommand()));
        }

        private void executeRefreshCommand()
        {
            Kunden = AutoReservationService.GetKunden();
        }

        RelayCommand<object> _addCommand;
        public ICommand AddCommand
        {
            get => _addCommand ?? (_addCommand = new RelayCommand<object>(param => this.executeAddCommand()));
        }

        private void executeAddCommand()
        {
            OnRequestCreateKunde?.Invoke(this, null);
        }

        RelayCommand<KundeDto> _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new RelayCommand<KundeDto>(param => this.executeDeleteCommand(param)));
        }

        private void executeDeleteCommand(KundeDto kunde)
        {
            OnRequestDelete?.Invoke(this, (caller,ok)=> { if (ok) delete(kunde); });
        }

        private void delete(KundeDto kunde)
        {
            try
            {
                AutoReservationService.DeleteKunde(kunde);
                RefreshCommand?.Execute(null);
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnDeleteKundeFailed?.Invoke(this, null);
                RefreshCommand?.Execute(null);
            }
        }

        RelayCommand<int> _editCommand;
        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new RelayCommand<int>(param => this.executeEditCommand(param)));
        }

        private void executeEditCommand(int id)
        {
            OnRequestEditKunde?.Invoke(this, id);
        }

        #endregion
    }
}
