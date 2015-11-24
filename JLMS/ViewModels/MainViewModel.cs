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
        public class CaseFile
        {
            public string CaseType { get; set; }
            public string CaseName { get; set; }
        }
        ObservableCollection<CaseFile> _casefiles = new ObservableCollection<CaseFile>();
        private void LoadCaseFiles()
        {
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 1" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 2" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 3" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 4" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 5" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 6" });
            _casefiles.Add(new CaseFile() { CaseType = "CME", CaseName = "Case 7" });
            _casefiles.Add(new CaseFile() { CaseType = "DA", CaseName = "Case DA 1" });
            _casefiles.Add(new CaseFile() { CaseType = "DA", CaseName = "Case DA 2" });
            _casefiles.Add(new CaseFile() { CaseType = "DA", CaseName = "Case DA 3" });

            
        }
                   
        public ObservableCollection<CaseFile> CaseFiles
        {
            get
            {
                return _casefiles;
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
            LoadCaseFiles();
        }

        private void Load()
        {
            mFileNames.Add(new FileInfo ("x"));
            mFileNames.Add(new FileInfo("y"));
            mFileNames.Add(new FileInfo("z"));
        }
    }
}
