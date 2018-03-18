using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion


    }
}
