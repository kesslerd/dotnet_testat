using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
        [FaultContract(typeof(DataManipulationFault))]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void AddKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(DataManipulationFault))]
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
        [FaultContract(typeof(DataManipulationFault))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void AddAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(DataManipulationFault))]
        void DeleteAuto(AutoDto auto);

        #endregion

        #region Reservation

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        List<ReservationDto> GetReservations();

        [OperationContract]
        [FaultContract(typeof(DataManipulationFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        void AddReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(DataManipulationFault))]
        void DeleteReservation(ReservationDto reservation);

        #endregion
    }
}
