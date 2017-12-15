using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.UI.ViewModels
{
    public abstract class BaseDialogViewModel : BaseViewModel
    {
        public event EventHandler OnRequestClose;
        public event EventHandler OnSaveError;

        RelayCommand<object> _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ?? (_saveCommand = new RelayCommand<object>(param => this.ExecuteSaveCommand(), param => CanExecuteSaveCommand()));
        }

        protected abstract void ExecuteSaveCommand();
        protected abstract bool CanExecuteSaveCommand();

        RelayCommand<object> _cancelCommand;
        public ICommand CancelCommand
        {
            get => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(param => this.ExecuteCancelCommand()));
        }

        private void ExecuteCancelCommand()
        {
            OnRequestClose?.Invoke(this, null);
        }

        RelayCommand<object> _reloadCommand;
        public ICommand ReloadCommand
        {
            get => _reloadCommand ?? (_reloadCommand = new RelayCommand<object>(param => this.ExecuteReloadCommand(), param => CanExecuteReloadCommand()));
        }

        protected abstract void ExecuteReloadCommand();
        protected abstract bool CanExecuteReloadCommand();

        protected virtual void InvokeOnRequestClose(EventArgs e = null)
        {
            OnRequestClose?.Invoke(this, e);
        }

        protected virtual void InvokeOnSaveError(EventArgs e = null)
        {
            OnSaveError?.Invoke(this, e);
        }
    }
}
