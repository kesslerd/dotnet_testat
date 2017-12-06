using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class AutoUnavailableFault
    {
        [DataMember]
        public string Message{ get; set; }
    }
}
