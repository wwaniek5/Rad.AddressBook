﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAD.AddressBook.Views
{
    /// <summary>
    /// Interaction logic for AddressBookView.xaml
    /// </summary>
    public partial class AddressBookView : UserControl
    {
        public AddressBookView()
        {
            InitializeComponent();
            Application.Current.MainWindow.Height = 700;
            Application.Current.MainWindow.Width = 1000;
        }
    }
}
