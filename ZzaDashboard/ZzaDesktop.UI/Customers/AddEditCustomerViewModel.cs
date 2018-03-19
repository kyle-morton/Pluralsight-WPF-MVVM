using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
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

        #endregion

        public AddEditCustomerViewModel()
        {
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        #region COMMANDS 

        private void OnSave()
        {
            Done();
        }

        private bool CanSave()
        {
            return true;
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
            Customer = new SimpleEditableCustomer();
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

        #endregion
    }
}
