﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.CoreViewModel _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new ViewModels.CoreViewModel(typeof(LoginForm));
            DataContext = _context;
            this.Closing += SaveCurrentState;
        }

        private void SaveCurrentState(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _context.SaveAndEncrypt();
        }
    }
}
