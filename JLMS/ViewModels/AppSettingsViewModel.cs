using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.ViewModels
{
    class AppSettingsViewModel : ViewModelBase
    {
        RelayCommand _setappfoldercommand;
        private string _appfolder;
        public string AppFolder { get { return _appfolder; } }
        public AppSettingsViewModel()
        {
            _appfolder = Properties.Settings.Default.JLMSFolder;
        }
        public RelayCommand SetAppFolderCommand
        {
            get
            {
                if (_setappfoldercommand == null)
                {
                    _setappfoldercommand = new RelayCommand(
                        param => this.SetAppFolder(),
                        param => this.CanSetAppFolder
                        );
                }
                return _setappfoldercommand;
            }
        }

        bool CanSetAppFolder
        {
            get { return true; }
        }
        private void SetAppFolder()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //foldername.Text = dlg.FileName;
                _appfolder = dialog.SelectedPath;
                Properties.Settings.Default.JLMSFolder = _appfolder;
                Properties.Settings.Default.Save();
                OnPropertyChanged("AppFolder");
            }

        }
    }
}
