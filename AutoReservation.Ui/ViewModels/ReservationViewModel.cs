using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Ui.Factory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationDto> reservationen = new ObservableCollection<ReservationDto>();

        public ReservationViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<ReservationDto> Reservationen
        {
            get { return reservationen; }
        }

        private ReservationDto selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    SelectedAutoId = value?.Auto != null ? value.Auto.Id : 0;
                    SelectedKundeId = value?.Kunde != null ? value.Kunde.Id : 0;

                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        private int selectedAutoId;
        public int SelectedAutoId
        {
            get { return selectedAutoId; }
            set
            {
                if (selectedAutoId != value)
                {
                    selectedAutoId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Auto = Autos.SingleOrDefault(a => a.Id == value);
                    }

                    OnPropertyChanged(nameof(SelectedAutoId));
                }
            }
        }

        private int selectedKundeId;
        public int SelectedKundeId
        {
            get { return selectedKundeId; }
            set
            {
                if (selectedKundeId != value)
                {
                    selectedKundeId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Kunde = Kunden.SingleOrDefault(k => k.Id == value);
                    }

                    OnPropertyChanged(nameof(SelectedKundeId));
                }
            }
        }

        private readonly ObservableCollection<AutoDto> autos = new ObservableCollection<AutoDto>();
        public ObservableCollection<AutoDto> Autos
        {
            get { return autos; }
        }

        private readonly ObservableCollection<KundeDto> kunden = new ObservableCollection<KundeDto>();
        public ObservableCollection<KundeDto> Kunden
        {
            get { return kunden; }
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
            Reservationen.Clear();
            
            Kunden.Clear();
            Autos.Clear();

            foreach (KundeDto kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
            }
            foreach (AutoDto auto in Service.Autos)
            {
                Autos.Add(auto);
            }
            foreach (ReservationDto reservation in Service.Reservationen)
            {
                Reservationen.Add(reservation);
            }
            SelectedReservation = Reservationen.FirstOrDefault();
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
            foreach (var reservation in Reservationen)
            {
                if (reservation.ReservationsNr == default(int))
                {
                    Service.InsertReservation(reservation);
                }
                else
                {
                    Service.UpdateReservation(reservation);
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

            return Validate(Reservationen);
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
            Reservationen.Add(new ReservationDto
            {
                Von = DateTime.Today,
                Bis = DateTime.Today
            });
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
            Service.DeleteReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedReservation != null &&
                SelectedReservation.ReservationsNr != default(int);
        }

        #endregion

    }
}