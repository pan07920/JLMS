using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace JLMS.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public class FileInfo
        {
            public string Name { get; set; }
            public DateTime LastModified { get; set; }
            public FileInfo(string name)
            {
                Name = name;
                LastModified = DateTime.Now;
            }
        }

        ObservableCollection<FileInfo> mFileNames = new ObservableCollection<FileInfo>();

        public ObservableCollection<FileInfo> FileNames
        {
            get
            {
                return mFileNames;
            }
        }

        public MainViewModel()
        {
            Load();
        }

        private void Load()
        {
            mFileNames.Add(new FileInfo ("x"));
            mFileNames.Add(new FileInfo("y"));
            mFileNames.Add(new FileInfo("z"));
        }
    }
}
