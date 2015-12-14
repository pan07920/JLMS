using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    class Investor
    {
        public Investor() { }
        public Investor(string investort)
        {
            InvestorTemplate = investort;
            NumberOfInvestors = 10;
            ReoptimizationFrequency = "dd";
            PortfolioAnalystUsed = 0;
            TraderTemplateUsed = 0;
            RiskAversion = 8;
            MeanStartWealth = 7;
            SigmaStartWealth = 0;
            Probability = 0;
            LowerEdge = 0;
            UpperEdge = 0;
            Increment = 0;
            MaximumBuy = 0.1;
            MaximuSellNormal =0.03;
            MaximuSellUrgent = 0.1;
            TemporaryExtraLeverage = 0;
            TraceFraction = -1;
            TraceWeathiesNumber = 1;

        }
        public string InvestorTemplate { get; set; }
        public int  NumberOfInvestors { get; set; }
        public string ReoptimizationFrequency { get; set; }
        public int PortfolioAnalystUsed { get; set; }
        public int TraderTemplateUsed { get; set; }
        public int RiskAversion { get; set; }
        public int MeanStartWealth { get; set; }
        public int SigmaStartWealth { get; set; }
        public double Probability { get; set; }
        public double LowerEdge { get; set; }
        public double UpperEdge { get; set; }
        public double Increment { get; set; }
        public double MaximumBuy { get; set; }
        public double MaximuSellNormal { get; set; }
        public double MaximuSellUrgent { get; set; }
        public int TemporaryExtraLeverage { get; set; }
        public int TraceFraction { get; set; }
        public int TraceWeathiesNumber { get; set; }

    }
}
