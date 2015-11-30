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
    class OutputViewModel : ViewModelBase
    {
        [DllImport("DataParsingDLL.dll", EntryPoint = "Process")]
        public static extern int ParsingProcessData(string SimInput, string TraceFile, string output);
        private string _jlmsimfolder = @"C:\JLMSim";
        int _perdays;
        double[,] _price;
        int[,] _volume;
        double[,] _weightmarket;
        int[] _totalvolume;

        DataTable _weightstable;
        DataTable _returnstable;
        DataTable _priceindicestable;
        DataTable _volumeindicestable;
        DataTable _securitypricetable;
        DataTable _securityvolumetable;

        private CaseSummary _selectedcase;
        private string _security;
        private ObservableCollection<string> _securities;
        public string SelectedCaseName
        {
            get { return _selectedcase != null ? _selectedcase.Name : ""; }

        }
        public int PerDays
        {
            get { return _perdays; }
            set
            {
                _perdays = value;
                RefreshIndicesData();
                RefreshSecuritiesData();
                OnPropertyChanged("PerDays");
            }
        }
        public CaseSummary SelectedCase
        {
            get { return _selectedcase; }
            set
            {
                _selectedcase = value;

                if (_selectedcase != null)
                {
                    LoadData();
                }
                OnPropertyChanged("SelectedCase");
                OnPropertyChanged("SelectedCaseSummary");
                OnPropertyChanged("SecurityWeights");
                OnPropertyChanged("SecurityReturns");
                
                OnPropertyChanged("SecurityPrice");
                OnPropertyChanged("SecurityVolume");
                OnPropertyChanged("PriceIndices");
                OnPropertyChanged("VolumeIndices");
            }
        }
        public ObservableCollection<KeyValuePair<string, string>> SelectedCaseSummary
        {
            get { return _selectedcase == null ? null : _selectedcase.Summary; }

        }
        public string SelectedSecurity
        {
            get { return _security; }
            set
            {
                _security = value;
            }
        }

        public ObservableCollection<string> Securities
        {
            get { return _securities; }

        }
        public OutputViewModel()
        {
            _perdays = 40;
            _security = "S1";
            _securities = new ObservableCollection<string>();

            _weightstable = new DataTable("WeightsTable");
            _weightstable.Columns.Add("Security", typeof(String));
            _weightstable.Columns.Add("Day", typeof(Int32));
            _weightstable.Columns.Add("Value", typeof(Double));

            _returnstable = new DataTable("ReturnsTable");
            _returnstable.Columns.Add("Security", typeof(String));
            _returnstable.Columns.Add("Month", typeof(Int32));
            _returnstable.Columns.Add("Value", typeof(Double));

            _priceindicestable = new DataTable("PriceIndicesTable");
            _priceindicestable.Columns.Add("WeightedType", typeof(String));
            _priceindicestable.Columns.Add("Day", typeof(Int32));
            _priceindicestable.Columns.Add("Value", typeof(Double));

            _volumeindicestable = new DataTable("VolumeIndicesTable");
            _volumeindicestable.Columns.Add("Day", typeof(Int32));
            _volumeindicestable.Columns.Add("Value", typeof(Int32));

            _securitypricetable = new DataTable("SecurityPriceTable");
            _securitypricetable.Columns.Add("Security", typeof(String));
            _securitypricetable.Columns.Add("Day", typeof(Int32));
            _securitypricetable.Columns.Add("OPEN", typeof(Double));
            _securitypricetable.Columns.Add("HIGH", typeof(Double));
            _securitypricetable.Columns.Add("LOW", typeof(Double));
            _securitypricetable.Columns.Add("CLOSE", typeof(Double));
            _securitypricetable.Columns.Add("Value", typeof(Double));

            _securityvolumetable = new DataTable("SecurityVolumeTable");
            _securityvolumetable.Columns.Add("Security", typeof(String));
            _securityvolumetable.Columns.Add("Day", typeof(Int32));
            _securityvolumetable.Columns.Add("Value", typeof(Int32));

        }
     
        public DataTable SecurityWeights
        {
            get { return _weightstable; }
        }
        public DataTable SecurityReturns
        {
            get { return _returnstable; }
        }

        public DataTable PriceIndices
        {
            get { return _priceindicestable; }
        }
        public DataTable VolumeIndices
        {
            get { return _volumeindicestable; }
        }
        public DataTable SecurityPrice
        {
            get { return _securitypricetable; }
        }
        public DataTable SecurityVolume
        {
            get { return _securityvolumetable; }
        }


        private void LoadData()
        {
            if (_selectedcase == null)
                return;

            for (int i = 0; i<_selectedcase.TotalSecurities; i++)
            {
                _securities.Add("S" + i.ToString());
            }
            if (!_selectedcase.MTOperationMode)
            {
                LoadDailyDataFile();
                RefreshIndicesData();
                RefreshSecuritiesData();
            }
            else
            {
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
        }

        private void RefreshSecuritiesData()
        {
            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;
            _securitypricetable.Clear();

            int mprv;
            double prcValue;
            int volValue;
            double op = 0;
            double lo = 0;
            double hi = 0;
            double cl = 0;

            for (int nSecurityPosition = 0; nSecurityPosition < security_count; nSecurityPosition++)
            {
                _securitypricetable.Rows.Add(nSecurityPosition.ToString(),  1, _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition]);
                _securityvolumetable.Rows.Add(nSecurityPosition.ToString(), 1, _volume[0, nSecurityPosition]);
            }

                for (int nSecurityPosition = 0; nSecurityPosition < security_count; nSecurityPosition++)
            {
                mprv = 0;
                double P = 0;
                int datapoints = (int)Math.Floor((double)total_day_count / PerDays);
                for (int i = 1; i <= datapoints; i++)
                {
                    int k = PerDays * i-1;
                    prcValue = _price[k, nSecurityPosition];
                    volValue = 0;

                    op = _price[mprv, nSecurityPosition];
                    lo = 1000000;
                    hi = -1000000;

                    for (int m = mprv; m <= k; m++)
                    {
                        volValue = volValue + _volume[m, nSecurityPosition];
                        P = _price[m, nSecurityPosition];
                        if (P > hi)
                            hi = P;

                        if (P < lo)
                            lo = P;
                    }
                    cl = P;
                    _securitypricetable.Rows.Add(nSecurityPosition.ToString(), k+ 1, op, hi, lo, cl, prcValue);
                    _securityvolumetable.Rows.Add(nSecurityPosition.ToString(), k + 1, volValue);
                    mprv = k + 1;
                }
            }
        }
        private void RefreshIndicesData()
        {
            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;
            _priceindicestable.Clear();
            _volumeindicestable.Clear();
            string eqwt = "Equally Weighted";
            string capwt = "Capitalization Weighted";
            int datapoints = (int) Math.Floor( (double)total_day_count / PerDays);
            int mprv = 0;
            double volumeValue;
            _priceindicestable.Rows.Add(eqwt,  1, _weightmarket[0, 0]);
            _priceindicestable.Rows.Add(capwt, 1, _weightmarket[0, 1]);
            for (int i = 1; i <= datapoints; i++)
            {
                int k = PerDays * i-1;
                _priceindicestable.Rows.Add(eqwt, k+1, _weightmarket[k, 0]);
                _priceindicestable.Rows.Add(capwt, k+1, _weightmarket[k, 1]);
                volumeValue = 0;
                for (int m = mprv; m <= k; m++)
                {
                    volumeValue = volumeValue + _totalvolume[m];
                }
                mprv = k+1;
                _volumeindicestable.Rows.Add(k+1, volumeValue);
            }
        }

        private void LoadDailyDataFile()
        {
            string prefixDailyReport = "Daily Reports for Case ";
            string casename = _selectedcase.Name; 
            string dailyreportfile = _jlmsimfolder + @"\" + prefixDailyReport + casename + ".csv";
            List<string> inputlines = File.ReadAllLines(dailyreportfile).ToList<string>();

            for (int i = 0; i < 4; i++)
                inputlines.RemoveAt(0);

            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;
            _price = new double[total_day_count, security_count];
            _volume = new int[total_day_count, security_count];
            _weightmarket = new double[total_day_count, 2];
            _totalvolume = new int[total_day_count];
            int row = 0;

            foreach (string line in inputlines)
            {
                string workingline = line.Replace("1.#J", "0");
                workingline = line.Replace("1.#IO", "0");
                List<string> linedata = line.Split(',').ToList<string>();
                int day = int.Parse(linedata[0]);

                for (int i = 1; i <= security_count; i++)
                {
                    
                    _price[row, i - 1] = double.Parse(linedata[i]);
                    _volume[row, i - 1] = int.Parse(linedata[i + 2 + security_count]);
                    //_securitypricetable.Rows.Add("S" + i.ToString(), double.Parse(linedata[i]));
                    //_securityvolumetable.Rows.Add("S" + i.ToString(), int.Parse(linedata[i + 2 + security_count]));
                }
               
                _weightmarket[row, 0] = double.Parse(linedata[security_count + 1]);
                _weightmarket[row, 1] = double.Parse(linedata[security_count + 2]);
                //_priceindicestable.Rows.Add(eqwt, row, double.Parse(linedata[security_count + 1]));
                //_priceindicestable.Rows.Add(capwt, row, double.Parse(linedata[security_count + 2]));

                _totalvolume[row] = int.Parse(linedata[3 + 2 * security_count]);
                //_volumeindicestable.Rows.Add(row, int.Parse(linedata[3 + 2 * security_count]));
                row++;
            }

        }
        private void SamplingParsedWeightData(string filename, int interval)
        {
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
                List <string> linedata = inputlines[i * interval].Split(',').ToList<string>();
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
        }

        private void CMEReturnEstimateData(string filename)
        {
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
                for (int j = 0; j < _security_count ; j++)
                {
                    sec = "S" + j.ToString();
                    returns = 100 * double.Parse(linedata[j + 1]);
                    _returnstable.Rows.Add(new object[] { sec, month, returns });
                }
            }
        }

    }
}
