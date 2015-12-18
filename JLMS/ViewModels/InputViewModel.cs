using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using JLMS.Model;
using System.Data;
using System.IO;
using DevExpress.Xpf.Core;

namespace JLMS.ViewModels
{
    class InputViewModel : ViewModelBase
    {
        string _casename;
        string _scovmatrixmodel;
        string _selectedsamplefile;
        RelayCommand _addfactorcommand;
        RelayCommand _removefactorcommand;
        RelayCommand _addsecuritycommand;
        RelayCommand _removesecuritycommand;
        RelayCommand _addinvestorcommand;
        RelayCommand _removeinvestorcommand;
        RelayCommand _addtradercommand;
        RelayCommand _removetradercommand;
        RelayCommand _addstatisticiancommand;
        RelayCommand _removestatisticiancommand;
        RelayCommand _addanalystcommand;
        RelayCommand _removeanalystcommand;
        PropertyObserver<InputViewModel> _observer;

        BasicData _basicdata = new BasicData();

        public bool CMECase { get; set; }
        public bool NewCase { get; set; }
        public BasicData BasicDataObj
        {
            get { return _basicdata; }
        }
        public string CaseName
        {
            get { return _casename; } 
            set
            {
                if (_casename != value)
                {
                    _casename = value;
                    OnPropertyChanged("CaseName");
                }
            }
        }
        public string SelectedCovMatrixModel
        {
            get
            {
                return _scovmatrixmodel;
            }
            set
            {
                if (_scovmatrixmodel != value)
                {
                    _scovmatrixmodel = value;
                    OnPropertyChanged("SelectedCovMatrixModel");
                }
            }
        }

        public ObservableCollection<SecurityData> SecurityList { get; set; }
        public ObservableCollection<AnalystData> AnalystList { get; set; }
        public ObservableCollection<Investor> InvestorList { get; set; }
        public ObservableCollection<TraderData> TraderList { get; set; }
        public ObservableCollection<StatisticianData> StatisticianList { get; set; }

        public ObservableCollection<FactorData> FactorList { get; set; }
        public ObservableCollection<CovMatrix> CovMatrixList { get; set; }
        public ObservableCollection<string> CovMatrixModelList { get; }
        public ObservableCollection<string> SampleFiles { get; }
        public string SelectedSampleFile
        {
            get { return _selectedsamplefile; }
            set
            {
                if (_selectedsamplefile != value)
                    _selectedsamplefile = value;
                OnPropertyChanged("SelectedSampleFile");
            }
        }
        private void OnSelectedSampleFileChanged(string filename)
        {
            LoadCaseInputFile(filename);

        }
        private void LoadCaseInputFile(string filename)
        {
            if (!VerifyJLMInputFile(filename))
            {
                DXMessageBox.Show(filename + " is not a valide JLM Simulator input file!", "JLMS Simulator", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            List<string> inputlines = File.ReadAllLines(filename).ToList<string>();
            

        }

        private bool LoadBasicData(ref List<string> inputlines)
        {
            try
            {
                string casename = GetDataFromLines("Data number or description (no spaces)", ref inputlines);
                _basicdata.CaseDescription = GetDataFromLines("Data number or description (no spaces)", ref inputlines);
                _basicdata.RandomSeed = int.Parse(GetDataFromLines("Initial random seed (or -1 for default)", ref inputlines));
                _basicdata.SimLength = int.Parse(GetDataFromLines("Length of Simulation (in days)", ref inputlines));
                
                _basicdata.DaysKept = int.Parse(GetDataFromLines("Nr. of Days Data Kept for statisticians", ref inputlines));
                _basicdata.MonthsKept = int.Parse(GetDataFromLines("Nr. of Months Data Kept for statisticians", ref inputlines));
                _basicdata.DaysToImpact = double.Parse(GetDataFromLines("Time in days or fraction thereof) ", ref inputlines));
                string OpMode = GetDataFromLines("or that specific specs follow ", ref inputlines).ToUpper();
                if (OpMode == "X" || OpMode == "N")
                {
                    _basicdata.CMEMode = false;
                    if (OpMode == "X")
                        _basicdata.DataSourceExogenous = true;
                    else
                        _basicdata.DataSourceExogenous = false;
                }
                else if (OpMode == "CME" || OpMode == "MODIFIED_CME" || OpMode == "CME_PACKAGE")
                    _basicdata.CMEMode = true;
                string A0A1B0B1 = GetDataFromLines("Reset A0,A1,B0,B1 parameters", ref inputlines);
                if (A0A1B0B1 != "")
                {
                    char[] whitespace = new char[] { ' ', '\t' };
                    string[] paramsarray = A0A1B0B1.Split(whitespace);
                    _basicdata.A0Param = double.Parse(paramsarray[1]);
                    _basicdata.A1Param = double.Parse(paramsarray[2]);
                    _basicdata.B0Param = double.Parse(paramsarray[3]);
                    _basicdata.B1Param = double.Parse(paramsarray[4]);
                }
              
                _basicdata.MaxInitialLpS = double.Parse(GetDataFromLines("Max Initial Sum of Long + |Short|", ref inputlines));
                _basicdata.MaxMMLpS = double.Parse(GetDataFromLines("Max Mark-to-Market Sum of Long + |Short|", ref inputlines));
                _basicdata.InitialBrokerRate = double.Parse(GetDataFromLines("Initial \"Annual\" Broker Rate (broker charges)", ref inputlines));
                _basicdata.InitialBrokerPays = double.Parse(GetDataFromLines("Initial \"Annual\" Riskfree Rate (broker pays)", ref inputlines));
                _basicdata.ThresholdDebtIncrease = double.Parse(GetDataFromLines("Increase Rates if total debt exceeds this", ref inputlines));
                _basicdata.ThresholdDebtDecrease = double.Parse(GetDataFromLines("Decrease Rates if total debt is less than this", ref inputlines));
                _basicdata.BrokerRateDelta = double.Parse(GetDataFromLines("Broker Rate Delta (\"Annual\")", ref inputlines));
                _basicdata.BrokerPaysDelta = double.Parse(GetDataFromLines("Riskfree Rate Delta (\"Annual\")", ref inputlines));
                _basicdata.EarliestRateReduction = double.Parse(GetDataFromLines("Don't lower rates before this day", ref inputlines));
                _basicdata.EarliestRateIncrease = double.Parse(GetDataFromLines("Don't raise rates before this day", ref inputlines));
                _basicdata.SmallPurchaseSlack = double.Parse(GetDataFromLines("this much purchasing power)", ref inputlines));

                string uptick = GetDataFromLines("Can only short on uptick?  Y or N", ref inputlines);

                if (uptick == "Y" || uptick == "N")
                {
                    if (uptick == "Y")
                        _basicdata.ShortOnUptick = true;
                    else
                        _basicdata.ShortOnUptick = false;

                    _basicdata.ShortRebateFraction = double.Parse(GetDataFromLines("Short rebate fraction", ref inputlines));
                    _basicdata.UseShortRebate = true;
                }
                else if (uptick == "YY" || uptick == "NN")
                {
                    if (uptick == "YY")
                        _basicdata.ShortOnUptick = true;
                    else
                        _basicdata.ShortOnUptick = false;
                    string rbt = GetDataFromLines("Short rebate fraction", ref inputlines);
                    string[] alpha_beta = rbt.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    _basicdata.AlphaRebateFraction = double.Parse(alpha_beta[0]);
                    _basicdata.BetaRebateFraction = double.Parse(alpha_beta[1]);

                }
               

                //_basicdata.InitialPortfolioSpecification = int.Parse(GetDataFromLines("Data number or description (no spaces)", ref inputlines));
                // _basicdata.InitialCashFraction = double.Parse(GetDataFromLines("Data number or description (no spaces)", ref inputlines));
                // _basicdata.CCCMType = GetDataFromLines("Data number or description (no spaces)", ref inputlines);
            }
            catch (Exception e)
            {
                DXMessageBox.Show("Error parsing Basic Data section JLM Simulator input file: " + Environment.NewLine + e.ToString(), "JLMS Simulator", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                 return false;
            }
            return true;
        }

        private string GetDataFromLines(string key, ref List<string> list)
        {
            string val = list.Find(delegate (string s) { return s.Contains(key); });
            if (val == null)
                val = "";
            else
            {
                val = val.Split(':').ToList<string>()[1].Trim();
            }
            return val;
        }

        private bool VerifyJLMInputFile(string filename)
        {

            if (File.Exists(filename))
            {
                string line;
                using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
                {
                    if ((line = file.ReadLine()) != null)
                    {
                        if (line.ToUpper().Contains("JLMSIM INPUT FILE"))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        private void OnCaseNameChange(string caseName)
        {

            {
                string s = caseName;
            }
            //ReadInputFile();

        }
        private void OnSelectedMatrixModelChanged(InputViewModel vm)
        {
            string s = vm.SelectedCovMatrixModel;
        }
        public InputViewModel()
        {
            UiServices.SetBusyState();

            _observer =
                new PropertyObserver<InputViewModel>(this)
                   .RegisterHandler(n => n.SelectedSampleFile, n => OnSelectedSampleFileChanged(n.SelectedSampleFile))
                   .RegisterHandler(n => n.CaseName, n => this.OnCaseNameChange(n.CaseName))
                   .RegisterHandler(n => n.SelectedCovMatrixModel, this.OnSelectedMatrixModelChanged);

            _casename = "New Case!";
            SampleFiles = new ObservableCollection<string>();
            LoadInputFileList();

            CovMatrixModelList = new ObservableCollection<string>();
            CovMatrixModelList.Add("DF");
            CovMatrixModelList.Add("AC-UH");
            CovMatrixModelList.Add("AC-LH");
            CovMatrixModelList.Add("AC-BH");
            SelectedCovMatrixModel = "AC-LH";
            FactorList = new ObservableCollection<FactorData>();
            FactorList.Add(new FactorData("Factor0", 1,  0.0125));
            FactorList.Add(new FactorData("Factor1", 0, 0.0225));


            SecurityList = new ObservableCollection<SecurityData>();
            SecurityList.Add(new SecurityData("Security0", FactorList.Count));
            SecurityList.Add(new SecurityData("Security1", FactorList.Count));
            SecurityList.Add(new SecurityData("Security2", FactorList.Count));
            SecurityList.Add(new SecurityData("Security3", FactorList.Count));
            SecurityList.Add(new SecurityData("Security4", FactorList.Count));
            SecurityList.Add(new SecurityData("Security5", FactorList.Count));
            SecurityList.Add(new SecurityData("Security6", FactorList.Count));
            SecurityList.Add(new SecurityData("Security7", FactorList.Count));


            AnalystList = new ObservableCollection<AnalystData>();
            AnalystList.Add(new AnalystData("PA0", 1, false));
            AnalystList.Add(new AnalystData("PA1", 0, true));

            InvestorList = new ObservableCollection<Investor>();
            InvestorList.Add(new Investor("Templ0"));
            InvestorList.Add(new Investor("Templ1"));

            TraderList = new ObservableCollection<TraderData>();
            TraderList.Add(new TraderData("LiqTrad"));

            StatisticianList = new ObservableCollection<StatisticianData>();
            StatisticianList.Add(new StatisticianData("Stat0", SecurityList.Count));


            string covmatrixmodel = "AC_LH";
            CovMatrixList = new ObservableCollection<CovMatrix>();
            for (int i = 0; i < SecurityList.Count; i++)
            {
                if (covmatrixmodel == "AC_UH")
                    CovMatrixList.Add(new CovMatrix(0, SecurityList.Count, i - 1, true));
                else if (covmatrixmodel == "AC_LH")
                    CovMatrixList.Add(new CovMatrix(0, SecurityList.Count, i + 1, false));
                else if (covmatrixmodel == "AC_BH")
                    CovMatrixList.Add(new CovMatrix(0, SecurityList.Count));
                else
                    CovMatrixList.Clear();
            }

        }
     
        private void LoadInputFileList()
        {
            SampleFiles.Clear();
            string workingfolder = Properties.Settings.Default.JLMSFolder;
            try
            {
                var files = from file in Directory.EnumerateFiles(workingfolder, "*.txt", SearchOption.TopDirectoryOnly)
                            where (!(file.Contains("Trace file for Case")))
                            select file;
         
                foreach (var f in files)
                {
                    SampleFiles.Add(f);
                }

            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }
      
        public RelayCommand AddFactorCommand
        {
            get
            {
                if (_addfactorcommand == null)
                {
                    _addfactorcommand = new RelayCommand(
                        param => this.AddFactor(),
                        param => this.CanAddFactor
                        );
                }
                return _addfactorcommand;
            }
        }

        bool CanAddFactor
        {
            get { return FactorList.Count < 99; }
        }
        private void AddFactor()
        {
            int n = FactorList.Count + 1;
            string factorname;
            factorname = "Factor" + n.ToString();
            FactorList.Add(new FactorData(factorname));
        }

        public RelayCommand RemoveFactorCommand
        {
            get
            {
                if (_removefactorcommand == null)
                {
                    _removefactorcommand = new RelayCommand(
                        param => this.RemoveFactor(),
                        param => this.CanRemoveFactor
                        );
                }
                return _removefactorcommand;
            }
        }

        bool CanRemoveFactor
        {
            get { return FactorList.Count>0; }
        }
        private void RemoveFactor()
        {
            if (FactorList.Count == 0)
                return;
            FactorList.RemoveAt(FactorList.Count-1);
        }
        public RelayCommand AddSecurityCommand
        {
            get
            {
                if (_addsecuritycommand == null)
                {
                    _addsecuritycommand = new RelayCommand(
                        param => this.AddSecurity(),
                        param => this.CanAddSecurity
                        );
                }
                return _addsecuritycommand;
            }
        }

        bool CanAddSecurity
        {
            get { return SecurityList.Count < 99; }
        }
        private void AddSecurity()
        {
            int n = SecurityList.Count + 1;
            string Securityname;
            Securityname = "Security" + n.ToString();
            SecurityList.Add(new SecurityData(Securityname, FactorList.Count));
        }

        public RelayCommand RemoveSecurityCommand
        {
            get
            {
                if (_removesecuritycommand == null)
                {
                    _removesecuritycommand = new RelayCommand(
                        param => this.RemoveSecurity(),
                        param => this.CanRemoveSecurity
                        );
                }
                return _removesecuritycommand;
            }
        }

        bool CanRemoveSecurity
        {
            get { return SecurityList.Count > 0; }
        }
        private void RemoveSecurity()
        {
            if (SecurityList.Count == 0)
                return;
            SecurityList.RemoveAt(SecurityList.Count - 1);
        }

        public RelayCommand AddAnalystCommand
        {
            get
            {
                if (_addanalystcommand == null)
                {
                    _addanalystcommand = new RelayCommand(
                        param => this.AddAnalyst(),
                        param => this.CanAddAnalyst
                        );
                }
                return _addanalystcommand;
            }
        }

        bool CanAddAnalyst
        {
            get { return AnalystList.Count < 99; }
        }
        private void AddAnalyst()
        {
            int n = AnalystList.Count + 1;
            string Analystname;
            Analystname = "PA" + n.ToString();
            AnalystList.Add(new AnalystData(Analystname));
        }

        public RelayCommand RemoveAnalystCommand
        {
            get
            {
                if (_removeanalystcommand == null)
                {
                    _removeanalystcommand = new RelayCommand(
                        param => this.RemoveAnalyst(),
                        param => this.CanRemoveAnalyst
                        );
                }
                return _removeanalystcommand;
            }
        }

        bool CanRemoveAnalyst
        {
            get { return AnalystList.Count > 0; }
        }
        private void RemoveAnalyst()
        {
            if (AnalystList.Count == 0)
                return;
            AnalystList.RemoveAt(AnalystList.Count - 1);
        }

        public RelayCommand AddInvestorCommand
        {
            get
            {
                if (_addinvestorcommand == null)
                {
                    _addinvestorcommand = new RelayCommand(
                        param => this.AddInvestor(),
                        param => this.CanAddInvestor
                        );
                }
                return _addinvestorcommand;
            }
        }

        bool CanAddInvestor
        {
            get { return InvestorList.Count < 99; }
        }
        private void AddInvestor()
        {
            int n = InvestorList.Count + 1;
            string Investorname;
            Investorname = "Templ" + n.ToString();
            InvestorList.Add(new Investor(Investorname));
        }

        public RelayCommand RemoveInvestorCommand
        {
            get
            {
                if (_removeinvestorcommand == null)
                {
                    _removeinvestorcommand = new RelayCommand(
                        param => this.RemoveInvestor(),
                        param => this.CanRemoveInvestor
                        );
                }
                return _removeinvestorcommand;
            }
        }

        bool CanRemoveInvestor
        {
            get { return InvestorList.Count > 0; }
        }
        private void RemoveInvestor()
        {
            if (InvestorList.Count == 0)
                return;
            InvestorList.RemoveAt(InvestorList.Count - 1);
        }

        public RelayCommand AddTraderCommand
        {
            get
            {
                if (_addtradercommand == null)
                {
                    _addtradercommand = new RelayCommand(
                        param => this.AddTrader(),
                        param => this.CanAddTrader
                        );
                }
                return _addtradercommand;
            }
        }

        bool CanAddTrader
        {
            get { return TraderList.Count < 99; }
        }
        private void AddTrader()
        {
            int n = TraderList.Count-1 ;
            string Tradername;
            Tradername = "TT" + n.ToString();
            TraderList.Add(new TraderData(Tradername));
        }

        public RelayCommand RemoveTraderCommand
        {
            get
            {
                if (_removetradercommand == null)
                {
                    _removetradercommand = new RelayCommand(
                        param => this.RemoveTrader(),
                        param => this.CanRemoveTrader
                        );
                }
                return _removetradercommand;
            }
        }

        bool CanRemoveTrader
        {
            get { return TraderList.Count > 0; }
        }
        private void RemoveTrader()
        {
            if (TraderList.Count == 0)
                return;
            TraderList.RemoveAt(TraderList.Count - 1);
        }
        public RelayCommand AddStatisticianCommand
        {
            get
            {
                if (_addstatisticiancommand == null)
                {
                    _addstatisticiancommand = new RelayCommand(
                        param => this.AddStatistician(),
                        param => this.CanAddStatistician
                        );
                }
                return _addstatisticiancommand;
            }
        }

        bool CanAddStatistician
        {
            get { return StatisticianList.Count < 99; }
        }
        private void AddStatistician()
        {
            int n = StatisticianList.Count + 1;
            string Statisticianname;
            Statisticianname = "Stat" + n.ToString();
            StatisticianList.Add(new StatisticianData(Statisticianname, SecurityList.Count) );
        }

        public RelayCommand RemoveStatisticianCommand
        {
            get
            {
                if (_removestatisticiancommand == null)
                {
                    _removestatisticiancommand = new RelayCommand(
                        param => this.RemoveStatistician(),
                        param => this.CanRemoveStatistician
                        );
                }
                return _removestatisticiancommand;
            }
        }

        bool CanRemoveStatistician
        {
            get { return StatisticianList.Count > 0; }
        }
        private void RemoveStatistician()
        {
            if (StatisticianList.Count == 0)
                return;
            StatisticianList.RemoveAt(StatisticianList.Count - 1);
        }
    }
}
