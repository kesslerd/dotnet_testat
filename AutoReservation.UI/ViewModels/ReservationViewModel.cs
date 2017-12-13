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
            Kunden = AutoReservationService.GetKunden();
            Autos = AutoReservationService.GetAutos();

            if (reservationsNr != -1)
            {
                this.ReservationsNr = reservationsNr;
                ReloadCommand?.Execute(null);
            }
        }

        public List<KundeDto> Kunden { get; private set; }
        public List<AutoDto> Autos { get; private set; }

        private KundeDto _selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set { _selectedKunde = value; OnPropertyChanged(nameof(SelectedKunde)); }
        }

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set { _selectedAuto = value; OnPropertyChanged(nameof(SelectedAuto)); }
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
        public event EventHandler<EventHandler<bool>> OnRequestSave;
        public event EventHandler OnSaveError;

        #region commands

        RelayCommand<ReservationDto> _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ?? (_saveCommand = new RelayCommand<ReservationDto>(param => this.ExecuteSaveCommand(reservationDto)));
        }

        private void ExecuteSaveCommand(ReservationDto reservation)
        {
            /* TODO
             *  Hier wird eigentlich der ok-Parameter nicht benötigt.
             *  Ich habe aber noch nicht herausgefunden, wie man den Event-Handler genau typisieren muss,
             *  damit man das Ding weglassen kann. */
            OnRequestSave?.Invoke(reservation, (caller, ok) => { Save(reservation); });
        }

        private void Save(ReservationDto reservation)
        {
            try
            {
                if (RowVersion != null)
                {
                    AutoReservationService.UpdateReservation(reservation);
                }
                else
                {
                    AutoReservationService.AddReservation(reservation);
                }
                OnRequestClose?.Invoke(this, null);
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnSaveError?.Invoke(this, null);
                if (CanExecuteReloadCommand) ReloadCommand.Execute(null);
            }
        }

        RelayCommand<object> _cancelCommand;
        public ICommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(param => this.ExecuteCancelCommand()));
        }

        private void ExecuteCancelCommand()
        {
            OnRequestClose?.Invoke(this, null);
        }

        RelayCommand<object> _reloadCommand;
        public ICommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new RelayCommand<object>(param => this.ExecuteReloadCommand(), param => CanExecuteReloadCommand));
        }

        private void ExecuteReloadCommand()
        {
            this.reservationDto = AutoReservationService.GetReservation(this.ReservationsNr);

            SelectedKunde = Kunden.FirstOrDefault(kunde => kunde.Id == reservationDto.Kunde.Id);
            SelectedAuto = Autos.FirstOrDefault(auto => auto.Id == reservationDto.Auto.Id);

            OnPropertyChanged(nameof(ReservationsNr));
            OnPropertyChanged(nameof(SelectedKunde));
            OnPropertyChanged(nameof(SelectedAuto));
            OnPropertyChanged(nameof(Von));
            OnPropertyChanged(nameof(Bis));
            OnPropertyChanged(nameof(RowVersion));
            OnPropertyChanged(nameof(CanExecuteReloadCommand));
        }

        public bool CanExecuteReloadCommand
        {
            get => RowVersion != null;
            private set { }
        }

        #endregion
    }
}
