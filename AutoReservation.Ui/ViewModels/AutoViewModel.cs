using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Ui.Factory;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoReservation.Ui.ViewModels
{
    public class AutoViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AutoDto> autos = new ObservableCollection<AutoDto>();

        public AutoViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<AutoDto> Autos
        {
            get { return autos; }
        }

        private AutoDto selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return selectedAuto; }
            set
            {
                if (selectedAuto != value)
                {
                    selectedAuto = value;
                    OnPropertyChanged(nameof(SelectedAuto));
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
            Autos.Clear();
            foreach (var auto in Service.Autos)
            {
                Autos.Add(auto);
            }
            SelectedAuto = Autos.FirstOrDefault();
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
            foreach (var auto in Autos)
            {
                if (auto.Id == default(int))
                {
                    Service.InsertAuto(auto);
                }
                else
                {
                    Service.UpdateAuto(auto);
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

            return Validate(Autos);
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
            Autos.Add(new AutoDto());
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
            Service.DeleteAuto(SelectedAuto);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedAuto != null &&
                SelectedAuto.Id != default(int);
        }

        #endregion

    }
}