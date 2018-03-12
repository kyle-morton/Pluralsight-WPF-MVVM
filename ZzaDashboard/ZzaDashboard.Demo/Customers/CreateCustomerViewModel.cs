using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Logic.Services;
using ZzaDashboard.Shared;

namespace ZzaDashboard.Demo.Customers
{
    public class CreateCustomerViewModel : INotifyPropertyChanged
    {

        #region PROPS

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private Customer _newCustomer;

        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set
            {
                if (_newCustomer != value)
                {
                    _newCustomer = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NewCustomer"));
                }
            }
        }

        private ICustomersRepository _repo = new CustomersRepository();

        public RelayCommand CreateCommand { get; private set; }

        #endregion

        public CreateCustomerViewModel()
        {
            NewCustomer = new Customer();
            CreateCommand = new RelayCommand(OnCreate, CanCreate);
        }

        #region COMMAND 

        private async void OnCreate()
        {
            await _repo.AddCustomerAsync(_newCustomer);
            _newCustomer = new Customer();
        }

        private bool CanCreate()
        {
            //return  !string.IsNullOrEmpty(_newCustomer.FirstName) 
            //   && !string.IsNullOrEmpty(_newCustomer.LastName)
            //   && !string.IsNullOrEmpty(_newCustomer.Email);
            return true;
        }

        #endregion


    }
}
