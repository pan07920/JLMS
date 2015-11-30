﻿using DevExpress.Xpf.WindowsUI.Navigation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JLMS.ViewModels;
namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for OutputView.xaml
    /// </summary>
    public partial class CMAOutputView : UserControl, INavigationAware
    {
        OutputViewModel _outputviewmodel = new OutputViewModel();
        public CMAOutputView()
        {
            InitializeComponent();
            DataContext = _outputviewmodel;
        }
        public void NavigatedTo(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {
            _outputviewmodel.SelectedCase = (CaseSummary)e.Parameter;
            //var dataContext = DataContext as OutputViewModel;
            //dataContext.SelectedCase = (CaseSummary) e.Parameter;

        }
        public void NavigatingFrom(DevExpress.Xpf.WindowsUI.Navigation.NavigatingEventArgs e)
        {
            //string s = e.Parameter.ToString();
        }
        public void NavigatedFrom(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {
           // string s = e.Parameter.ToString();
        }
    }
}
