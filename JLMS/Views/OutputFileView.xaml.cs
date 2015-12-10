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
using DevExpress.Spreadsheet;
using System.IO;

namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for OutputFileView.xaml
    /// </summary>
    public partial class OutputFileView : UserControl
    {
        public OutputFileView()
        {
            InitializeComponent();
        }



        private void txtboxfileselect_TextChanged(object sender, TextChangedEventArgs e)
        {
            UiServices.SetBusyState();

            string filename = ((TextBox)sender).Text;
            if (System.IO.File.Exists(filename))
            {
                if (checkboxopenextern.IsChecked == true)
                {
                    System.Diagnostics.Process.Start(filename);
                }
                else
                { 
                // outputFileSheet.LoadDocument(filename);

                IWorkbook workbook = outputFileSheet.Document;


                // Load a workbook from a stream. 
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    if (filename.ToUpper().Contains(".TXT"))
                        workbook.LoadDocument(stream, DocumentFormat.Text);
                    else if (filename.ToUpper().Contains(".CSV"))
                        workbook.LoadDocument(stream, DocumentFormat.Csv);
                }
            }
            }
        }
    }
}
