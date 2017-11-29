using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using System.Collections.Generic;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        KundeManager kundeManager = new KundeManager();

        private static void WriteActualMethod() 
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public void AddKunde(KundeDto kunde)
        {
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            kundeManager.Add(kundeEntity);
        }

        public void DeleteKunde(KundeDto kunde)
        {
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            kundeManager.Delete(kundeEntity);
        }

        public KundeDto GetKunde(int id)
        {
            return DtoConverter.ConvertToDto(kundeManager.Find(id));
        }

        public List<KundeDto> GetKunden()
        {
            return DtoConverter.ConvertToDtos(kundeManager.List);
        }

        public void UpdateKunde(KundeDto kunde)
        {
            var kundeEntity = DtoConverter.ConvertToEntity(kunde);
            kundeManager.Update(kundeEntity);
        }
    }
}