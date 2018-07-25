using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoragePasswords.Models.Commands;
using StoragePasswords.Common;
namespace StoragePasswords.Models.Interfaces
{
    interface IMediator
    {
        List<IViewModel> ViewModelList { get;}
        ViewBase CurrentView { get; }
        OpenWindowCommand OpenNewWindow { get; }
        OpenWindowCommand ShowPopup { get; }
        void UpdateCurrentView(IViewModel sender);
        void Register(IViewModel viewModel);
        void SendTo(object from, IViewModel to, object[] data);
    }
}
