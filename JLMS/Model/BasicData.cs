using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JLMS.Model
{
    public class BasicData: INotifyPropertyChanged
    {
        private static string defaultCaseDescription = "";
        private static int defaultRandomSeed = -1;
        private static int defaultSimLength = 8000;
        private static bool defaultShortOnUptick = false;
        private static bool defaultUseShortRebate = false;
        private static double defaultShortRebateFraction = 0;
        private static double defaultAlphaRebateFraction = -0.0025;
        private static double defaultBetaRebateFraction = 1;
        private static int defaultDaysKept  = 20;
        private static int defaultMonthsKept  = 2;
        private static double defaultDaysToImpact  = 100;
        private static bool defaultCMEMode = true;
        //private static string defaultDataSource = "N";
        private static double defaultA0Param  = 0;
        private static double defaultA1Param  = 0;
        private static double defaultB0Param  = 0;
        private static double defaultB1Param  = 0.005;

        private static double defaultMaxInitialLpS = 2;
        private static double defaultMaxMMLpS  = 4;
        private static double defaultInitialBrokerRate  = 0.06;
        private static double defaultInitialBrokerPays  = 0.03;
        private static double defaultThresholdDebtIncrease = 10000000000;
        private static double defaultThresholdDebtDecrease = 0;
        private static double defaultBrokerRateDelta = 0.0025;
        private static double defaultBrokerPaysDelta  = 0.00125;
        private static double defaultEarliestRateReduction = 20000;
        private static double defaultEarliestRateIncrease  = 20000;
        private static double defaultSmallPurchaseSlack = 1000;

        private static bool defaultInitialcash = false;
        private static double defaultInitialCashFraction = 0;
        private static string defaultCCCMType = "DF";


        private string _casedescription = defaultCaseDescription;
        private int _simlength = defaultSimLength;
        private double _randomseed = defaultRandomSeed;
        private bool _shortonuptick = defaultShortOnUptick;
        private bool _useshortrebate = defaultUseShortRebate;
        private double _shortrebatefraction = defaultShortRebateFraction;
        private double _alpharebatefraction = defaultAlphaRebateFraction;
        private double _betarebatefraction = defaultBetaRebateFraction;
        private int _dayskept = defaultDaysKept;
        private int _monthskept = defaultMonthsKept;
        private double _daystoimpact = defaultDaysToImpact;
        private bool _datasourceexogenous = false;
        private bool _cmemode = defaultCMEMode;
        private double _a0param = defaultA0Param;
        private double _a1param = defaultA1Param;
        private double _b0param = defaultB0Param;
        private double _b1param = defaultB1Param;

        private double _maxinitiallps = defaultMaxInitialLpS;
        private double _maxmmlps = defaultMaxMMLpS;
        private double  _initialbrokerrate = defaultInitialBrokerRate;
        private double  _initialbrokerpays = defaultInitialBrokerPays;
        private double  _thresholddebtincrease = defaultThresholdDebtIncrease;
        private double  _thresholddebtdecrease = defaultThresholdDebtDecrease;
        private double  _brokerratedelta = defaultBrokerRateDelta;
        private double  _brokerpaysdelta = defaultBrokerPaysDelta;
        private double  _earliestratereduction = defaultEarliestRateReduction;
        private double  _earliestrateincrease = defaultEarliestRateIncrease;
        private double  _smallpurchaseslack = defaultSmallPurchaseSlack;
        private bool    _initialcash = defaultInitialcash;
        private double _initialcashfraction = defaultInitialCashFraction;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
        ////protected bool SetField<T>(ref T field, T value, string propertyName)
        //{
        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
        //Example
        //private string name;
        //public string Name
        //{
        //    get { return name; }
        //    set { SetField(ref name, value, "Name"); }
        //}
        //C# 5 //set { SetField(ref name, value); }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public BasicData()
        {
            _casedescription = defaultCaseDescription;
            _simlength = defaultSimLength;
            _randomseed = defaultRandomSeed;
            _shortonuptick = defaultShortOnUptick;
            _useshortrebate = defaultUseShortRebate;
            _shortrebatefraction = defaultShortRebateFraction;
            _alpharebatefraction = defaultAlphaRebateFraction;
            _betarebatefraction = defaultBetaRebateFraction;
            _dayskept = defaultDaysKept;
            _monthskept = defaultMonthsKept;
            _daystoimpact = defaultDaysToImpact;
            _datasourceexogenous = false;
            _cmemode = defaultCMEMode;
            _a0param = defaultA0Param;
            _a1param = defaultA1Param;
            _b0param = defaultB0Param;
            _b1param = defaultB1Param;

            _maxinitiallps = defaultMaxInitialLpS;
            _maxmmlps = defaultMaxMMLpS;
            _initialbrokerrate = defaultInitialBrokerRate;
            _initialbrokerpays = defaultInitialBrokerPays;
            _thresholddebtincrease = defaultThresholdDebtIncrease;
            _thresholddebtdecrease = defaultThresholdDebtDecrease;
            _brokerratedelta = defaultBrokerRateDelta;
            _brokerpaysdelta = defaultBrokerPaysDelta;
            _earliestratereduction = defaultEarliestRateReduction;
            _earliestrateincrease = defaultEarliestRateIncrease;
            _smallpurchaseslack = defaultSmallPurchaseSlack;
            _initialcash = defaultInitialcash;

            _initialcashfraction = defaultInitialCashFraction;
            //_CCCMType = defaultCCCMType;
        }  
        public string CaseDescription
        {
            get { return _casedescription; }
            set { SetField(ref _casedescription, value); }
        }
        public double RandomSeed
        {
            get { return _randomseed; }
            set { SetField(ref _randomseed, value); }
        }
        public int SimLength
        {
            get { return _simlength; }
            set { SetField(ref _simlength, value); }
        }
        public bool ShortOnUptick
        {
            get { return _shortonuptick; }
            set { SetField(ref _shortonuptick, value); }
        }
        public bool UseShortRebate
        {
            get { return _useshortrebate; }
            set { SetField(ref _useshortrebate, value); }
        }
        public double ShortRebateFraction
        {
            get { return _shortrebatefraction; }
            set { SetField(ref _shortrebatefraction, value); }
        }
        public double AlphaRebateFraction
        {
            get { return _alpharebatefraction; }
            set { SetField(ref _alpharebatefraction, value); }
        }
        public double BetaRebateFraction
        {
            get { return _betarebatefraction; }
            set { SetField(ref _betarebatefraction, value); }
        }
        public int DaysKept
        {
            get { return _dayskept; }
            set { SetField(ref _dayskept, value); }
        }
        public int MonthsKept
        {
            get { return _monthskept; }
            set { SetField(ref _monthskept, value); }
        }
        public double DaysToImpact
        {
            get { return _daystoimpact; }
            set { SetField(ref _daystoimpact, value); }
        }
        public bool CMEMode
        {
            get { return _cmemode; }
            set { SetField(ref _cmemode, value); }
        }
        public bool DataSourceExogenous
        {
            get { return _datasourceexogenous; }
            set { SetField(ref _datasourceexogenous, value); }
        }
        //public bool A0A1B0B1
        //{
        //    get { return _a0a1b0b1; }
        //    set { SetField(ref _a0a1b0b1, value); }
        //}
        public double A0Param
        {
            get { return _a0param; }
            set { SetField(ref _a0param, value); }
        }
        public double A1Param
        {
            get { return _a1param; }
            set { SetField(ref _a1param, value); }
        }
        public double B0Param
        {
            get { return _b0param; }
            set { SetField(ref _b0param, value); }
        }
        public double B1Param
        {
            get { return _b1param; }
            set { SetField(ref _b1param, value); }
        }
        public double MaxInitialLpS
        {
            get { return _maxinitiallps; }
            set { SetField(ref _maxinitiallps, value); }
        }
        public double MaxMMLpS
        {
            get { return _maxmmlps; }
            set { SetField(ref _maxmmlps, value); }
        }
        public double InitialBrokerRate
        {
            get { return _initialbrokerrate; }
            set { SetField(ref _initialbrokerrate, value); }
        }
        public double InitialBrokerPays
        {
            get { return _initialbrokerpays; }
            set { SetField(ref _initialbrokerpays, value); }
        }
        public double ThresholdDebtIncrease
        {
            get { return _thresholddebtincrease; }
            set { SetField(ref _thresholddebtincrease, value); }
        }
        public double ThresholdDebtDecrease
        {
            get { return _thresholddebtdecrease; }
            set { SetField(ref _thresholddebtdecrease, value); }
        }
        public double BrokerRateDelta
        {
            get { return _brokerratedelta; }
            set { SetField(ref _brokerratedelta, value); }
        }
        public double BrokerPaysDelta
        {
            get { return _brokerpaysdelta; }
            set { SetField(ref _brokerpaysdelta, value); }
        }
        public double EarliestRateReduction
        {
            get { return _earliestratereduction; }
            set { SetField(ref _earliestratereduction, value); }
        }
        public double EarliestRateIncrease
        {
            get { return _earliestrateincrease; }
            set { SetField(ref _earliestrateincrease, value); }
        }
        public double SmallPurchaseSlack
        {
            get { return _smallpurchaseslack; }
            set { SetField(ref _smallpurchaseslack, value); }
        }
        public double InitialCashFraction
        {
            get { return _initialcashfraction; }
            set { SetField(ref _initialcashfraction, value); }
        }
        public bool InitialCash
        {
            get { return _initialcash; }
            set { SetField(ref _initialcash, value); } 
        }
        //public string CCCMType { get; set; }

    }
}
