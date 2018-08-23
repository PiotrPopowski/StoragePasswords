using StoragePasswords.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoragePasswords.Models.Commands;
using StoragePasswords.Models.Interfaces;
using System.ComponentModel;
using StoragePasswords.Common;
using StoragePasswords.Models;

namespace StoragePasswords.ViewModels
{
    class CoreViewModel : IViewModel, INotifyPropertyChanged
    {
        public CoreViewModel()
        {
            PswFileName = "Passwords";
            OpenViews = new ObservableCollection<ViewBase>();

            OpenNewWindow = new OpenWindowCommand(SetView);
            CreateAccount = new CreateAccountCommand(AccountWizard);
            Login = new LoginCommand(LoginToAccount);

            passwordManager = new PasswordProvider();
            mediator = new ConcreteMediator(OpenNewWindow);
            mediator.Register(this);
        }
        public CoreViewModel(Type initialView) : this()
        {
            SetView(initialView);
        }

        private ConcreteMediator mediator;
        private PasswordProvider passwordManager;
        public string PswFileName { get; set; }
        public ObservableCollection<ViewBase> OpenViews { get; set; }
        private ViewBase _CurrentView;
        public ViewBase CurrentView
        {
            get { return _CurrentView; }
            set { _CurrentView = value; RaiseChangedEvent(this, "CurrentView"); }
        }

        public OpenWindowCommand OpenNewWindow { get; private set; }
        public CreateAccountCommand CreateAccount { get; private set; }
        public LoginCommand Login { get; private set; }
        
        public PasswordListViewModel SecretContentViewModel {get;private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseChangedEvent(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            mediator.ChangeCurrentView(CurrentView,this);
        }


        public void SetView(Type view)
        {
            
            foreach (ViewBase openView in OpenViews)
                if (openView.GetType().Equals(view))
                {
                    CurrentView = openView;
                    CurrentView.Reset();
                    return;
                }

            CurrentView = Activator.CreateInstance(view) as ViewBase;
            if (CurrentView.TargetDataContext == null)
                CurrentView.DataContext = this;
            else
            {
                var context = Activator.CreateInstance(CurrentView.TargetDataContext,new object[] { mediator});
                var type = CurrentView.TargetDataContext;
                CurrentView.DataContext = Convert.ChangeType(context, type);
            }
            OpenViews.Add(CurrentView);
        }
        public void CreateEncryptedFile(string login, string password)
        {
            passwordManager = new PasswordProvider(
                new Passwords(login,password,
                        Encoding.UTF8.GetString(CryptographyService.GenerateKey()),
                        Encoding.UTF8.GetString(CryptographyService.GenerateKey())));
            passwordManager.Encrypt();
            passwordManager.Save(PswFileName);
        }

        public void AccountWizard(string login, string password, string confirmedPassword)
        {
            if (!password.Equals(confirmedPassword))
            {
                System.Windows.MessageBox.Show("Passwords do not match - please re-enter");
                CurrentView.Reset();
                return;
            }
            if(login.Length<8 || password.Length<8)
            {
                System.Windows.MessageBox.Show("Password and login must be between 8-32 characters");
                return;
            }
            CreateEncryptedFile(login, password);
            CurrentView.Reset();
            System.Windows.MessageBox.Show("Account created");

        }

        public void LoginToAccount(string login, string password)
        {
            if(login.Length<8 || password.Length<8)
            {
                System.Windows.MessageBox.Show("Incorrect login or password");
                return;
            }
            if (passwordManager.Passwords == null)
                passwordManager.Load(PswFileName);
            if(passwordManager.VerifyPassword(password,login))
            {
                passwordManager.Decrypt(password, login);
                SetView(typeof(PasswordList));
                mediator.SendTo(this, typeof(PasswordListViewModel), new object[] { passwordManager.Passwords });
            }
            else System.Windows.MessageBox.Show("Incorrect login or password");
        }

        public void SaveAndEncrypt()
        {
            if (passwordManager.Passwords != null)
            {
                passwordManager.Encrypt();
                passwordManager.Save(PswFileName);
            }
        }

        public void Receive(object sender, object[] argument)
        {
            throw new NotImplementedException();
        }
    }
}
