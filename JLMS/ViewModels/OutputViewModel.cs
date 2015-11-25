using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;

namespace JLMS.ViewModels
{
    class OutputViewModel : ViewModelBase
    {
        [DllImport("DataParsingDLL.dll", EntryPoint = "Process")]
        public static extern int ParsingProcessData(string SimInput, string TraceFile, string output);
        private string _jlmsimfolder = @"C:\JLMSim";
        double[,] _price;
        int[,] _volume;
        double[,] _weightmarket;
        int[] _totalvolume;

        DataTable _weightstable;
        DataTable _returnstable;
        private CaseSummary _selectedcase;
        public string SelectedCaseName
        {
            get { return _selectedcase != null ? _selectedcase.Name : ""; }

        }
        public CaseSummary SelectedCase
        {
            get { return _selectedcase; }
            set
            {
                _selectedcase = value;

                if (_selectedcase != null)
                {
                    LoadData();
                }
                OnPropertyChanged("SelectedCase");
                OnPropertyChanged("SelectedCaseSummary");
            }
        }
        public ObservableCollection<KeyValuePair<string, string>> SelectedCaseSummary
        {
            get { return _selectedcase == null ? null : _selectedcase.Summary; }

        }
        public OutputViewModel()
        {
            _weightstable = new DataTable("WeightsTable");
            _weightstable.Columns.Add("Security", typeof(String));
            _weightstable.Columns.Add("Day", typeof(Int16));
            _weightstable.Columns.Add("Value", typeof(Double));

            _returnstable = new DataTable("ReturnsTable");
            _returnstable.Columns.Add("Security", typeof(String));
            _returnstable.Columns.Add("Month", typeof(Int16));
            _returnstable.Columns.Add("Value", typeof(Double));
            
        }
     
        public DataTable SecurityWeights
        {
            get { return _weightstable; }
        }
        public DataTable SecurityReturns
        {
            get { return _returnstable; }
        }
     
       

        private void LoadData()
        {
            if (_selectedcase == null)
                return;

            if (!_selectedcase.MTOperationMode)
            {
                LoadDailyDataFile();
               // RefreshIndicesChart();
            }
            else
            {
                string prefixInput = "JLMSimInput for Case ";

                string casename = _selectedcase.Name;
                string caseinputfile = _jlmsimfolder + @"\" + prefixInput + casename + ".txt";

                string tracefile = _jlmsimfolder + @"\" + "Trace file for Case " + casename + ".txt";
                string outfile = _jlmsimfolder + @"\" + "ParsedSecurityWts.csv";

                FileInfo fio = new FileInfo(outfile);
                FileInfo fic = new FileInfo(caseinputfile);

                if (fio.LastWriteTime < fic.LastWriteTime)
                {
                    int rtn = ParsingProcessData(caseinputfile, tracefile, outfile);
                }

                SamplingParsedWeightData(outfile, 20);

                string returnfile = _jlmsimfolder + @"\" + "Estimates During Case " + casename + ".csv";
                CMEReturnEstimateData(returnfile);//, 100);
            }
        }

        private void LoadDailyDataFile()
        {
            string prefixDailyReport = "Daily Reports for Case ";
            string casename = _selectedcase.Name; 
            string dailyreportfile = _jlmsimfolder + @"\" + prefixDailyReport + casename + ".csv";
            List<string> inputlines = File.ReadAllLines(dailyreportfile).ToList<string>();

            for (int i = 0; i < 4; i++)
                inputlines.RemoveAt(0);

            int security_count = _selectedcase.TotalSecurities;
            int total_day_count = _selectedcase.SimulationLength;
            _price = new double[total_day_count, security_count];
            _volume = new int[total_day_count, security_count];
            _weightmarket = new double[total_day_count, 2];
            _totalvolume = new int[total_day_count];
            int row = 0;
            foreach (string line in inputlines)
            {
                string workingline = line.Replace("1.#J", "0");
                workingline = line.Replace("1.#IO", "0");
                List<string> linedata = line.Split(',').ToList<string>();
                int day = int.Parse(linedata[0]);

                for (int i = 1; i <= security_count; i++)
                {
                    _price[row, i - 1] = double.Parse(linedata[i]);
                    _volume[row, i - 1] = int.Parse(linedata[i + 2 + security_count]);
                }
                _weightmarket[row, 0] = double.Parse(linedata[security_count + 1]);
                _weightmarket[row, 1] = double.Parse(linedata[security_count + 2]);
                _totalvolume[row] = int.Parse(linedata[3 + 2 * security_count]);
                row++;
            }

        }
        private void SamplingParsedWeightData(string filename, int interval)
        {
            int _security_count = _selectedcase.TotalSecurities;
            List<string> inputlines = File.ReadAllLines(filename).ToList<string>();
            int totalline = inputlines.Count;

            int samplecount = totalline / interval;
            int[] sampleindex = new int[samplecount];
            double[,] sampledata = new double[samplecount, _security_count + 1];
            string sec = "";
            double weight = 0;
            int day = 0;
            for (int i = 0; i < samplecount; i++)
            {
                List <string> linedata = inputlines[i * interval].Split(',').ToList<string>();
                day = int.Parse(linedata[0]);
                for (int j = 0; j < _security_count + 1; j++)
                {
                    sec = "S" + j.ToString();
                    if (j == _security_count)
                        sec = "Cash";
                     weight = double.Parse(linedata[j + 1]);
                    _weightstable.Rows.Add(new object[] { sec, day, weight });
                }
            }
        }

        private void CMEReturnEstimateData(string filename)
        {
            //For CME cases, Return Chart, all securities, by month
            int _security_count = _selectedcase.TotalSecurities;
            List<string> inputlines = File.ReadAllLines(filename).ToList<string>();
            int totalline = inputlines.Count;

            int numberofmonth = totalline - 2;

            string sec = "";
            double returns = 0;
            int month = 0;

            for (int i = 0; i < numberofmonth; i++)
            {
                List<string> linedata = inputlines[i + 2].Split(',').ToList<string>();
                month = int.Parse(linedata[0]);
                for (int j = 0; j < _security_count ; j++)
                {
                    sec = "S" + j.ToString();
                    returns = 100 * double.Parse(linedata[j + 1]);
                    _returnstable.Rows.Add(new object[] { sec, month, returns });
                }
            }

            //chartReturns.Series.Clear();
            //for (int i = 0; i < TotalSecurities; i++)
            //{
            //    chartReturns.Series.Add("S" + i.ToString(), DevExpress.XtraCharts.ViewType.Line);
            //}
            //double ymax = 10, ymin = 10;
            //double pointvalue;
            //for (int m = 0; m < numberofmonth; m++)
            //{
            //    for (int s = 0; s < TotalSecurities; s++)
            //    {
            //        pointvalue = 100 * estimatereturndata[m, s];
            //        ymax = Math.Max(ymax, pointvalue);
            //        ymin = Math.Min(ymin, pointvalue);
            //        chartReturns.Series[s].Points.Add(new DevExpress.XtraCharts.SeriesPoint(m, 100 * estimatereturndata[m, s]));
            //    }
            //}
            //ymax = Math.Ceiling(ymax);
            //ymin = Math.Floor(ymin);
            //DevExpress.XtraCharts.XYDiagram diagram = (DevExpress.XtraCharts.XYDiagram)chartReturns.Diagram;
            //// Enable the diagram's scrolling.
            //diagram.EnableAxisXScrolling = true;
            //diagram.EnableAxisYScrolling = true;

           

            //// Define the whole range for the Y-axis. 
            //diagram.AxisY.WholeRange.Auto = false;
            //diagram.AxisY.WholeRange.SetMinMaxValues(ymin, ymax);

            //diagram.AxisX.VisualRange.AutoSideMargins = false;
            //diagram.AxisY.VisualRange.AutoSideMargins = false;

        }

    }
}
