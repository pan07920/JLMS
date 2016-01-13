using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    class AnalystData
    {
        public AnalystData() { }
        public AnalystData(string analyst, double maxLPlusS = 0, bool canshort = true, int statisticiansused = 0)
        {
            Analyst = analyst;
            MaxLPlusS = maxLPlusS;
            ClientsCanShort = canshort;
            StatisticiansUsed = statisticiansused;
        }
        public string Analyst { get; private set; }

        public Double MaxLPlusS { get; set; }

        public bool ClientsCanShort { get; set; }
        public int StatisticiansUsed { get; set; }
    }
}
