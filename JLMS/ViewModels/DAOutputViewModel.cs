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
using DevExpress.Xpf.Core;
using JLMS.Model;

namespace JLMS.ViewModels
{
    class DAOutputViewModel : ViewModelBase
    {
       
        private string _jlmsimfolder = @"C:\JLMSim";
        int _perdays;
        double[,] _price;
        int[,] _volume;
        double[,] _weightmarket;
        int[] _totalvolume;


        DataTable _priceindicestable;
        DataTable _volumeindicestable;
        DataTable _securitypricetable;
        DataTable _securityvolumetable;

        DataTable _selectedsecuritypricetable;
        DataTable _selectedsecurityvolumetable;


        private CaseSummary _selectedcase;
        private string _selectedsecurity;
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
                _perdays = (int)_selectedcase.SimulationLength / 100;
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
                RefreshSelectedSecurityData();
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
                _perdays = (int)_selectedcase.SimulationLength / 100;
                LoadData();
            }
            
        }
        public DAOutputViewModel()
        {
            _perdays = 10;
            _selectedsecurity = "S1";
            _securities = new ObservableCollection<string>();

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


            _selectedsecuritypricetable = new DataTable("SecurityPriceTable");
            _selectedsecuritypricetable.Columns.Add("Security", typeof(String));
            _selectedsecuritypricetable.Columns.Add("Day", typeof(Int32));
            _selectedsecuritypricetable.Columns.Add("OPEN", typeof(Double));
            _selectedsecuritypricetable.Columns.Add("HIGH", typeof(Double));
            _selectedsecuritypricetable.Columns.Add("LOW", typeof(Double));
            _selectedsecuritypricetable.Columns.Add("CLOSE", typeof(Double));
            _selectedsecuritypricetable.Columns.Add("Value", typeof(Double));

            _selectedsecurityvolumetable = new DataTable("SecurityVolumeTable");
            _selectedsecurityvolumetable.Columns.Add("Security", typeof(String));
            _selectedsecurityvolumetable.Columns.Add("Day", typeof(Int32));
            _selectedsecurityvolumetable.Columns.Add("Value", typeof(Int32));

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

        //public DataTable SelectedSecurityPrice
        //{
        //    get { return _selectedsecuritypricetable; }
        //}

        public DataView SelectedSecurityPrice
        {
            get { return _selectedsecuritypricetable.DefaultView; }
        }
        public DataView SelectedSecurityVolume
        {
            get { return _selectedsecurityvolumetable.DefaultView;  }
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
                RefreshSelectedSecurityData();
            }
           
        }

        private void RefreshSelectedSecurityData()
        {
            UiServices.SetBusyState();
            
            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;

            int nSecurityPosition = 0;
            int.TryParse(_selectedsecurity.Replace("S", ""), out nSecurityPosition);

            _selectedsecuritypricetable.Rows.Clear();
            _selectedsecurityvolumetable.Rows.Clear();

            int mprv;
            double prcValue;
            int volValue;
            double op = 0;
            double lo = 0;
            double hi = 0;
            double cl = 0;


            //_selectedsecuritypricetable.Rows.Add("S" + nSecurityPosition.ToString(),  1, _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition]);
            //_selectedsecuritypricetable.Rows.Add("S" + nSecurityPosition.ToString(), 1, _volume[0, nSecurityPosition]);
            
            mprv = 0;
            double P = 0;
           // int datapoints = (int)Math.Floor((double)total_day_count / PerDays);
            for (int i = 1; i <= 100; i++)
            {
                int k = PerDays * i-1;
                prcValue = _price[k, nSecurityPosition];
                volValue = 0;

                op = _price[mprv, nSecurityPosition];
                lo = double.MaxValue;
                hi = double.MinValue;

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
  
                _selectedsecuritypricetable.Rows.Add("S" + nSecurityPosition.ToString(), k + 1, op, hi, lo, cl, prcValue);
                _selectedsecurityvolumetable.Rows.Add("S" + nSecurityPosition.ToString(), k + 1, volValue);

                mprv = k + 1;
            }

            OnPropertyChanged("SelectedSecurityPrice");
            OnPropertyChanged("SelectedSecurityVolume");
        }
        private void RefreshSecuritiesData()
        {
            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;
            _securitypricetable.Rows.Clear();
            _securityvolumetable.Rows.Clear();
            int mprv;
            double prcValue;
            int volValue;
            double op = 0;
            double lo = 0;
            double hi = 0;
            double cl = 0;

            for (int nSecurityPosition = 0; nSecurityPosition < security_count; nSecurityPosition++)
            {
                _securitypricetable.Rows.Add("S" + nSecurityPosition.ToString(), 1, _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition], _price[0, nSecurityPosition]);
                _securityvolumetable.Rows.Add("S" + nSecurityPosition.ToString(), 1, _volume[0, nSecurityPosition]);
            }

            for (int nSecurityPosition = 0; nSecurityPosition < security_count; nSecurityPosition++)
            {
                mprv = 0;
                double P = 0;
                int datapoints = (int)Math.Floor((double)total_day_count / PerDays);
                for (int i = 1; i <= datapoints; i++)
                {
                    int k = PerDays * i - 1;
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
                    _securitypricetable.Rows.Add("S" + nSecurityPosition.ToString(), k + 1, op, hi, lo, cl, prcValue);
                    _securityvolumetable.Rows.Add("S" + nSecurityPosition.ToString(), k + 1, volValue);

                    mprv = k + 1;
                }
            }
            OnPropertyChanged("SecurityPrice");
            OnPropertyChanged("SecurityVolume");

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
            OnPropertyChanged("PriceIndices");
            OnPropertyChanged("VolumeIndices");
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
       

    }
}
