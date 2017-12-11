using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private ReservationDto reservationDto = new ReservationDto();

        public ReservationViewModel(int reservationsNr = -1)
        {
            if (reservationsNr != -1)
            {
                this.ReservationsNr = reservationsNr;
                ReloadCommand?.Execute(null);
            }
        }

        public int ReservationsNr
        {
            get { return reservationDto.ReservationsNr; }
            set { reservationDto.ReservationsNr = value; OnPropertyChanged(nameof(ReservationsNr)); }
        }

        public DateTime Von
        {
            get { return reservationDto.Von; }
            set { reservationDto.Von = value; OnPropertyChanged(nameof(Von)); }
        }

        public DateTime Bis
        {
            get { return reservationDto.Bis; }
            set { reservationDto.Bis = value; OnPropertyChanged(nameof(Bis)); }
        }

        public byte[] RowVersion
        {
            get { return reservationDto.RowVersion; }
            set { reservationDto.RowVersion = value; OnPropertyChanged(nameof(RowVersion)); }
        }

        public event EventHandler OnRequestClose;

        #region commands

        RelayCommand<object> _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ?? (_saveCommand = new RelayCommand<object>(param => this.executeSaveCommand()));
        }

        private void executeSaveCommand()
        {
            try
            {
                if (RowVersion != null)
                {
                    AutoReservationService.UpdateReservation(this.reservationDto);
                }
            }
            catch(FaultException<DataManipulationFault> e)
            {

            }
        }

        RelayCommand<object> _cancelCommand;
        public ICommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(param => this.executeCancelCommand()));
        }

        private void executeCancelCommand()
        {
            OnRequestClose?.Invoke(this, null);
        }

        RelayCommand<object> _reloadCommand;
        public ICommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new RelayCommand<object>(param => this.executeReloadCommand(), param => canExecuteReloadCommand()));
        }

        private void executeReloadCommand()
        {
            this.reservationDto = AutoReservationService.GetReservation(this.ReservationsNr);
            OnPropertyChanged(nameof(ReservationsNr));
            OnPropertyChanged(nameof(Von));
            OnPropertyChanged(nameof(Bis));
            OnPropertyChanged(nameof(RowVersion));
        }

        private bool canExecuteReloadCommand()
        {
            return RowVersion != null;
        }

        #endregion
    }
}
