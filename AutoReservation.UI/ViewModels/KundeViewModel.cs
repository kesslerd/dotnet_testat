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
    public class KundeViewModel : BaseDialogViewModel
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

        public byte[] RowVersion
        {
            get { return kundeDto.RowVersion; }
            set { kundeDto.RowVersion = value; OnPropertyChanged(nameof(RowVersion)); }
        }

        protected override void ExecuteSaveCommand()
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
                InvokeOnRequestClose();
            }
            catch (FaultException<DataManipulationFault>)
            {
                InvokeOnSaveError();
                if (CanExecuteReloadCommand()) ReloadCommand.Execute(null);                        
            }
        }

        protected override bool CanExecuteSaveCommand()
        {
            return Nachname != null && Nachname.Trim().Length != 0 && Vorname != null && Vorname.Trim().Length != 0 && Geburtsdatum != null && Geburtsdatum > (DateTime)SqlDateTime.MinValue;
        }

        protected override void ExecuteReloadCommand()
        {
            this.kundeDto = AutoReservationService.GetKunde(this.Id);
            OnPropertyChanged(nameof(Nachname));
            OnPropertyChanged(nameof(Vorname));
            OnPropertyChanged(nameof(Geburtsdatum));
            OnPropertyChanged(nameof(RowVersion));
            OnPropertyChanged(nameof(CanExecuteReloadCommand));
        }

        protected override bool CanExecuteReloadCommand()
        {
            return RowVersion != null;
        }
    }
}
