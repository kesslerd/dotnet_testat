using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.UI.Service
{
    class Service
    {

        static Service()
        {
            channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            AutoReservationService = channelFactory.CreateChannel();

        }

        private static ChannelFactory<IAutoReservationService> channelFactory;

        public static IAutoReservationService AutoReservationService
        {
            get;
            private set;
        }


    }
}
