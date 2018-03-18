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
        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private Customer _editCustomer = null;

        public void SetCustomer(Customer customer)
        {
            _editCustomer = customer;
        }
    }
}
