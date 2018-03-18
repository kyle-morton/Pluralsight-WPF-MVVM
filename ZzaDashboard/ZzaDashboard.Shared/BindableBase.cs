using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZzaDashboard.Shared
{
    public class BindableBase : INotifyPropertyChanged
    {

        /// <summary>
        /// triggers on property changed event for property if value has changed. 
        /// Uses C# CallerMemberName to get property name in the setter for which method was called in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="val"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {

            //checks if member variable and new value are same
            if (object.Equals(member, val)) return;

            //otherwise, sets new value and triggers prop changed event
            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// used to invoke PropertyChanged event for property w/ given name
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
