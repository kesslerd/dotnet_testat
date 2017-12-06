using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.BusinessLayer.Exceptions
{
    class AutoUnavailableException : Exception
    {
        public AutoUnavailableException(string message) : base(message) { }
    }
}
