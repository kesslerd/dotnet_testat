using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using static AutoReservation.UI.Service.Service;
using System.Windows.Input;
using System.ServiceModel;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationenViewModel : BaseTabViewModel<ReservationDto>
    {
        private List<ReservationDto> _reservationen;
        public List<ReservationDto> Reservationen
        {
            get
            {
                return _reservationen ?? (_reservationen = new List<ReservationDto>());
            }
            private set
            {
                _reservationen = value;
                OnPropertyChanged(nameof(Reservationen));
            }
        }

        protected override void ExecuteRefreshCommand()
        {
            Reservationen = AutoReservationService.GetReservations();
        }

        protected override void Delete(ReservationDto reservation)
        {
            try
            {
                AutoReservationService.DeleteReservation(reservation);
            }
            catch (FaultException<DataManipulationFault>)
            {
                OnRequestDeleteFailed();
            }
            RefreshCommand.Execute(null);
        }
    }
}
