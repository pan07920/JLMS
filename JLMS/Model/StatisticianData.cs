using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace JLMS.Model
{
    class StatisticianData
    {
        private ObservableCollection<Security> _listsecurity;
        private string _retfreq;
        private int? _retnum;
        private string _method;
        public StatisticianData()
        {
            if (_listsecurity == null)
                _listsecurity = new ObservableCollection<Security>();
            Method = "HIST";
            _retnum = 0;
            RetFreq = "dd";
            CovFreq = "dd";
            CovNum = 0;

        }
        public StatisticianData(string statistician, int nsecurity)
        {
            if (_listsecurity == null)
                _listsecurity = new ObservableCollection<Security>();

            Statistician = statistician;
            for (int i = 0; i < nsecurity; i++)
            {
                _listsecurity.Add(new Security("Sec" + i.ToString()));
            }
            Method = "HIST";
            _retnum = 0;
            RetFreq = "dd";
            CovFreq = "dd";
            CovNum = 0;
        }
        public ObservableCollection<Security> SecurityList
        {
            get
            {
                return _listsecurity;
            }
        }
        public string Statistician { get; }
        public string Method
        {
            get { return _method; }
            set
            {
                if (value == "RPS_C")
                {
                    _retfreq = null;
                    _retnum = null;
                }
                else
                {
                    _retfreq = "";
                    _retnum = 0;
                }
                _method = value;
            }
        }
        public string RetFreq
        {
            get { return _retfreq; }
            set
            {
                if (Method == "RPS_C")
                    _retfreq = null;
                else
                    _retfreq = value;
            }
        }
        public int? RetNum
        {
            get { return _retnum; }
            set
            {
                if (Method == "RPS_C")
                    _retnum = null;
                else
                    _retnum = value;
            }
        }
        public string CovFreq { get; set; }
        public int CovNum { get; set; }
    }
}
