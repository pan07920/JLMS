using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.NavBar;


namespace JLMS
{
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new JLMS.ViewModels.MainViewModel();
        }

    }

    public class NavBarGroupEx : NavBarGroup
    {
        public object CurrentItem
        {
            get { return (object)GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty CurrentItemProperty =
            DependencyProperty.Register("CurrentItem", typeof(object), typeof(NavBarGroupEx), new PropertyMetadata(null, new PropertyChangedCallback((d, e) => {
                var instance = (NavBarGroupEx)d;
                if (e.NewValue == null)
                    instance.SetCurrentValue(SelectedItemProperty, null);
                else
                {
                    var item = instance.Items.FirstOrDefault(i => ((NavBarItem)i).DataContext == e.NewValue);
                    instance.SetCurrentValue(SelectedItemProperty, item);
                }
            })));

        protected override void OnSelectedItemChanged(NavBarItem oldItem, NavBarItem newItem)
        {
            base.OnSelectedItemChanged(oldItem, newItem);
            SetCurrentValue(CurrentItemProperty, newItem != null ? newItem.DataContext : null);
        }
    }
}
