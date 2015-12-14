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
using DevExpress.Xpf.WindowsUI.Navigation;
using JLMS.ViewModels;
using DevExpress.Xpf.Grid;
using JLMS.Model;
namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for CMEInputView.xaml
    /// </summary>
    public partial class CMEInputView : UserControl, INavigationAware
    {
        InputViewModel _vm = new InputViewModel();
        public CMEInputView()
        {
            InitializeComponent();
            DataContext = _vm;
        }
        public void NavigatedTo(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {

            string casename = (string)e.Parameter;
            _vm.CaseName = casename;
        }
        public void NavigatingFrom(DevExpress.Xpf.WindowsUI.Navigation.NavigatingEventArgs e)
        {
            //string s = e.Parameter.ToString();
        }
        public void NavigatedFrom(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {
            // string s = e.Parameter.ToString();
        }

        private void TableView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //gridFactor.SetCellValue(e.RowHandle, "Factor", "Factor");
            //gridFactor.SetCellValue(e.RowHandle, "DailyReturn", 0);
            //gridFactor.SetCellValue(e.RowHandle, "DailySigma", 0);
        }

        private void TableView_ValidateRow(object sender, GridRowValidationEventArgs e)
        {
            //if (e.Row == null) return;
            //if (e.RowHandle == GridControl.NewItemRowHandle)
            //{
            //    e.IsValid = !string.IsNullOrEmpty(((FactorData)e.Row).Factor);
            //    e.Handled = true;
            //}
        }

        private void TableView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            //if (e.RowHandle == GridControl.NewItemRowHandle)
            //{
            //    e.ErrorText = "Please enter the factor name. ";
            //    e.WindowCaption = "Input Error";
            //}
        }
    }


}