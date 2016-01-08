using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    class Beta
    {
        public Beta() { }
        public Beta(string name, double val = 0)
        {
            Name = name;
            Value = val;
        }
        public string Name { get;  }
        public double Value { get; set; }
    }
}
