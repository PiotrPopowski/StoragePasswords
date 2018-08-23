using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoragePasswords.Models.Commands
{
    class ChangeMainPasswordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string, string, string> _execute;

        public ChangeMainPasswordCommand(Action<string, string, string> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var parameters = (object[])parameter;
            var currentPassword = (parameters[0] as PasswordBox).Password;
            var newPassword = (parameters[1] as PasswordBox).Password;
            var repeatedPassword = (parameters[2] as PasswordBox).Password;
            _execute.Invoke(currentPassword, newPassword, repeatedPassword);
        }
    }
}
