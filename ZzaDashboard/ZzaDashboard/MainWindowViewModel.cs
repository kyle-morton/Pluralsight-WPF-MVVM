﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDashboard.Customers;

namespace ZzaDashboard
{
    public class MainWindowViewModel
    {
        public object CurrentViewModel { get; set; }

        public MainWindowViewModel()
        {
            CurrentViewModel = new CustomerListViewModel();
        }
    }
}
