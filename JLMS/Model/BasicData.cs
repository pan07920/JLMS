using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    public class BasicData
    {
        private static string defaultCaseDescription = "";
        private static int defaultRandomSeed = -1;
        private static int defaultSimLength = 8000;
        private static string defaultShortOnUptick = "NN";
        private static double defaultShortRebateFraction = 0;
        private static double defaultAlphaRebateFraction = -0.0025;
        private static double defaultBetaRebateFraction = 1;
        private static int defaultDaysKept  = 20;
        private static int defaultMonthsKept  = 2;
        private static double defaultDaysToImpact  = 100;
        private static bool defaultCMEMode = true;
        private static string defaultDataSource = "N";
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
        private static double defaultEaliestRateReduction = 20000;
        private static double defaultEarliestRateIncrease  = 20000;
        private static double defaultSmallPurchaseSlack = 1000;
        private static int defaultInitialPortfolioSpecification = 0;
        private static double defaultInitialCashFraction = 0;
        private static string defaultCCCMType = "DF";
        public BasicData()
        {
            CaseDescription = defaultCaseDescription;
            SimLength = defaultSimLength;
            RandomSeed = defaultRandomSeed;
            ShortOnUptick = defaultShortOnUptick;
            ShortRebateFraction = defaultShortRebateFraction;
            AlphaRebateFraction = defaultAlphaRebateFraction;
            BetaRebateFraction = defaultBetaRebateFraction;
            DaysKept = defaultDaysKept;
            MonthsKept = defaultMonthsKept;
            DaysToImpact = defaultDaysToImpact;
            DataSource = "N";
            CMEMode = defaultCMEMode;
            A0Param = defaultA0Param;
            A1Param = defaultA1Param;
            B0Param = defaultB0Param;
            B1Param = defaultB1Param;

            MaxInitialLpS = defaultMaxInitialLpS;
            MaxInitialLpS = defaultMaxMMLpS;
            InitialBrokerRate = defaultInitialBrokerRate;
            InitialBrokerPays = defaultInitialBrokerPays;
            ThresholdDebtIncrease = defaultThresholdDebtIncrease;
            ThresholdDebtDecrease = defaultThresholdDebtDecrease;
            BrokerRateDelta = defaultBrokerRateDelta;
            BrokerPaysDelta = defaultBrokerPaysDelta;
            EaliestRateReduction = defaultEaliestRateReduction;
            EarliestRateIncrease = defaultEarliestRateIncrease;
            SmallPurchaseSlack = defaultSmallPurchaseSlack;
            InitialPortfolioSpecification = defaultInitialPortfolioSpecification;
            InitialCashFraction = defaultInitialCashFraction;
            CCCMType = defaultCCCMType;
        }
        public string CaseDescription { get; set; }
        public double RandomSeed { get; set; }
        public int SimLength { get; set; }
        public string ShortOnUptick { get; set; }
        public double ShortRebateFraction { get; set; }
        public double AlphaRebateFraction { get; set; }
        public double BetaRebateFraction { get; set; }
        public int DaysKept { get; set; }
        public int MonthsKept { get; set; }
        public double DaysToImpact { get; set; }
        public bool CMEMode { get; set; }
        public string DataSource { get; set; }
        public bool A0A1B0B1 { get; set; }
        public double A0Param { get; set; }
        public double A1Param { get; set; }
        public double B0Param { get; set; }
        public double B1Param { get; set; }
        public double MaxInitialLpS { get; set; }
        public double MaxMMLpS { get; set; }
        public double InitialBrokerRate { get; set; }
        public double InitialBrokerPays { get; set; }
        public double ThresholdDebtIncrease { get; set; }
        public double ThresholdDebtDecrease { get; set; }
        public double BrokerRateDelta { get; set; }
        public double BrokerPaysDelta { get; set; }
        public double EaliestRateReduction { get; set; }
        public double EarliestRateIncrease { get; set; }
        public double SmallPurchaseSlack { get; set; }
        public int InitialPortfolioSpecification { get; set; }
        public double InitialCashFraction { get; set; }
        public string CCCMType { get; set; }
    }
}
