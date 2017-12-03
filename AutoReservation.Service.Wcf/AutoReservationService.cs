using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using System.Collections.Generic;
using System.ServiceModel;

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
            kundeManager.Delete(kundeEntity);
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
            kundeManager.Update(kundeEntity);
        }

        #endregion

        #region Auto

        AutoManager autoManager = new AutoManager();

        public bool IsAutoAvailable(AutoDto auto, DateTime von, DateTime bis)
        {
            WriteActualMethod();
            // Waiting for Reservation to be merged to check availability
            throw new NotImplementedException();
        }

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
            autoManager.Update(autoEntity);
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
            autoManager.Delete(autoEntity);
        }

        #endregion
    }
}