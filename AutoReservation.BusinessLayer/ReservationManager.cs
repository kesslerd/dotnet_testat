using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> List
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    return context.Reservationen.ToList();
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

        public void Add(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                // TODO Constraints einbauen

                context.Entry(reservation).State = EntityState.Added;
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }

        public void Update(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    // TODO Constraints einbauen

                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Reservation>(context, reservation);
                }
            }
        }

        public void Delete(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(reservation).State = EntityState.Deleted;
                    context.Reservationen.Remove(reservation);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Reservation>(context, reservation);
                }
            }
        }
    }
}