using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Shared;
using ZzaDesktop.UI.Customers;
using ZzaDesktop.UI.OrderPrep;
using ZzaDesktop.UI.Orders;

namespace ZzaDesktop.UI
{
    public class MainWindowViewModel : BindableBase
    {

        #region PROPS

        #region view models

        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private AddEditCustomerViewModel _addEditCustomerViewModel = new AddEditCustomerViewModel();

        private BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        #endregion

        #region commands

        public RelayCommand<string> NavCommand { get; private set; }

        #endregion

        #endregion

        #region CTOR

        public MainWindowViewModel()
        {
            //set NavCommand to call OnNav method
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
        }

        #endregion

        #region NAVIGATION

        /// <summary>
        /// sets current view model based on destination string passed
        /// </summary>
        /// <param name="destination"></param>
        private void OnNav(string destination)
        {
            switch(destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }

        private void NavToOrder(Guid customerId)
        {
            //set id prop on vm (set context), then navigate to it
            _orderViewModel.CustomerId = customerId;
            CurrentViewModel = _orderViewModel;
        }

        private void NavToAddCustomer(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = false;
            _addEditCustomerViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerViewModel;
        }

        private void NavToEditCustomer(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = true;
            _addEditCustomerViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerViewModel;
        }

        #endregion


    }
}
