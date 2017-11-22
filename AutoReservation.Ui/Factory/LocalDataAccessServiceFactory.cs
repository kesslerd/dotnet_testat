using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
    public class LocalDataAccessServiceFactory : IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            return new AutoReservationService();
        }
    }
}
