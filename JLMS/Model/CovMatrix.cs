using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace JLMS.Model
{
    class CovMatrix : INotifyPropertyChanged
    {
        public string Model { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        double?[] values;
        public string name;
        public CovMatrix(double val, int colCount, int nullIndex, bool bupper)
        {
            values = new double?[colCount];
            for (int i = 0; i < colCount; i++)
            {
                if (bupper)
                {
                    if (i <= nullIndex)
                        values[i] = null;
                    else
                        values[i] = val;
                }
                else
                {
                    if (i >= nullIndex)
                        values[i] = null;
                    else
                        values[i] = val;
                }
            }
            name = val.ToString();
        }

        public CovMatrix(double val, int colCount)
        {
            values = new double?[colCount];
            for (int i = 0; i < colCount; i++)
            {
               values[i] = val;
            }
            name = val.ToString();
        }

        public int GetColumnsColunt() { return values.Length; }


        public double? this[int i]
        {
            get { return values[i]; }
            set
            {
                values[i] = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
