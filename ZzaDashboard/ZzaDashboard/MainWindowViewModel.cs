using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZzaDashboard.Customers;

namespace ZzaDashboard
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        #region PROPS

        #region event handlers

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        private Timer _timer = new Timer(5000);
        public object CurrentViewModel { get; set; }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get
            {
                return _notificationMessage;
            }
            set
            {
                if (_notificationMessage != value)
                {
                    _notificationMessage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
                }
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            CurrentViewModel = new CustomerListViewModel();
            _timer.Elapsed += (s, e) =>
            {
                NotificationMessage = "At the tone, the time will be: " + DateTime.Now.ToLocalTime().ToString();
            };
            _timer.Start();
        }

    }
}
