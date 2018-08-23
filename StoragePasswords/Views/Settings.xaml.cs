﻿using StoragePasswords.Common;
using StoragePasswords.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace StoragePasswords.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Settings.xaml
    /// </summary>
    public partial class Settings : PopupWindow
    {
        public Settings()
        {
            InitializeComponent();
            TargetDataContext = typeof(SettingsViewModel);
        }
        public override Type TargetDataContext { get => base.TargetDataContext; set => base.TargetDataContext = value; }
    }
}
