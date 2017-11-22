using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    public interface IServiceFactory
    {
        IAutoReservationService GetService();
    }
}