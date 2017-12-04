using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> List
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    return context.Kunden.ToList();
                }
            }
        }

        public Kunde Find(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Kunden.Find(id);
            }
        }

        public void Add(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Added;
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
        }

        public void Update(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    if (Find(kunde.Id) == null) return;
                    context.Entry(kunde).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Kunde>(context, kunde);
                }
            }
        }

        public void Delete(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(kunde).State = EntityState.Deleted;
                    context.Kunden.Remove(kunde);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Kunde>(context, kunde);
                }
            }
        }
    }
}