using AutoReservation.Common.DataTransferObjects.Core;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IServiceFactory factory;

        protected ViewModelBase(IServiceFactory factory)
        {
            this.factory = factory;
        }

        protected IAutoReservationService Service { get; private set; }

        public bool ServiceExists => Service  != null;

        public void Init()
        {
            Service = factory.GetService();
            Load();
        }

        protected abstract void Load();

        protected bool Validate(IEnumerable<IValidatable> items) 
        {
            var errorText = new StringBuilder();
            foreach (var item in items)
            {
                var error = item.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(item.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                if (errorText != value)
                {
                    errorText = value;
                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
