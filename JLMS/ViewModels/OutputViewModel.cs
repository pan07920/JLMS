using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace JLMS.ViewModels
{
    class OutputViewModel : ViewModelBase
    {
        private CaseSummary _selectedcase;
        public CaseSummary SelectedCase
        {
            get { return _selectedcase; }
            set
            {
                _selectedcase = value;
                OnPropertyChanged("SelectedCase");
                OnPropertyChanged("SelectedCaseSummary");
            }
        }
        public ObservableCollection<KeyValuePair<string, string>> SelectedCaseSummary
        {
            get { return _selectedcase == null ? null : _selectedcase.Summary; }

        }
    }
}
