using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {

        public List<Auto> List
        {
            get
            {
                using (var context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

        public Auto Find(int id)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Autos.Find(id);
            }
        }

        public void Add(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
        }

        public void Update(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(auto).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    try
                    { 
                        throw CreateOptimisticConcurrencyException<Auto>(context, auto);
                    } 
                    catch (NullReferenceException){ }

                }
            }
        }

        public void Delete(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(auto).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Auto>(context, auto);
                }
            }
        }

    }
}