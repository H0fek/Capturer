using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Capturer.Converters
{ 
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (int.Parse(value.ToString()) < 0) return "";
            else return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            
            try
            {
                return int.Parse((string)value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
