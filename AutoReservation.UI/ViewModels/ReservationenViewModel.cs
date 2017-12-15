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
using System.Windows.Threading;

namespace AutoReservation.UI.ViewModels
{
    public class ReservationenViewModel : BaseTabViewModel<ReservationDto>
    {
        public ReservationenViewModel()
        {
            StartDispatcher();
        }

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
        
        private bool _includeFinished = true;
        public bool IncludeFinished
        {
            get
            {
                return _includeFinished;
            }
            set
            {
                _includeFinished = value;
                ExecuteRefreshCommand();
                OnPropertyChanged(nameof(IncludeFinished));
            }
        }

        private bool _includeFinished = true;
        public bool IncludeFinished
        {
            get
            {
                return _includeFinished;
            }
            set
            {
                _includeFinished = value;
                ExecuteRefreshCommand();
                OnPropertyChanged(nameof(IncludeFinished));
            }
        }

        protected override void ExecuteRefreshCommand()
        {
            Reservationen = AutoReservationService.GetReservations(IncludeFinished);
        }

        protected override void Delete(ReservationDto reservation)
        {
            try
            {
                AutoReservationService.DeleteReservation(reservation);
            }
            catch (FaultException<DataManipulationFault>)
            {
                InvokeOnRequestDeleteFailed();
            }
            RefreshCommand.Execute(null);
        }

        private void StartDispatcher()
        {
            var dispatcher = new DispatcherTimer();
            dispatcher.Tick += (sender, arg) => ExecuteRefreshCommand();
            dispatcher.Interval = new TimeSpan(0, 0, 30);
            dispatcher.Start();
        }
    }
}
