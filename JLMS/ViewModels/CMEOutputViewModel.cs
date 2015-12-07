using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;

namespace JLMS.ViewModels
{
    class CMEOutputViewModel : ViewModelBase
    {
        [DllImport("DataParsingDLL.dll", EntryPoint = "Process")]
        public static extern int ParsingProcessData(string SimInput, string TraceFile, string output);
        private string _jlmsimfolder = @"C:\JLMSim";

        DataTable _weightstable;
        DataTable _returnstable;
      
        private CaseSummary _selectedcase;
        private string _selectedsecurity;
        private ObservableCollection<string> _securities;
        public string SelectedCaseName
        {
            get { return _selectedcase != null ? _selectedcase.Name : ""; }

        }
      
        public CaseSummary SelectedCase
        {
            get { return _selectedcase; }
            set
            {
                _selectedcase = value;

                LoadData();
                OnPropertyChanged("SelectedCase");
            }
        }

        public string SelectedSecurity
        {
            get { return _selectedsecurity; }
            set
            {
                _selectedsecurity = value;
                OnPropertyChanged("SelectedSecurity");
            }
        }

        public ObservableCollection<string> Securities
        {
            get { return _securities; }

        }
        //protected override void OnPropertyChanged(string propertyName)
        //{
        //    if (propertyName == "SelectedCase")
        //        OnSelectedCaseChanged();
        //    else if (propertyName == "SelectedSecurity")
        //        RefreshSelectedSecurityData();

        //}
        private void OnSelectedCaseChanged()
        {
            if (_selectedcase != null)
            {
                LoadData();
            }

        }
        public CMEOutputViewModel()
        {
          
            _securities = new ObservableCollection<string>();

            _weightstable = new DataTable("WeightsTable");
            _weightstable.Columns.Add("Security", typeof(String));
            _weightstable.Columns.Add("Day", typeof(Int32));
            _weightstable.Columns.Add("Value", typeof(Double));

            _returnstable = new DataTable("ReturnsTable");
            _returnstable.Columns.Add("Security", typeof(String));
            _returnstable.Columns.Add("Month", typeof(Int32));
            _returnstable.Columns.Add("Value", typeof(Double));

        }

        public DataTable SecurityWeights
        {
            get { return _weightstable; }
        }
        public DataTable SecurityReturns
        {
            get { return _returnstable; }
        }



        private void LoadData()
        {
            UiServices.SetBusyState();
            if (_selectedcase == null)
                return;

            for (int i = 0; i < _selectedcase.TotalSecurities; i++)
            {
                _securities.Add("S" + i.ToString());
            }

           
           string prefixInput = "JLMSimInput for Case ";

           string casename = _selectedcase.Name;
           string caseinputfile = _jlmsimfolder + @"\" + prefixInput + casename + ".txt";

           string tracefile = _jlmsimfolder + @"\" + "Trace file for Case " + casename + ".txt";
           string outfile = _jlmsimfolder + @"\" + "ParsedSecurityWts.csv";

           FileInfo fio = new FileInfo(outfile);
           FileInfo fic = new FileInfo(caseinputfile);

           if (fio.LastWriteTime < fic.LastWriteTime)
           {
               int rtn = ParsingProcessData(caseinputfile, tracefile, outfile);
           }

           SamplingParsedWeightData(outfile, 20);

           string returnfile = _jlmsimfolder + @"\" + "Estimates During Case " + casename + ".csv";
           CMEReturnEstimateData(returnfile);//, 100);
              
        }

   
      
        private void SamplingParsedWeightData(string filename, int interval)
        {
            UiServices.SetBusyState();
            int _security_count = _selectedcase.TotalSecurities;
            List<string> inputlines = File.ReadAllLines(filename).ToList<string>();
            int totalline = inputlines.Count;

            int samplecount = totalline / interval;
            int[] sampleindex = new int[samplecount];
            double[,] sampledata = new double[samplecount, _security_count + 1];
            string sec = "";
            double weight = 0;
            int day = 0;
            for (int i = 0; i < samplecount; i++)
            {
                List<string> linedata = inputlines[i * interval].Split(',').ToList<string>();
                day = int.Parse(linedata[0]);
                for (int j = 0; j < _security_count + 1; j++)
                {
                    sec = "S" + j.ToString();
                    if (j == _security_count)
                        sec = "Cash";
                    weight = double.Parse(linedata[j + 1]);
                    _weightstable.Rows.Add(new object[] { sec, day, weight });
                }
            }
            OnPropertyChanged("SecurityWeights");
        }

        private void CMEReturnEstimateData(string filename)
        {
            UiServices.SetBusyState();
            //For CME cases, Return Chart, all securities, by month
            int _security_count = _selectedcase.TotalSecurities;
            List<string> inputlines = File.ReadAllLines(filename).ToList<string>();
            int totalline = inputlines.Count;

            int numberofmonth = totalline - 2;

            string sec = "";
            double returns = 0;
            int month = 0;

            for (int i = 0; i < numberofmonth; i++)
            {
                List<string> linedata = inputlines[i + 2].Split(',').ToList<string>();
                month = int.Parse(linedata[0]);
                for (int j = 0; j < _security_count; j++)
                {
                    sec = "S" + j.ToString();
                    returns = 100 * double.Parse(linedata[j + 1]);
                    _returnstable.Rows.Add(new object[] { sec, month, returns });
                }
            }

            OnPropertyChanged("SecurityReturns");
        }

    }
}
