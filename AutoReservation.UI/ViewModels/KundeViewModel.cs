using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value, nameof(Id)); }
        }


        private String _nachname;

        public String Nachname
        {
            get { return _nachname; }
            set { SetProperty(ref _nachname, value, nameof(Nachname)); }
        }

        private String _vorname;

        public String Vorname
        {
            get { return _vorname; }
            set { SetProperty(ref _vorname, value, nameof(Vorname)); }
        }


        private DateTime _geburtsdatum;

        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set { SetProperty(ref _geburtsdatum, value, nameof(Geburtsdatum)); }
        }

        private byte[] _rowVersion;

        public byte[] RowVerwion
        {
            get { return _rowVersion; }
            set { SetProperty(ref _rowVersion, value, nameof(RowVerwion)); }
        }
    }
}
