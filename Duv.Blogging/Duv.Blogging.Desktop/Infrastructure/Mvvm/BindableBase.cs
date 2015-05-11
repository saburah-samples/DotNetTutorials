using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Infrastructure.Mvvm
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetPropertyAndNotify<T>(ref T currentValue, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (Equals(currentValue, newValue))
            {
                return false;
            }

            //RaisePropertyChanging(propertyName);
            currentValue = newValue;
            RaisePropertyChanged(propertyName);

            return true;
        }

    }
}
