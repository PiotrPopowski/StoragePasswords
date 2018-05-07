using System;

namespace StoragePasswords.Views
{
    /// <summary>
    /// Interaction logic for AccWizard.xaml
    /// </summary>
    public partial class AccWizard: StoragePasswords.Common.ViewBase
    {
        public AccWizard()
        {
            InitializeComponent();
        }

        public override void Reset()
        {
            Password.Password = "";
            Login.Text = "";
            ConfirmedPassword.Password = "";
        }

    }
}
