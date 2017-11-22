using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        [Key]
        public int Id { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual List<Reservation> Reservationen { get; set; }
    }
}
