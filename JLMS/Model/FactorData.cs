using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    class FactorData
    {
        public FactorData() { }
        public FactorData(string factor, double dailyreturn = 0, double dailysigma = 0)
        {
            Factor = factor;
            DailyReturn = dailyreturn;
            DailySigma = dailysigma;
        }
        public string Factor { get; private set; }

        public Double DailyReturn { get; set; }

        public Double DailySigma { get; set; }
    }
}
