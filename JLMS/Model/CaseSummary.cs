using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace JLMS.Model
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
}
