using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoReservation.UI.Service.Service;

namespace AutoReservation.UI.ViewModels
{
    public class AutosViewModel : BaseViewModel
    {

        public AutosViewModel()
        {
            ExecuteRefreshCommand();
        }

        private List<AutoDto> _autos;
        public List<AutoDto> Autos
        {
            get
            {
                return _autos ?? (_autos = new List<AutoDto>());
            }
            private set
            {
                _autos = value;
                OnPropertyChanged(nameof(Autos));
            }
        }

        public event EventHandler<int> OnRequestEditAuto;
        public event EventHandler<object> OnRequestCreateAuto;
        public event EventHandler<EventHandler<bool>> OnRequestDeleteAuto;
        public event EventHandler<object> OnRequestDeleteFailed;

        #region commands

        RelayCommand<object> _refreshCommand;

        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(param => this.ExecuteRefreshCommand()));
        }

        private void ExecuteRefreshCommand()
        {
            Autos = AutoReservationService.GetAutos();
        }

        RelayCommand<object> _addCommand;
        public ICommand AddCommand
            {
            get => _addCommand ?? (_addCommand = new RelayCommand<object>(param => ExecuteAddCommand()));
            }

        private void ExecuteAddCommand()
        {
            OnRequestCreateAuto?.Invoke(this, null);
        }

        RelayCommand<AutoDto> _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new RelayCommand<AutoDto>(param => ExecuteDeleteCommand(param)));
        }

        private void ExecuteDeleteCommand(AutoDto auto)
        {
            OnRequestDeleteAuto?.Invoke(this, (caller, ok) => { if (ok) Delete(auto); });
        }

        private void Delete(AutoDto auto)
        {
            try
            {
                AutoReservationService.DeleteAuto(auto);
            } catch (FaultException<DataManipulationFault> e)
            {
                OnRequestDeleteFailed?.Invoke(this, null);
            }
            RefreshCommand.Execute(null);
        }

        RelayCommand<int> _editCommand;
        public ICommand EditCommand
        {
            get => _editCommand ?? (_editCommand = new RelayCommand<int>(param => this.ExecuteEditCommand(param)));
        }

        private void ExecuteEditCommand(int id)
        {
            OnRequestEditAuto?.Invoke(this, id);
        }

        #endregion

    }
}
