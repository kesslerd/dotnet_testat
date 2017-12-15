﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Data.SqlTypes;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationViewModel : BaseDialogViewModel
    {
        private ReservationDto reservationDto = new ReservationDto();

        public ReservationViewModel(int reservationsNr = -1)
        {
            Kunden = AutoReservationService.GetKunden();
            Autos = AutoReservationService.GetAutos();

            if (reservationsNr != -1)
            {
                this.ReservationsNr = reservationsNr;
                ReloadCommand.Execute(null);
            }
        }

        public List<KundeDto> Kunden { get; private set; }
        public List<AutoDto> Autos { get; private set; }

        private KundeDto _selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set { _selectedKunde = value; OnPropertyChanged(nameof(SelectedKunde)); OnPropertyChanged(nameof(CanSafe)); }
        }

        private AutoDto _selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set { _selectedAuto = value; OnPropertyChanged(nameof(SelectedAuto)); OnPropertyChanged(nameof(CanSafe)); }
        }

        public int ReservationsNr
        {
            get { return reservationDto.ReservationsNr; }
            set { reservationDto.ReservationsNr = value; OnPropertyChanged(nameof(ReservationsNr)); }
        }

        public DateTime Von
        {
            get { return reservationDto.Von; }
            set { reservationDto.Von = value; OnPropertyChanged(nameof(Von)); OnPropertyChanged(nameof(CanSafe)); }
        }

        public DateTime Bis
        {
            get { return reservationDto.Bis; }
            set { reservationDto.Bis = value; OnPropertyChanged(nameof(Bis)); OnPropertyChanged(nameof(CanSafe)); }
        }

        public byte[] RowVersion
        {
            get { return reservationDto.RowVersion; }
            set { reservationDto.RowVersion = value; OnPropertyChanged(nameof(RowVersion)); }
        }

        public event EventHandler<EventHandler<object>> OnRequestSave;
        public event EventHandler OnSaveError;

        protected override void ExecuteSaveCommand()
        {
            OnRequestSave?.Invoke(this.reservationDto, (caller, _) => { Save(this.reservationDto); });
        }
        
        protected override bool CanExecuteSaveCommand()
        {
            return SelectedKunde != null && SelectedAuto != null && Von != null && Von > (DateTime)SqlDateTime.MinValue && Bis != null && Bis > (DateTime)SqlDateTime.MinValue;
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
                InvokeOnRequestClose();
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnSaveError?.Invoke(this, null);
                if (CanExecuteReloadCommand()) ReloadCommand.Execute(null);
            }
        }
        
        protected override void ExecuteReloadCommand()
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
            OnPropertyChanged(nameof(CanSafe));
        }

        protected override bool CanExecuteReloadCommand()
        {
            return RowVersion != null;
        }
    }
}
