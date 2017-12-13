using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public enum AutoKlasse
    {
        [EnumMember]
        Standard,
        [EnumMember]
        Mittelklasse,
        [EnumMember]
        Luxusklasse
    }
}
