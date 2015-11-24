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

namespace JLMS.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
           // DataContext = new JLMS.ViewModels.MainViewModel();
        }
        //public class FileInfo
        //{
        //    public string Name { get; set; }
        //    public DateTime LastModified { get; set; }
        //    public FileInfo(string name)
        //    {
        //        Name = name;
        //        LastModified = DateTime.Now;
        //    }
        //}

        //ObservableCollection<FileInfo> mFileNames = new ObservableCollection<FileInfo>();

        //public ObservableCollection<FileInfo> FileNames
        //{
        //    get
        //    {
        //        return mFileNames;
        //    }
        //}

        ////public MainViewModel()
        ////{
        ////    Load();
        ////}

        //private void Load()
        //{
        //    mFileNames.Add(new FileInfo("x"));
        //    mFileNames.Add(new FileInfo("y"));
        //    mFileNames.Add(new FileInfo("z"));
        //}
    }
}
