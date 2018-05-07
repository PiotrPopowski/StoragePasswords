using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoragePasswords.Models.Commands
{
    class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string,string> _execute;
        public LoginCommand(Action<string,string> execute)
        {
            _execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            string login = values[0] as string;
            string password = (values[1] as PasswordBox).Password;
            _execute.Invoke(login, password);
        }
    }
}
