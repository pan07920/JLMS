using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;
using System.Timers;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
namespace JLMS
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool original = (bool)value;
            return !original;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool original = (bool)value;
            return !original;
        }
    }

    class DataTableMaxValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataTable table = (DataTable)value;
           // Double min = Double.MaxValue;
            Double max = Double.MinValue;
            foreach (DataRow dr in table.Rows)
            {
                Double val = dr.Field<Double>("Value");
               // min = Math.Min(min, val);
                max = Math.Max(max, val);
            }
            return max;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class DataTableMinValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataTable table = (DataTable)value;
             Double min = Double.MaxValue;
           // Double max = Double.MinValue;
            foreach (DataRow dr in table.Rows)
            {
                Double val = dr.Field<Double>("Value");
                 min = Math.Min(min, val);
               // max = Math.Max(max, val);
            }
            return min;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return Visibility.Visible; //todo
            if ((bool)value) //server mode;
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertVisibilityConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                Visibility vis = (Visibility)value;
                return vis == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new Exception("Invalid call - one way only");
        }
    }
    class NegateBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean && (bool)value)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value is Visibility && (Visibility)value == Visibility.Visible)
            //{
            //    return true;
            //}
            //return false;
            throw new NotImplementedException();
        }
    }



}


