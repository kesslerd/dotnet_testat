using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    public class NullServiceFactory : IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            return new NullAutoReservationService();
        }
    }

    public class NullAutoReservationService : IAutoReservationService
    {
        public List<AutoDto> Autos => new List<AutoDto>();
        public List<KundeDto> Kunden => new List<KundeDto>();
        public List<ReservationDto> Reservationen => new List<ReservationDto>();
        public AutoDto GetAutoById(int id) => null;
        public KundeDto GetKundeById(int id) => null;
        public ReservationDto GetReservationByNr(int reservationsNr) => null;
        public AutoDto InsertAuto(AutoDto auto) => null;
        public KundeDto InsertKunde(KundeDto kunde) => null;
        public ReservationDto InsertReservation(ReservationDto reservation) => null;
        public AutoDto UpdateAuto(AutoDto auto) => null;
        public KundeDto UpdateKunde(KundeDto kunde) => null;
        public ReservationDto UpdateReservation(ReservationDto reservation) => null;
        public void DeleteAuto(AutoDto auto) { }
        public void DeleteKunde(KundeDto kunde) { }
        public void DeleteReservation(ReservationDto reservation) { }
    }
}
