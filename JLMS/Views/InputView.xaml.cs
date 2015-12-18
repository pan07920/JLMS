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
using System.Collections.ObjectModel;
namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for CMEInputView.xaml
    /// </summary>
    public partial class InputView : UserControl, INavigationAware
    {
        InputViewModel _vm = new InputViewModel();
        public InputView()
        {
            InitializeComponent();
            DataContext = _vm;
        }
        public void NavigatedTo(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(CaseSummary))
            {
                CaseSummary cs = (CaseSummary)e.Parameter;
                _vm.CaseName = cs.Name;
                _vm.CMECase = cs.MTOperationMode;
                _vm.NewCase = false;
            }
            else
            {
                string casename = (string)e.Parameter;
                if (casename == "CME:New")
                {
                    _vm.CMECase = true;
                    _vm.NewCase = true;
                }
                else if (casename == "DA:New")
                {
                    _vm.CMECase = false;
                    _vm.NewCase = true;
                }
                
            }
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

        private void gridControlCovMatrix_CustomUnboundColumnData_1(object sender, GridColumnDataEventArgs e)
        {
            GridColumn col = e.Column;
            CovMatrix m = gridControlCovMatrix.GetRow(e.ListSourceRowIndex) as CovMatrix;
            int colNum = int.Parse(col.FieldName);
            e.Value = m[colNum];
        }

        private void gridControlCovMatrix_ItemsSourceChanged_1(object sender, ItemsSourceChangedEventArgs e)
        {
            if ((sender as GridControl).ItemsSource is ObservableCollection<CovMatrix> )
            {
                ObservableCollection<CovMatrix> it = (sender as GridControl).ItemsSource as ObservableCollection<CovMatrix>;
                int colCount = it[0].GetColumnsColunt();
                for (int i = 0; i < colCount; i++)
                {
                    GridColumn col = new GridColumn();
                    col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                    col.FieldName = i.ToString();
                    col.Header = "C-" + i.ToString();
                    gridControlCovMatrix.Columns.Add(col);
                }
            }

        }

        private void tableviewCovMatrix_CellValueChanged_1(object sender, CellValueChangedEventArgs e)
        {
            CovMatrix md = e.Row as CovMatrix;
            GridColumn col = e.Column;
            int colNum = int.Parse(col.FieldName);

            var v = (sender as TableView).ActiveEditor.EditValue;
            if (md[colNum] == null)
                md[colNum] = null;
            else
                md[colNum] = double.Parse(v.ToString());
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }


}