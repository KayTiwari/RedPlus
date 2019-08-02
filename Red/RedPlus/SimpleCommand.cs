using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedPlus
{
    class SimpleCommand : ICommand
    {
        private Func<object, bool> canExecuteDel;
        private Action<object> executeDel;

        public SimpleCommand(Func<object, bool> canExecuteDel, Action<object> executeDel)
        {
            this.canExecuteDel = canExecuteDel;
            this.executeDel = executeDel;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuteDel(parameter);
        }

        public void Execute(object parameter)
        {
            this.executeDel(parameter);
        }
    }
}
