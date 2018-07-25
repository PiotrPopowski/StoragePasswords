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

        private void OpenSettings_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var uriSource = new Uri("C:/Users/ppopo/Downloads/pretool.png", UriKind.Absolute);
            OpenSettings.Content = new System.Windows.Media.Imaging.BitmapImage(uriSource);
        }

        private void OpenSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var uriSource = new Uri("C:/Users/ppopo/Downloads/tools.png", UriKind.Absolute);
            OpenSettings.Content = new System.Windows.Media.Imaging.BitmapImage(uriSource);

        }
    }
}
