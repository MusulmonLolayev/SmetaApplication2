using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SmetaApplication.Converter
{
    class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return ((double)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            double d;
            string val = (string)value;
            if (!double.TryParse(val, out d))
            {
                val = val.Replace('.', ',');
                if (!double.TryParse(val, out d))
                {
                    val = val.Replace(',', '.');
                    if (!double.TryParse(val, out d))
                    {
                        MessageBox.Show("Ошибка при вводы");
                        value = val;
                        return value;
                    }
                    else
                        value = val;
                }
                else
                    value = val;
                return value;
            }
            return d;
        }
    }
}