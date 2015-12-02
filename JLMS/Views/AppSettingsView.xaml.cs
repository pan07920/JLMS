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


namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for AppSettingsView.xaml
    /// </summary>
    public partial class AppSettingsView : UserControl
    {
        private string jlmsfolder;
        public AppSettingsView()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            
            if( result == System.Windows.Forms.DialogResult.OK)
            {
                //foldername.Text = dlg.FileName;
                foldername.Text = dialog.SelectedPath;
                Properties.Settings.Default.JLMSFolder = foldername.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            jlmsfolder = Properties.Settings.Default.JLMSFolder;
            foldername.Text = jlmsfolder;
           
        }
    }
}
