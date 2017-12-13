using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.UI.ViewModels
{
    public abstract class BaseTabViewModel<T> : BaseViewModel
    {
        public event EventHandler<object> OnRequestCreate;
        public event EventHandler<int> OnRequestEdit;
        public event EventHandler<EventHandler<bool>> OnRequestDelete;
        public event EventHandler<object> OnReqestDeleteFailed;

        RelayCommand<object> _addCommand;
        public ICommand AddCommand
        {
            get => _addCommand ?? (_addCommand = new RelayCommand<object>(param => this.ExecuteAddCommand()));
        }

        private void ExecuteAddCommand()
        {
            OnRequestCreate?.Invoke(this, null);
        }

        private RelayCommand<int> _editCommand;
        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new RelayCommand<int>(param => this.ExecuteEditCommand(param)));
        }

        protected void ExecuteEditCommand(int id)
        {
            OnRequestEdit?.Invoke(this, id);
        }

        RelayCommand<object> _refreshCommand;
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(param => this.ExecuteRefreshCommand()));
        }

        protected abstract void ExecuteRefreshCommand();

        RelayCommand<T> _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new RelayCommand<T>(param => this.ExecuteDeleteCommand(param)));
        }

        private void ExecuteDeleteCommand(T o)
        {
            OnRequestDelete?.Invoke(this, (caller, ok) => { if (ok) Delete(o); });
        }

        protected abstract void Delete(T o);

        protected virtual void OnRequestDeleteFailed(EventArgs e = null)
        {
            OnReqestDeleteFailed?.Invoke(this, null);
        }
    }
}
