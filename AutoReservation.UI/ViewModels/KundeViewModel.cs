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

        private KundeDto kundeDto;

        public int Id
        {
            get { return kundeDto.Id; }
            set { kundeDto.Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public String Nachname
        {
            get { return kundeDto.Nachname; }
            set { kundeDto.Nachname = value; OnPropertyChanged(nameof(Nachname)); }
        }

        public String Vorname
        {
            get { return kundeDto.Vorname; }
            set { kundeDto.Vorname = value; OnPropertyChanged(nameof(Vorname)); }
        }


        public DateTime Geburtsdatum
        {
            get { return kundeDto.Geburtsdatum; }
            set { kundeDto.Geburtsdatum = value; OnPropertyChanged(nameof(Geburtsdatum)); }
        }


        public byte[] RowVerwion
        {
            get { return kundeDto.RowVersion; }
            set { kundeDto.RowVersion = value; OnPropertyChanged(nameof(RowVerwion)); }
        }
    }
}
