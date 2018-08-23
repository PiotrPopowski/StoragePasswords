using StoragePasswords.Common;
using StoragePasswords.Models;
using StoragePasswords.Models.Commands;
using StoragePasswords.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StoragePasswords.ViewModels
{
    class PasswordListViewModel: IViewModel, INotifyPropertyChanged
    {
        public OpenWindowCommand OpenNewWindow { get; }
        public RemovePasswordCommand RemoveCommand { get; }
        public OpenWindowCommand ShowPopup { get; }

        private Passwords _accountData;
        private Passwords accountData
        {
            get { return _accountData; }
            set
            {
                _accountData = value;
                Data = _accountData.Collection;
            }
        }
        private StoragePasswords.Common.ViewBase currentView;
        public Common.ViewBase CurrentView
        {
            get { return currentView; }
            set { currentView = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView")); }
        }
        private IMediator mediator;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Data { get; private set; }
        public AddPasswordCommand Add { get; private set; }

        public PasswordListViewModel(IMediator mediator)
        {
            Add = new AddPasswordCommand(AddPassword);
            RemoveCommand = new RemovePasswordCommand(RemovePassword);
            this.mediator = mediator;
            this.mediator.Register(this);
            OpenNewWindow = mediator.OpenNewWindow;
            ShowPopup = new OpenWindowCommand(ShowSettings);
            CurrentView = mediator.CurrentView;
        }

        public void AddPassword(string password)
        {
            Data.Add(password);
        }
        public void RemovePassword(string lv)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to permanently remove the password?", "Confirmation", MessageBoxButton.YesNo,MessageBoxImage.Warning,MessageBoxResult.No,MessageBoxOptions.DefaultDesktopOnly);
            if(result==MessageBoxResult.Yes)
                Data.Remove(lv);
        }

        public void ShowSettings(Type popup)
        {
            var window = Activator.CreateInstance(popup) as PopupWindow;
            window.DataContext = new SettingsViewModel(accountData);
            window.ShowDialog();
        }

        public void Receive(object sender, object[] arguments)
        {
            if (sender.GetType() == typeof(CoreViewModel) && arguments[0].GetType() == typeof(Passwords))
                accountData = arguments[0] as Passwords;
        }
    }
}
