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
        public Beta(string name)
        {
            Name = name;
            Value = 0;
        }
        public string Name { get;  }
        public double Value { get; set; }
    }
}
