using AutoReservation.Common.DataTransferObjects;
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
    public class AutoViewModel : BaseDialogViewModel
    {
        private AutoDto autoDto = new AutoDto();

        public AutoViewModel(int id = -1)
        {
            if (id != -1)
            {
                this.Id = id;
                ReloadCommand.Execute(null);
            }
        }

        public int Id
        {
            get { return autoDto.Id; }
            set { autoDto.Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public String Marke
        {
            get { return autoDto.Marke; }
            set { autoDto.Marke = value; OnPropertyChanged(nameof(Marke)); OnPropertyChanged(nameof(CanSafe)); }
        }

        public int Basistarif
        {
            get { return autoDto.Basistarif; }
            set { autoDto.Basistarif = value; OnPropertyChanged(nameof(Basistarif)); }
        }

        public int Tagestarif
        {
            get { return autoDto.Tagestarif; }
            set { autoDto.Tagestarif = value; OnPropertyChanged(nameof(Tagestarif)); }
        }

        public AutoKlasse AutoKlasse
        {
            get { return autoDto.AutoKlasse; }
            set
            {
                autoDto.AutoKlasse = value;
                OnPropertyChanged(nameof(AutoKlasse));
                OnPropertyChanged(nameof(CanSafe));
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

        public bool IsNew
        {
            get => RowVersion == null;
        }

        public override bool CanSafe
        {
            get
            {
                return Marke != null && Marke.Trim().Length != 0 && AutoKlasse != null;
            }
        }

        public override bool CanReload
        {
            get
            {
                return RowVersion != null;
            }
        }

        protected override void ExecuteSaveCommand()
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
                InvokeOnRequestClose();
            }
            catch (FaultException<DataManipulationFault> e)
            {
                InvokeOnSaveError();
                if (CanReload) ReloadCommand.Execute(null);
            }
        }

        protected override void ExecuteReloadCommand()
        {
            this.autoDto = AutoReservationService.GetAuto(this.Id);
            OnPropertyChanged(nameof(Marke));
            OnPropertyChanged(nameof(Basistarif));
            OnPropertyChanged(nameof(Tagestarif));
            OnPropertyChanged(nameof(AutoKlasse));
            OnPropertyChanged(nameof(CanSafe));
            OnPropertyChanged(nameof(RowVersion));
        }
    }
}
