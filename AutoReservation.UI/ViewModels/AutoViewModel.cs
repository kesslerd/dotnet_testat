﻿using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;

namespace AutoReservation.UI.ViewModels
{
    public class AutoViewModel : BaseViewModel
    {
        private AutoDto autoDto = new AutoDto();

        public AutoViewModel(int id = -1)
        {
            if (id != -1)
            {
                this.Id = id;
                ReloadCommand?.Execute(null);
            }
        }

        public int Id
        {
            get { return autoDto.Id; }
            set
            {
                autoDto.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public String Marke
        {
            get { return autoDto.Marke; }
            set
            {
                autoDto.Marke = value;
                OnPropertyChanged(nameof(Marke));
            }
        }

        public int Basistarif
        {
            get { return autoDto.Basistarif; }
            set
            {
                autoDto.Basistarif = value;
                OnPropertyChanged(nameof(Basistarif));
            }
        }

        public int Tagestarif
        {
            get { return autoDto.Tagestarif; }
            set
            {
                autoDto.Tagestarif = value;
                OnPropertyChanged(nameof(Tagestarif));
            }
        }

        public AutoKlasse AutoKlasse
        {
            get { return autoDto.AutoKlasse; }
            set
            {
                autoDto.AutoKlasse = value;
                OnPropertyChanged(nameof(AutoKlasse));
            }
        }
        
        public byte[] RowVersion
        {
            get { return autoDto.RowVersion; }
            set
            {
                autoDto.RowVersion = value;
                OnPropertyChanged(nameof(RowVersion));
            }
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
                    AutoReservationService.UpdateAuto(this.autoDto);
                }
                else
                {
                    AutoReservationService.AddAuto(this.autoDto);
                }
                OnRequestClose?.Invoke(this, null);
            }
            catch (FaultException<DataManipulationFault> e)
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
            this.autoDto = AutoReservationService.GetAuto(this.Id);
            OnPropertyChanged(nameof(Marke));
            OnPropertyChanged(nameof(Basistarif));
            OnPropertyChanged(nameof(Tagestarif));
            OnPropertyChanged(nameof(AutoKlasse));
            OnPropertyChanged(nameof(RowVersion));
        }

        private bool canExecuteReloadCommand()
        {
            return RowVersion != null;
        }

        #endregion
    }
}