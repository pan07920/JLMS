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

namespace JLMS.View
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void CopyRightContent_Loaded(object sender, RoutedEventArgs e)
        {
            CopyRight.Text = "© " +  DateTime.Now.ToString("yyyy") + " Jacobs Levy Equity Management. All rights reserved";
            CopyRightContent.Text = global::JLMS.Properties.Resources.copyright;
        }
    }
}
