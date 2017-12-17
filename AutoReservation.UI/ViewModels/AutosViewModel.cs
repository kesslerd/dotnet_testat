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
    public class AutosViewModel : BaseTabViewModel<AutoDto>
    {
        private List<AutoDto> _autos;
        public List<AutoDto> Autos
        {
            get
            {
                return _autos ?? (_autos = new List<AutoDto>());
            }
            private set
            {
                _autos = value;
                OnPropertyChanged(nameof(Autos));
            }
        }

        protected override void ExecuteRefreshCommand()
        {
            Autos = AutoReservationService.GetAutos();
        }

        protected override void Delete(AutoDto auto)
        {
            try
            {
                AutoReservationService.DeleteAuto(auto);
            } catch (FaultException<DataManipulationFault> e)
            {
                InvokeOnRequestDeleteFailed();
            }
            RefreshCommand.Execute(null);
        }
    }
}
