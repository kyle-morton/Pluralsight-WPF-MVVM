using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel
    {

        #region PROPS

        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                //reevaluate whether delete button should be enabled
                //CanExecute is run by default once (on init) so forcing it to rerun
                DeleteCommand.RaiseCanExecuteChanged(); 
            }
        }


        #region COMMANDS

        public RelayCommand DeleteCommand { get; private set; }

        #endregion

        #endregion

        public CustomerListViewModel()
        {
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        #region BEHAVIORS (LOADED EVENT)

        /// <summary>
        /// behavior called via MvvmBehaviors (assigned in XAML)
        /// </summary>
        public async void LoadCustomers()
        {
            //if in design mode, return so it doesn't break
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        #endregion

        #region COMMANDS

        private void OnDelete()
        {
            if (SelectedCustomer == null)
                return;

            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        #endregion

    }
}
