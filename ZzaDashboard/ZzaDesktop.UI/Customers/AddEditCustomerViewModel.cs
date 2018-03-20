using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Logic.Services;
using ZzaDashboard.Shared;

namespace ZzaDesktop.UI.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        #region PROPS

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private Customer _editCustomer = null;

        private SimpleEditableCustomer _customer;

        public SimpleEditableCustomer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done = delegate { };

        private ICustomersRepository _repo;

        #endregion

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        #region COMMANDS 

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editCustomer);
            else
                await _repo.AddCustomerAsync(_editCustomer);
            Done();
        }

        /// <summary>
        /// copy over values from form to object we'll save in db
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }

        private bool CanSave()
        {

            return !Customer.HasErrors;
        }

        private void OnCancel()
        {
            Done();
        } 

        #endregion

        #region CUSTOMER

        public void SetCustomer(Customer customer)
        {
            _editCustomer = customer;
            if (Customer != null)
                Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
            CopyCustomer(customer, Customer);
        }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Phone = source.Phone;
                target.Email = source.Email;
            }
        }

        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
