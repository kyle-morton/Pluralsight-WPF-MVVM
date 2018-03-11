using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Logic.Services;

namespace ZzaDashboard.Demo.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {

        #region PROPS

        #region event handlers

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region notify props

        private ICustomersRepository _repo = new CustomersRepository();
        /// <summary>
        /// list of current customers
        /// </summary>
        private ObservableCollection<Customer> _customers;
        /// <summary>
        /// currently selected customer
        /// </summary>
        private Customer _selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    //Updates UI that Customers property is changed (must be here or won't show initial list!)
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
                }
            }
        }

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    //reevaluate whether delete button should be enabled
                    //CanExecute is run by default once (on init) so forcing it to rerun
                    DeleteCommand.RaiseCanExecuteChanged();

                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
                }

            }
        }

        #endregion

        #region commands



        #endregion

        #endregion

    }
}
