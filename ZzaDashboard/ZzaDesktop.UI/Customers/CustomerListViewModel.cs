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

        private ICustomersRepository _repo = new CustomersRepository();


        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }

        #region events

        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        #endregion

        #endregion

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }

        #region BEHAVIORS

        /// <summary>
        /// load customers list into view model
        /// </summary>
        /// <returns></returns>
        public async void LoadCustomers()
        {
            //REMEMBER: behavior method must return void (not task) in this scenario


            Customers = new ObservableCollection<Customer>(
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

        #endregion

    }
}
