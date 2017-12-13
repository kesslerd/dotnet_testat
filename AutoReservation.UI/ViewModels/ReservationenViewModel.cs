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

namespace AutoReservation.UI.ViewModels
{
    public class ReservationenViewModel : BaseViewModel
    {
        public ReservationenViewModel()
        {
            ExecuteRefreshCommand();
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

        public event EventHandler<int> OnRequestEdit;
        public event EventHandler<object> OnRequestCreate;
        public event EventHandler<EventHandler<bool>> OnRequestDelete;

        #region commands

        RelayCommand<object> _refreshCommand;
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(param => this.ExecuteRefreshCommand()));
        }

        private void ExecuteRefreshCommand()
        {
            Reservationen = AutoReservationService.GetReservations();
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
                RefreshCommand?.Execute(null);
            }
            catch (FaultException<DataManipulationFault> e)
            {

            }
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
    }
}
