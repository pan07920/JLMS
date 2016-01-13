using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLMS.Model
{
    class Security
    {
        public Security() { }
        public Security(string name)
        {
            Name = name;
            Value = 0;
        }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
