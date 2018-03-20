using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Logic.Services;
using ZzaDashboard.Shared;
using ZzaDesktop.UI.Customers;
using ZzaDesktop.UI.OrderPrep;
using ZzaDesktop.UI.Orders;
using Microsoft.Practices.Unity;

namespace ZzaDesktop.UI
{
    public class MainWindowViewModel : BindableBase
    {

        #region PROPS

        #region view models

        private CustomerListViewModel _customerListViewModel;
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private AddEditCustomerViewModel _addEditCustomerViewModel;

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
            //todo: find fix fo newest IOC Unity package not having Resolve<type> method available
            //have container construct & inject both view models (that require customers repo)
            _customerListViewModel = IocContainerHelper.Container.Resolve<CustomerListViewModel>();
            _addEditCustomerViewModel = IocContainerHelper.Container.Resolve<AddEditCustomerViewModel>();


            //set NavCommand to call OnNav method
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _addEditCustomerViewModel.Done += NavToCustomerList;
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

        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }

        #endregion


    }
}
