using System.Collections.ObjectModel;
using System.ComponentModel;
using Zza.Data;
using ZzaDashboard.Logic.Services;
using ZzaDashboard.Shared;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {

        #region PROPS

        #region event handlers

        //delete {} -> now PropertyChanged wont ever be null (even if no subs)
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

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
                if (_customers != value)
                {
                    _customers = value;
                    //MAKE SURE THIS IS HERE -> notifies UI that this value has changed!!!
                    //Since no longer happening in VM constructor, must trigger this to show these!
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers")); 
                }
            }
        }

        private Customer _selectedCustomer;

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
        /// behavior setup in Blend on CustomerListView Loaded event (assigned in XAML)
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
