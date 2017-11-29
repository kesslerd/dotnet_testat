using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;

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

        public Reservation Find(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.Find(id);
            }
        }

        public void Add(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                CheckDateRange(reservation.Von, reservation.Bis);
                CheckAvailability(reservation);

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
                    CheckDateRange(reservation.Von, reservation.Bis);
                    CheckAvailability(reservation);

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

        /// <summary>
        /// Checks the given dates for two things:
        /// <list type="bullet">
        /// <item>
        /// <description>Is <c>To</c> before <c>From</c>?</description>
        /// </item>
        /// <item>
        /// <description>Is the duration between <c>From</c> and <c>To</c> less then 1 day?</description>
        /// </item>
        /// </list>
        /// Should any of these conditions be <c>true</c>, this check will throw an <see cref="InvalidDateRangeException"/>.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        private void CheckDateRange(DateTime From, DateTime To)
        {
            if (To > From)
            {
                throw new InvalidDateRangeException($"Bis-Datum [{To}] liegt vor dem Von-Datum [{From}].");
            }

            TimeSpan duration = To - From;
            if (duration.TotalDays < 1)
            {
                throw new InvalidDateRangeException($"Zeitspanne von [{From}] bis [{To}] beträgt weniger als 24 Stunden.");
            }
        }

        private bool CheckAvailability(Reservation reservation)
        {
            // TODO Check for availability

            return false;
        }

    }
}