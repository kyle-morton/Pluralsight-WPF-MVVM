using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Logic.Services;
using ZzaDashboard.Shared;

namespace ZzaDesktop.UI.Customers
{
    class CustomerListViewModel : BindableBase
    {

        #region PROPS

        private ICustomersRepository _repo;

        private ObservableCollection<Customer> _allCustomers;
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                SetProperty(ref _searchInput, value);
                FilterCustomers(_searchInput);
            }
        }

        #region events

        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        #endregion

        #endregion

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        #region BEHAVIORS

        /// <summary>
        /// load customers list into view model
        /// </summary>
        /// <returns></returns>
        public async void LoadCustomers()
        {
            //REMEMBER: behavior method must return void (not task) in this scenario
            _allCustomers 
                = Customers 
                = new ObservableCollection<Customer>(
                await _repo.GetCustomersAsync());
        }

        #endregion

        #region COMMANDS

        private void OnPlaceOrder(Customer customer) 
        {
            PlaceOrderRequested(customer.Id);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer { Id = Guid.NewGuid() });
        }

        private void OnEditCustomer(Customer customer)
        {
            EditCustomerRequested(customer);
        }

        private void OnClearSearch()
        {
            //empty search input to trigger propertychanged & filtercustomers()
            SearchInput = string.Empty; 
        }

        #endregion

        #region SEARCH 

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
            }
            else
            {
                Customers = new ObservableCollection<Customer>(
                        _allCustomers.Where(c => c.FullName.ToLower().Contains(searchInput.ToLower())));
            }
        }

        #endregion

    }
}
