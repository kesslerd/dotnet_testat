using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;

namespace AutoReservation.UI.ViewModels
{
    public class KundenViewModel : BaseTabViewModel<KundeDto>
    {
        public KundenViewModel()
        {
            ExecuteRefreshCommand();
        }

        private List<KundeDto> _kunden;
        public List<KundeDto> Kunden
        {
            get
            {
                return _kunden ?? (_kunden = new List<KundeDto>());
            }
            private set
            {
                _kunden = value;
                OnPropertyChanged(nameof(Kunden));
            }
        }

        protected override void ExecuteRefreshCommand()
        {
            Kunden = AutoReservationService.GetKunden();
        }

        protected override void Delete(KundeDto kunde)
        {
            try
            {
                AutoReservationService.DeleteKunde(kunde);
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnRequestDeleteFailed();
            }
            RefreshCommand.Execute(null);
        }
    }
}
