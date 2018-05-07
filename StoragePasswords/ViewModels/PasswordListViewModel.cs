using StoragePasswords.Common;
using StoragePasswords.Models;
using StoragePasswords.Models.Commands;
using StoragePasswords.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.ViewModels
{
    class PasswordListViewModel: IViewModel, INotifyPropertyChanged
    {
        public OpenWindowCommand OpenNewWindow { get; }
        private Passwords accountData;
        private ViewBase currentView;
        public ViewBase CurrentView
        {
            get { return currentView; }
            set { currentView = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView")); }
        }
        private IMediator mediator;

        public event PropertyChangedEventHandler PropertyChanged;

        public PasswordListViewModel(IMediator mediator)
        {
            this.mediator = mediator;
            this.mediator.Register(this);
            OpenNewWindow = mediator.OpenNewWindow;
            CurrentView = mediator.CurrentView;
        }

        public void Receive(object sender, object[] arguments)
        {
            if (sender.GetType() == typeof(CoreViewModel) && arguments[0].GetType() == typeof(Passwords))
                accountData = arguments[0] as Passwords;
        }
    }
}
