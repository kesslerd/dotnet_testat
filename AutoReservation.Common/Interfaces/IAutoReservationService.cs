using AutoReservation.Common.DataTransferObjects;
using System;
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

        #region Auto

        [OperationContract]
        bool IsAutoAvailable(AutoDto auto, DateTime von, DateTime bis);

        [OperationContract]
        AutoDto GetAuto(int id);

        [OperationContract]
        List<AutoDto> GetAutos();

        [OperationContract]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void AddAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);
        
        #endregion
    }
}
