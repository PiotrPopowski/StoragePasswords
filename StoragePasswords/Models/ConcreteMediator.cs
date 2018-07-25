using StoragePasswords.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoragePasswords.Common;
using StoragePasswords.Models.Commands;

namespace StoragePasswords.Models
{
    class ConcreteMediator : IMediator
    {
        public ConcreteMediator(OpenWindowCommand openWindowCommand, OpenWindowCommand showPopup)
        {
            ViewModelList = new List<IViewModel>();
            OpenNewWindow = openWindowCommand;
            ShowPopup = showPopup;
        }
        private ViewBase _currentView;
        public ViewBase CurrentView { get { return _currentView; } set { _currentView = value; } }
        public OpenWindowCommand OpenNewWindow { get; private set; }
        public OpenWindowCommand ShowPopup { get; private set; }
        private List<IViewModel> _viewModelList= new List<IViewModel>();
        public List<IViewModel> ViewModelList
        {
            get; private set;
        }

        public void Register(IViewModel viewModel)
        {
            ViewModelList.Add(viewModel);
        }
        public void ChangeCurrentView(ViewBase newView,IViewModel sender)
        {
            CurrentView = newView;
            UpdateCurrentView(sender);
        }
        public void UpdateCurrentView(IViewModel sender)
        {
            foreach (IViewModel vm in ViewModelList)
                if(!vm.Equals(sender))
                    vm.CurrentView = CurrentView;
        }

        public void SendTo(object from, Type to, object[] data)
        {
            foreach(IViewModel vm in ViewModelList)
            {
                if (vm.GetType() == to )
                    vm.Receive(from, data);
            }
        }
        public void SendTo(object from, IViewModel to, object[] data)
        {
            foreach (IViewModel vm in ViewModelList)
            {
                if (vm.Equals(to))
                    vm.Receive(from, data);
            }
        }
    }
}
