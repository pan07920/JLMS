using DevExpress.Xpf.Core;
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
            
            themelist.Items.Add("Office2016White");
            themelist.Items.Add("Office2016Black");
            themelist.Items.Add("Office2016Colorful");
            themelist.Items.Add("Office2013");
            themelist.Items.Add("Office2013DarkGray");
            themelist.Items.Add("Office2013LightGray");
            themelist.Items.Add("Office2010Black");
            themelist.Items.Add("Office2010Blue");
            themelist.Items.Add("Office2010Silver");
            themelist.Items.Add("MetropolisLight");
            themelist.Items.Add("MetropolisDark");
            themelist.Items.Add("VS2010");
            themelist.Items.Add("Office2007Black");
            themelist.Items.Add("Office2007Blue");
            themelist.Items.Add("Office2007Silver");
            themelist.Items.Add("Seven");
            themelist.Items.Add("DeepBlue");
            themelist.Items.Add("LightGray");
        }
    }
}
