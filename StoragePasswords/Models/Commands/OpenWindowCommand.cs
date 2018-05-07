using StoragePasswords.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoragePasswords.Models.Commands
{
    class OpenWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<Type> _execute;

        public OpenWindowCommand(Action<Type> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as Type);
            
        }
    }
}
