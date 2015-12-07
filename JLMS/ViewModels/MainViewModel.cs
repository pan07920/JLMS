﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.IO;
using System.Globalization;


namespace JLMS.ViewModels
{
    public class CaseSummary 
    {
        public string Name { get; set; }
        public int TotalSecurities { get; set; }
        public int SimulationLength { get; set; }
        public bool MTOperationMode { get; set; }
        public ObservableCollection<KeyValuePair<string, KeyValuePair<string, string>>> Summary { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> OutputFiles;
    }
    class MainViewModel : ViewModelBase
    {
       
        private string _workingfolder = @"C:\JLMSim"; //todo, in setting

        private CaseSummary _selectedcase;
       
        ObservableCollection<CaseSummary> _casefilescollectioncme = new ObservableCollection<CaseSummary>();
        ObservableCollection<CaseSummary> _casefilescollectionda = new ObservableCollection<CaseSummary>();
        public bool IsCaseReady
        {
            get { return _selectedcase != null && _selectedcase.Name != ""; }
        }
        public string FileFolder { get { return _workingfolder; } }
       
        public CaseSummary SelectedCase
        {
            get { return _selectedcase; }
            set
            {
                _selectedcase = value;
                OnPropertyChanged("SelectedCase");
                OnPropertyChanged("SelectedCaseSummary");
                OnPropertyChanged("IsCaseReady");
            }
        }
    
        public ObservableCollection<KeyValuePair<string, KeyValuePair<string, string>>> SelectedCaseSummary
        {
            get { return _selectedcase == null?null:_selectedcase.Summary; }
           
        }

        public ObservableCollection<KeyValuePair<string, string>> SelectedCaseFile
        {
            get { return _selectedcase == null ? null : _selectedcase.OutputFiles; }

        }

        public ObservableCollection<CaseSummary> CaseFilesCME
        {
            get
            {
                return _casefilescollectioncme;
            }
        }
        public ObservableCollection<CaseSummary> CaseFilesDA
        {
            get
            {
                return _casefilescollectionda;
            }
        }
        public MainViewModel()
        {
            LoadCaseFiles();
        }
        private void LoadSummary(string casename)
        {
            
            string prefixInput = "JLMSimInput for Case ";
            string prefixMessage = "Messages from Case ";
            
            string caseinputfile = _workingfolder + @"\" + prefixInput + casename + ".txt";
            string casemessagefile = _workingfolder + @"\" + prefixMessage + casename + ".csv";

          
            ObservableCollection<KeyValuePair<string, KeyValuePair<string, string>>> summary = new ObservableCollection<KeyValuePair<string, KeyValuePair<string, string>>>();

            KeyValuePair<string, KeyValuePair<string, string>> rowfirst = new KeyValuePair<string, KeyValuePair<string, string>>(  "Case Name", new KeyValuePair<string, string> ("Selected Case" ,casename ));
            summary.Add(rowfirst);

          
            List<string> inputlines = File.ReadAllLines(caseinputfile).ToList<string>();
            List<string> linemessagelines = File.ReadAllLines(casemessagefile).ToList<string>();

            List<string[]> frominput = GetCaseInputParameters();
            List<string[]> fromMessage = GetCaseMessageParameters();

            int nTotalSecurities = 0;
            int nSimulationLength = 0;
            foreach (string[] param in frominput)
            {
                string val = inputlines.Find(delegate (string s) { return s.Contains(param[0]); });
                if (val == null)
                    val = "";
                else
                {
                    val = val.Split(':').ToList<string>()[1].Trim();
                }
     
                KeyValuePair<string, KeyValuePair<string, string>> newrow = new KeyValuePair<string, KeyValuePair<string, string>>(param[1], new KeyValuePair<string, string>(param[2], val));
                summary.Add(newrow);
               
                if (param[1] == "Number of Securities")
                    nTotalSecurities = int.Parse(val);
                if (param[1] == "Simulation Length")
                    nSimulationLength = int.Parse(val);
            }

            foreach (string[] param in fromMessage)
            {
                string val = linemessagelines.Find(delegate (string s) { return s.Contains(param[0]); });
                if (val == null)
                    val = "0";
                else
                {
                    val = val.Split(new char[] { ':', ',' }).ToList<string>().Last<string>().Trim();
                }
                if (param[0].Contains("Total"))
                {
                    Decimal amount = Decimal.Parse(val);
                    val = String.Format(new CultureInfo("en-US"), "{0:C0}", amount); //"{0:C}" for decimal/cents
                }

                if (param[0].Contains("Nr."))
                {
                    int amount = int.Parse(val);
                    val = String.Format("{0:n0}", amount);
                }

                KeyValuePair<string, KeyValuePair<string, string>> newrow = new KeyValuePair<string, KeyValuePair<string, string>>(param[1], new KeyValuePair<string, string>(param[2], val));
                summary.Add(newrow);
            }
            bool bMTOperationMode = true; //show Weight and Returns
            string mode = inputlines.Find(delegate (string s) { return s.Contains("or that specific specs follow"); });
            if (mode == null)
                mode = "";
            else
            {
                mode = mode.Split(new char[] { ':', ',' }).ToList<string>().Last<string>().Trim();
            }
            if (mode == "X" || mode == "N")
                bMTOperationMode = false;

            ObservableCollection<KeyValuePair<string, string>> filelist = new ObservableCollection<KeyValuePair<string, string>>();
            

            filelist.Add(new KeyValuePair<string, string>("Order Impact Analysis for Case " , "Order Impact Analysis for Case " + casename + ".csv"));
            filelist.Add(new KeyValuePair<string, string>("WhoDidWhatIn Case", "WhoDidWhatIn Case " + casename + ".csv"));
            filelist.Add(new KeyValuePair<string, string>("Trace file for Case", "Trace file for Case " + casename + ".txt"));
            filelist.Add(new KeyValuePair<string, string>("Estimates During Case", "Estimates During Case " + casename + ".csv"));
            filelist.Add(new KeyValuePair<string, string>("Daily Reports for Case", "Daily Reports for Case " + casename + ".csv"));

            filelist.Add(new KeyValuePair<string, string>(prefixInput, prefixInput + casename + ".csv"));
            filelist.Add(new KeyValuePair<string, string>(prefixMessage, prefixMessage + casename + ".csv"));

            if (bMTOperationMode)
                 filelist.Add(new KeyValuePair<string, string>("Convergence Analysis for Case", "Convergence Analysis for Case " + casename + ".csv"));
             else
                filelist.Add(new KeyValuePair<string, string>("Order Impact Analysis for Case", "Order Impact Analysis for Case " + casename + ".csv"));

            CaseSummary simcase = new CaseSummary();
            simcase.Name = casename;
            simcase.MTOperationMode = bMTOperationMode;
            simcase.SimulationLength = nSimulationLength;
            simcase.TotalSecurities = nTotalSecurities;
            simcase.OutputFiles = filelist;
            simcase.Summary = summary;
            if(bMTOperationMode)
                _casefilescollectioncme.Add(simcase);
            else
                _casefilescollectionda.Add(simcase);

        }
        private void LoadCaseFiles()
        {
            string prefixcase = "JLMSimInput for Case ";
            try
            {
                var files = from file in Directory.EnumerateFiles(_workingfolder, "*.txt", SearchOption.AllDirectories)
                            where file.Contains(prefixcase)
                            select new
                            {
                                filename = file,
                                casename = (file.Replace(".txt", "")).Replace(_workingfolder + @"\" + prefixcase, "")
                            };

                foreach (var f in files)
                {
                    LoadSummary(f.casename);
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
                   
      

        private List<string[]> GetCaseInputParameters()
        {
            List<string[]> list = new List<string[]>();
            string[] SimLength = { "Length of Simulation (in days)", "Simulation Length", "Length of Simulation (in days)" };
            list.Add(SimLength);
            string[] Securities = { "Securities (other than cash and borrowing)", "Number of Securities", "Securities (other than cash and borrowing)" };
            list.Add(Securities);
            string[] Statisticians = { "   Statisticians", "Number of Statisticians", "   Statisticians" };
            list.Add(Statisticians);
            string[] Analysts = { "   Portfolio Analysts", "Number of Analysts", "   Portfolio Analysts" };
            list.Add(Analysts);
            string[] Investors = { "Investor Templates (types of investors)", "Types of Investors", "Investor Templates (types of investors)" };
            list.Add(Investors);
            string[] Trader = { "Trader Templates (types of traders)", "Types of Traders", "Trader Templates (types of traders)" };
            list.Add(Trader);
            string[] Factor = { "price series before the start of simulation", "Number of Factors", "price series before the start of simulation" };
            list.Add(Factor);
            string[] DayKept = { "Nr. of Days Data Kept for statisticians", "Number of Days Kept", "Nr. of Days Data Kept for statisticians" };
            list.Add(DayKept);
            string[] nMonthsKept = { "Nr. of Months Data Kept for statisticians", "Number of Months Kept", "Nr. of Months Data Kept for statisticians" };
            list.Add(nMonthsKept);
            string[] mILS = { "Max Initial Sum of Long + |Short|", "Max Initial L+abs(S)", "Max Initial Sum of Long + |Short|" };
            list.Add(mILS);
            string[] mMLS = { "Max Mark-to-Market Sum of Long + |Short|", "Max Maintenance L+abs(S)", "Max Mark-to-Market Sum of Long + |Short|" };
            list.Add(mMLS);
            string[] ds = { "(statisticians use simulation's own history)", "Data Source", "(statisticians use simulation's own history)" };//"Endogenous", "Exogenous"
            list.Add(ds);
            string[] uptickRule = { "Can only short on uptick?  Y or N", "Uptick Rule", "Can only short on uptick?  Y or N" };// N --No else Yes
            list.Add(uptickRule);
            string[] rebateFraction = { "Short rebate fraction", "Rebate Fraction", "Short rebate fraction" };
            list.Add(rebateFraction);
            return list;
        }
        private List<string[]> GetCaseMessageParameters()
        {
            List<string[]> list = new List<string[]>();
            string[] TD = { "Total Deposits", "Total Deposits", "Total Deposits" };
            list.Add(TD);
            string[] NoD = { "Nr. Deposits", "Number of Deposits", "Nr. Deposits" };
            list.Add(NoD);
            string[] TW = { "Total Withdrawals", "Total Withdrawals", "Total Withdrawals" };
            list.Add(TW);
            string[] NOW = { "Nr. Withdrawals", "Number of Withdrawals", "Nr. Withdrawals" };
            list.Add(NOW);
            string[] LRS = { "Last random number seed", "Last Random Seed", "Last random number seed" };
            list.Add(LRS);
            return list;
        }
    }
  
}
