using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationsNr { get; set; }
        public int AutoId { get; set; }
        public int KundeId { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        [Timestamp]
        public byte[] ReservationsVersion { get; set; }

        public virtual Kunde kunde { get; set; }
        public virtual Auto auto { get; set; }

    }
}
