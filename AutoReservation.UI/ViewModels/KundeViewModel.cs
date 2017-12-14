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
using System.Data.SqlTypes;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {
        private KundeDto kundeDto = new KundeDto();

        public KundeViewModel(int id = -1)
        {
            if (id != -1)
            {
                this.Id = id;
                ReloadCommand.Execute(null);
            }
        }

        public int Id
        {
            get { return kundeDto.Id; }
            set { kundeDto.Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public String Nachname
        {
            get { return kundeDto.Nachname; }
            set { kundeDto.Nachname = value; OnPropertyChanged(nameof(Nachname)); OnPropertyChanged(nameof(CanSafe)); }
        }

        public String Vorname
        {
            get { return kundeDto.Vorname; }
            set { kundeDto.Vorname = value; OnPropertyChanged(nameof(Vorname)); OnPropertyChanged(nameof(CanSafe)); }
        }


        public DateTime Geburtsdatum
        {
            get { return kundeDto.Geburtsdatum; }
            set { kundeDto.Geburtsdatum = value; OnPropertyChanged(nameof(Geburtsdatum)); OnPropertyChanged(nameof(CanSafe)); }
        }


        public byte[] RowVersion
        {
            get { return kundeDto.RowVersion; }
            set { kundeDto.RowVersion = value; OnPropertyChanged(nameof(RowVersion)); }
        }

        public event EventHandler OnRequestClose;
        public event EventHandler OnSaveError;

        public bool CanSafe
        {
            get
            {
                return Nachname != null && Nachname.Trim().Length != 0 && Vorname != null && Vorname.Trim().Length != 0 && Geburtsdatum != null && Geburtsdatum > (DateTime)SqlDateTime.MinValue;
            }
        }

        #region commands
        RelayCommand<object> _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ?? (_saveCommand = new RelayCommand<object>(param => this.executeSaveCommand(), param => CanSafe));
        }

        private void executeSaveCommand()
        {
            try { 
                if(RowVersion != null)
                {
                    AutoReservationService.UpdateKunde(this.kundeDto);
                }
                else
                {
                    AutoReservationService.AddKunde(this.kundeDto);
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
            get => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(param => this.executeCancelCommand()));
        }

        private void executeCancelCommand()
        {
            OnRequestClose?.Invoke(this, null);
        }

        RelayCommand<object> _reloadCommand;
        public ICommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new RelayCommand<object>(param => this.executeReloadCommand(), param => CanExecuteReloadCommand));
        }

        private void executeReloadCommand()
        {
            this.kundeDto = AutoReservationService.GetKunde(this.Id);
            OnPropertyChanged(nameof(Nachname));
            OnPropertyChanged(nameof(Vorname));
            OnPropertyChanged(nameof(Geburtsdatum));
            OnPropertyChanged(nameof(CanSafe));
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
