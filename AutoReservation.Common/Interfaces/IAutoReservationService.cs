using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        #region Kunde
        [OperationContract]
        KundeDto GetKunde(int id);

        [OperationContract]
        List<KundeDto> GetKunden();

        [OperationContract]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void AddKunde(KundeDto kunde);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);
        #endregion
    }
}
