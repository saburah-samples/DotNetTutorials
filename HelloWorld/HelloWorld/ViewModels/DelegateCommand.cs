using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelloWorld.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private Predicate<object> canExecute;
        private Action<object> execute;

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            Debug.Assert(execute != null, "Argument 'execute' is null");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
