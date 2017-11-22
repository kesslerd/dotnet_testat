using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Ui.Factory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        private readonly ObservableCollection<KundeDto> kunden = new ObservableCollection<KundeDto>();

        public KundeViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<KundeDto> Kunden
        {
            get { return kunden; }
        }

        private KundeDto selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                if (selectedKunde != value)
                {
                    selectedKunde = value;
                    OnPropertyChanged(nameof(SelectedKunde));
                }
            }
        }


        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new RelayCommand(param => Load(), param => CanLoad()));
            }
        }

        protected override void Load()
        {
            Kunden.Clear();
            foreach (var kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
            }
            SelectedKunde = Kunden.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return ServiceExists;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData()));
            }
        }

        private void SaveData()
        {
            foreach (var kunde in Kunden)
            {
                if (kunde.Id == default(int))
                {
                    Service.InsertKunde(kunde);
                }
                else
                {
                    Service.UpdateKunde(kunde);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (!ServiceExists)
            {
                return false;
            }

            return Validate(Kunden);
        }


        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                return newCommand ?? (newCommand = new RelayCommand(param => New(), param => CanNew()));
            }
        }

        private void New()
        {
            Kunden.Add(new KundeDto { Geburtsdatum = DateTime.Today });
        }

        private bool CanNew()
        {
            return ServiceExists;
        }

        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }

        private void Delete()
        {
            Service.DeleteKunde(SelectedKunde);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedKunde != null &&
                SelectedKunde.Id != default(int);
        }

        #endregion

    }
}
