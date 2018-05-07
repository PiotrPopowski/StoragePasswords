using StoragePasswords.Common;
using StoragePasswords.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.Models.Interfaces
{
    interface IViewModel
    {
        OpenWindowCommand OpenNewWindow { get; }
        ViewBase CurrentView { get; set; }
        void Receive(object sender, object[] arguments);
    }
}
