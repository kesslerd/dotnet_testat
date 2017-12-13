using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using static AutoReservation.UI.Service.Service;
using System.Windows.Input;
using System.ServiceModel;
using System.Windows.Threading;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationenViewModel : BaseViewModel
    {
        public ReservationenViewModel()
        {
            ExecuteRefreshCommand();
            StartDispatcher();
        }

        private List<ReservationDto> _reservationen;
        public List<ReservationDto> Reservationen
        {
            get
            {
                return _reservationen ?? (_reservationen = new List<ReservationDto>());
            }
            private set
            {
                _reservationen = value;
                OnPropertyChanged(nameof(Reservationen));
            }
        }

        private bool _includeFinished = true;

        public bool IncludeFinished
        {
            get
            {
                return _includeFinished;
            }
            set
            {
                _includeFinished = value;
                ExecuteRefreshCommand();
                OnPropertyChanged(nameof(IncludeFinished));
            }
        }

        public event EventHandler<int> OnRequestEdit;
        public event EventHandler<object> OnRequestCreate;
        public event EventHandler<EventHandler<bool>> OnRequestDelete;
        public event EventHandler<object> OnRequestDeleteFailed;

        #region commands

        RelayCommand<object> _refreshCommand;
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(param => this.ExecuteRefreshCommand()));
        }

        private void ExecuteRefreshCommand()
        {
            Reservationen = AutoReservationService.GetReservations(IncludeFinished);
        }

        RelayCommand<object> _addCommand;
        public ICommand AddCommand
        {
            get => _addCommand ?? (_addCommand = new RelayCommand<object>(param => this.ExecuteAddCommand()));
        }

        private void ExecuteAddCommand()
        {
            OnRequestCreate?.Invoke(this, null);
        }

        RelayCommand<ReservationDto> _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new RelayCommand<ReservationDto>(param => this.ExecuteDeleteCommand(param)));
        }

        private void ExecuteDeleteCommand(ReservationDto reservation)
        {
            OnRequestDelete?.Invoke(this, (caller, ok) => { if (ok) Delete(reservation); });
        }

        private void Delete(ReservationDto reservation)
        {
            try
            {
                AutoReservationService.DeleteReservation(reservation);
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnRequestDeleteFailed?.Invoke(this, null);
            }

            RefreshCommand.Execute(null);
        }

        RelayCommand<int> _editCommand;
        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new RelayCommand<int>(param => this.ExecuteEditCommand(param)));
        }

        private void ExecuteEditCommand(int id)
        {
            OnRequestEdit?.Invoke(this, id);
        }

        #endregion

        private void StartDispatcher()
        {
            var dispatcher = new DispatcherTimer();
            dispatcher.Tick += (sender, arg) => ExecuteRefreshCommand();
            dispatcher.Interval = new TimeSpan(0, 0, 30);
            dispatcher.Start();
        }
    }
}
