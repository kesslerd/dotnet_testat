using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using System.Windows.Input;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {
        private KundeDto kundeDto = new KundeDto();

        public int Id
        {
            get { return kundeDto.Id; }
            set { kundeDto.Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public String Nachname
        {
            get { return kundeDto.Nachname; }
            set { kundeDto.Nachname = value; OnPropertyChanged(nameof(Nachname)); }
        }

        public String Vorname
        {
            get { return kundeDto.Vorname; }
            set { kundeDto.Vorname = value; OnPropertyChanged(nameof(Vorname)); }
        }


        public DateTime Geburtsdatum
        {
            get { return kundeDto.Geburtsdatum; }
            set { kundeDto.Geburtsdatum = value; OnPropertyChanged(nameof(Geburtsdatum)); }
        }


        public byte[] RowVerwion
        {
            get { return kundeDto.RowVersion; }
            set { kundeDto.RowVersion = value; OnPropertyChanged(nameof(RowVerwion)); }
        }

        #region commands
        RelayCommand<object> _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ?? (_saveCommand = new RelayCommand<object>(param => this.executeSaveCommand()));
        }

        private void executeSaveCommand()
        {
            if(RowVerwion != null)
            {
                //save
            }
            else
            {
                //add
            }
        }

        RelayCommand<object> _cancelCommand;
        public ICommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(param => this.executeCancelCommand()));
        }

        private void executeCancelCommand()
        {
        }

        RelayCommand<object> _reloadCommand;
        public ICommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new RelayCommand<object>(param => this.executeReloadCommand(), param => canExecuteReloadCommand()));
        }

        private void executeReloadCommand()
        {
           
        }

        private bool canExecuteReloadCommand()
        {
            return RowVerwion != null;
        }
        #endregion

    }
}
