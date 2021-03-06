﻿using StoragePasswords.Common;
using StoragePasswords.Models.Interfaces;
using StoragePasswords.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoragePasswords.Views
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : ViewBase
    {
        public LoginForm()
        {
            InitializeComponent();
            Reset();
        }

        private bool loginChanged;
        private bool passwordChanged;

        public override void Reset()
        {
            Login.Text = "Login";
            Password.Password = "******";
            loginChanged = false;
            passwordChanged = false;
        }
        public void ClearLogin(object sender, EventArgs e)
        {
            if (!loginChanged)
            {
                Login.Text = "";
                loginChanged = true;
            }
        }
        public void ClearPassword(object sender, EventArgs e)
        {
            if (!passwordChanged)
            {
                Password.Password = "";
                passwordChanged = true;
            }
        }
    }
}
