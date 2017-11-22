using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Auto
    {
        [Key]
        public int Id { get; set; }
        public string Marke { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int Tagestarif { get; set; }
        
        public virtual List<Reservation> Reservationen { get; set; }
    }

    public class StandardAuto : Auto { }

    public class LuxusklasseAuto : Auto {
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto { }
}
