using AutoReservation.Common.Interfaces;
using System.ServiceModel;

namespace AutoReservation.Ui.Factory
{
    public class RemoteDataAccessServiceFactory : IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }
    }
}
