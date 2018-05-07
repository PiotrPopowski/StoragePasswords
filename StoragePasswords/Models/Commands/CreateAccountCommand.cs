using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoragePasswords.Models.Commands
{
    class CreateAccountCommand : ICommand
    {
        private Action<string,string,string> _execute;
        public CreateAccountCommand(Action<string,string,string> execute)
        {
            _execute = execute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            string login = values[0] as string;
            string password = ((PasswordBox)values[1]).Password;
            string confirmedPassword = ((PasswordBox)values[2]).Password;
            _execute.Invoke(login, password, confirmedPassword);
        }
    }
}
