using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace JLMS.Model
{
    class SecurityData
    {
        private ObservableCollection<Beta> _listbeta;
        public SecurityData()
        {
            if (_listbeta == null)
                _listbeta = new ObservableCollection<Beta>();
        }
        public SecurityData(string security, int nbeta)
        {
            if (_listbeta == null)
                _listbeta = new ObservableCollection<Beta>();

            Security = security;
            for (int i = 0; i < nbeta; i++)
            {
                _listbeta.Add(new Beta("Beta" + i.ToString()));
            }

        }
        public ObservableCollection<Beta> BetaList
        {
            get
            {
                return _listbeta;
            }
        }
        public string  Security { get; private set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public double Alpha { get; set; }
        public double Sigma { get; set; }
        //public double Beta0 { get; set; }
        //public double Beta1 { get; set; }
        //public double Beta2 { get; set; }
        //public double Beta3 { get; set; }
        //public double Beta4 { get; set; }
        //public double Beta5 { get; set; }
        //public double Beta6 { get; set; }
        //public double Beta7 { get; set; }
        public double EqPct { get; set; }
        public double InitRet { get; set; }
    }
}
