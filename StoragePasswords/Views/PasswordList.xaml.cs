

using System;
using StoragePasswords.Common;
using StoragePasswords.ViewModels;

namespace StoragePasswords.Views
{
    /// <summary>
    /// Interaction logic for PasswordList.xaml
    /// </summary>
    public partial class PasswordList : ViewBase
    {
        public PasswordList()
        {
            InitializeComponent();
            TargetDataContext = typeof(ViewModels.PasswordListViewModel);
        }
        public override Type TargetDataContext { get; set; }
        public override void Reset()
        {
           
        }
    }
}
