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

namespace JLMS.ViewModels
{
    class InputViewModel : ViewModelBase
    {
        string _casename;
        RelayCommand _addfactorcommand;
        RelayCommand _removefactorcommand;
        RelayCommand _addsecuritycommand;
        RelayCommand _removesecuritycommand;
        RelayCommand _addinvestorcommand;
        RelayCommand _removeinvestorcommand;

        RelayCommand _addanalystcommand;
        RelayCommand _removeanalystcommand;


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
        public ObservableCollection<SecurityData> SecurityList { get; set; }
        public ObservableCollection<AnalystData> AnalystList { get; set; }
        public ObservableCollection<Investor> InvestorList { get; set; }

        public ObservableCollection<FactorData> FactorList { get; set; }
        public InputViewModel()
        {
            _casename = "New Case!";

            FactorList = new ObservableCollection<FactorData>();
            FactorList.Add(new FactorData("Factor0", 1,  0.0125));
            FactorList.Add(new FactorData("Factor1", 0, 0.0225));


            SecurityList = new ObservableCollection<SecurityData>();
            SecurityList.Add(new SecurityData("Security0", FactorList.Count));
           
            SecurityList.Add(new SecurityData("Security1", FactorList.Count));

            AnalystList = new ObservableCollection<AnalystData>();
            AnalystList.Add(new AnalystData("PA0", 1, false));
            AnalystList.Add(new AnalystData("PA1", 0, true));

            InvestorList = new ObservableCollection<Investor>();
            InvestorList.Add(new Investor("Templ0"));
            InvestorList.Add(new Investor("Templ1"));

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

    }
}
