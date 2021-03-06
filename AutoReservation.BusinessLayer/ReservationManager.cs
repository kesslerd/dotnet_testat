﻿using System;
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
        private const string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";

        public List<Reservation> List(bool includeFinished = true)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.Include(res => res.Auto).Include(res => res.Kunde).Where(res => includeFinished || res.Bis > DateTime.Now).ToList();
            }
            
        }
        
        public Reservation Find(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.Include(res => res.Auto).Include(res => res.Kunde).FirstOrDefault(res => res.ReservationsNr == id);
            }
        }

        public void Add(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                CheckDateRange(reservation.Von, reservation.Bis);
                CheckAvailability(reservation);

                context.Entry(reservation).State = EntityState.Added;
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
                    CheckAvailability(reservation, true);

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
            if (To < From)
            {
                throw new InvalidDateRangeException($"Bis-Datum [{To}] liegt vor dem Von-Datum [{From}].");
            }

            TimeSpan duration = To - From;
            if (duration.TotalDays < 1)
            {
                throw new InvalidDateRangeException($"Zeitspanne von [{From}] bis [{To}] beträgt weniger als 24 Stunden.");
            }
        }

        /// <summary>
        /// Creates a query to check the availability of a car within the given date range.
        /// </summary>
        /// <param name="autoId"></param>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <param name="context"></param>
        /// <returns>query</returns>
        private IQueryable<Reservation> CreateAvailabilityQuery(int autoId, DateTime von, DateTime bis, AutoReservationContext context = null)
        {
            if (context == null)
            {
                context = new AutoReservationContext();
            }
            return (from r in context.Reservationen
                    where (
                        (von <= r.Von && bis >= r.Bis) ||
                        (von <= r.Von && bis >= r.Von) ||
                        (von >= r.Von && bis <= r.Bis) ||
                        (von <= r.Bis && bis >= r.Bis)
                    ) && autoId == r.AutoId
                    select r);
        }

        /// <summary>
        /// Checks whether the given Reservation overlaps with an existing one.
        /// Should this be the case, an <see cref="AutoUnavailableException"/> will be thrown.
        /// </summary>
        /// <param name="reservation"></param>
        /// <param name="ignoreOneself"></param>
        private void CheckAvailability(Reservation reservation, bool ignoreOneself = false)
        {
            using (var context = new AutoReservationContext())
            {
                var query = CreateAvailabilityQuery(reservation.AutoId, reservation.Von, reservation.Bis, context);

                if (ignoreOneself)
                {
                    query = query.Where(r => r.ReservationsNr != reservation.ReservationsNr);
                }

                if (query.Any())
                {
                    throw new AutoUnavailableException($"Im Zeitraum vom {reservation.Von.ToString(DateTimeFormat)} bis zum {reservation.Bis.ToString(DateTimeFormat)} existiert bereits eine Reservation.");
                }
            }
        }

        public bool CheckAutoAvailability(int autoId, DateTime von, DateTime bis)
        {
            return !CreateAvailabilityQuery(autoId, von, bis).Any();
        }

    }

}