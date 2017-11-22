using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Auto
    {
        public int Id { get; set; }
        public string Marke { get; set; }
        public byte[] RowVersion { get; set; }
        public int Tagestarif { get; set; }
    }

    public class StandardAuto : Auto { }

    public class LuxusklasseAuto : Auto {
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto { }
}
