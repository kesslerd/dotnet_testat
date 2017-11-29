using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        #region Kunde
        KundeDto GetKunde(int id);
        List<KundeDto> GetKunden();
        void UpdateKunde(KundeDto kunde);
        void AddKunde(KundeDto kunde);
        void DeleteKunde(KundeDto kunde);
        #endregion
    }
}
