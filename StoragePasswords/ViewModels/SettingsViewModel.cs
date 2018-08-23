using StoragePasswords.Common;
using StoragePasswords.Models;
using StoragePasswords.Models.Commands;
using StoragePasswords.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoragePasswords.ViewModels
{
    class SettingsViewModel
    {
        private PasswordProvider passwordManager;

        public SettingsViewModel(Passwords accountData)
        {
            ChangeMainPassword = new ChangeMainPasswordCommand(ChangePassword);
            passwordManager = new PasswordProvider(accountData);
        }

        public ChangeMainPasswordCommand ChangeMainPassword { get; }
        public void ChangePassword(string oldPassword, string newPassword, string repeatedPassword)
        {
            if(newPassword != repeatedPassword)
            {
                MessageBox.Show("Passwords don't match.", "Warring", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (passwordManager.Passwords.MainPassword == oldPassword &&
                newPassword.Length > 7)
            {
                passwordManager.ChangePassword(newPassword);
            }
            else
            {
                MessageBox.Show("Incorrect password.", "Warring", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void ChangeLogin(string currentLogin, string newLogin)
        {
            if(currentLogin != newLogin)
            {
                MessageBox.Show("Incorrect login.", "Warring", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            if (newLogin.Length >= 3)
            {
                MessageBox.Show("Login is too short(must contains at least 3 characters).", "Warring", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
