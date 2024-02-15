using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogApp.Converter
{
    public class StringToDateConverter : IValueConverter
    {
       
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime dateTime)
                {
                    return dateTime.ToString("dd/MM/yyyy  HH:mm");
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string dateTimeString && DateTime.TryParseExact(dateTimeString, "dd/MM/yyyy  HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    return dateTime;
                }
                return value;
            }
        }

    }


