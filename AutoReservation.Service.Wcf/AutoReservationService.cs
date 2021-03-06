﻿using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod() 
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        #region Kunde
        KundeManager kundeManager = new KundeManager();

        public void AddKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            kundeManager.Add(kundeEntity);
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            try
            {
                kundeManager.Delete(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException<DataManipulationFault>(new DataManipulationFault { Message = "Der Kunde wird momentan bearbeitet." });
            }
            
        }

        public KundeDto GetKunde(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(kundeManager.Find(id));
        }

        public List<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDtos(kundeManager.List);
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            try
            {
                kundeManager.Update(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException<DataManipulationFault>(new DataManipulationFault { Message = "Der Kunde wird momentan bearbeitet." });
            }
        }

        #endregion

        #region Auto

        AutoManager autoManager = new AutoManager();

        public AutoDto GetAuto(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(autoManager.Find(id));
        }

        public List<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDtos(autoManager.List);
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            var autoEntity = DtoConverter.ConvertToEntity(auto);
            try { 
                autoManager.Update(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                throw new FaultException<DataManipulationFault>(new DataManipulationFault { Message = "Das Auto wird momentan bearbeitet." });
            }
        }


        public void AddAuto(AutoDto auto)
        {
            WriteActualMethod();
            var autoEntity = DtoConverter.ConvertToEntity(auto);
            autoManager.Add(autoEntity);
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            var autoEntity = DtoConverter.ConvertToEntity(auto);
       
            try
            {
                autoManager.Delete(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                throw new FaultException<DataManipulationFault>(new DataManipulationFault { Message = "Das Auto wird momentan bearbeitet." });
            }
        }

        #endregion

        #region Reservation

        ReservationManager reservationManager = new ReservationManager();

        public bool IsAutoAvailable(AutoDto auto, DateTime von, DateTime bis)
        {
            return reservationManager.CheckAutoAvailability(auto.Id, von, bis);
        }

        public ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDto(reservationManager.Find(id));
        }

        public List<ReservationDto> GetReservations(bool includeFinished = true)
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDtos(reservationManager.List(includeFinished));
        }
        
        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var reservationEntity = DtoConverter.ConvertToEntity(reservation);
            try { 
                reservationManager.Update(reservationEntity);
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException<DataManipulationFault> (new DataManipulationFault { Message = "Die Reservation wird momentan bearbeitet." });
            }
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault> (new InvalidDateRangeFault { Message = "Ungültiger Datumsbereich eingegeben.", MessageDetails = e.Message });
            }
            catch (AutoUnavailableException)
            {
                throw new FaultException<AutoUnavailableFault> (new AutoUnavailableFault { Message = "Das gewählte Fahrzeug ist zur Zeit nicht verfügbar." });
            }
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var reservationEntity = DtoConverter.ConvertToEntity(reservation);

            try
            {
                reservationManager.Add(reservationEntity);
            }
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault>(new InvalidDateRangeFault { Message = "Ungültiger Datumsbereich eingegeben.", MessageDetails = e.Message });
            }
            catch (AutoUnavailableException)
            {
                throw new FaultException<AutoUnavailableFault>(new AutoUnavailableFault { Message = "Das gewählte Fahrzeug ist zur Zeit nicht verfügbar." });
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            var reservationEntity = DtoConverter.ConvertToEntity(reservation);

            try
            {
                reservationManager.Delete(reservationEntity);
            }
            catch (OptimisticConcurrencyException<Reservation>)
            {
                throw new FaultException<DataManipulationFault>(new DataManipulationFault { Message = "Die Reservation wird momentan bearbeitet." });
            }
        }

        #endregion
    }
}