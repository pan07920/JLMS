using System;
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
using DevExpress.Xpf.WindowsUI.Navigation;

namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, INavigationAware
    {
        public MainView()
        {
            InitializeComponent();
           // DataContext = new JLMS.ViewModels.MainViewModel();
        }
        public void NavigatedTo(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {
            string s;
            if (e.Parameter!=null)
                s = e.Parameter.ToString();
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
