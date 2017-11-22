using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        public int ReservationsNr { get; set; }
        public int AutoId { get; set; }
        public int KundenId { get; set; }
        public DateTime Bis { get; set; }
        public DateTime Von { get; set; }
        public byte[] ReservationsVersion { get; set; }

        public virtual Kunde kunde { get; set; }
        public virtual Auto auto { get; set; }

    }
}
