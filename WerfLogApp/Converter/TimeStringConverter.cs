using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogApp.Converter
{
   
        public class TimeStringConverter : IValueConverter
        {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value is TimeSpan time)
        //    {
        //        return time.ToString(@"hh\:mm");
        //    }
        //    return string.Empty;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (TimeSpan.TryParseExact(value?.ToString(), @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan result))
        //    {
        //        return result;
        //    }
        //    return TimeSpan.Zero;
        //}
        // Het formaat waarin u wilt dat gebruikers de datum en tijd invoeren.
        // Dit voorbeeld gebruikt het formaat "dd/MM/yyyy HH:mm", maar u kunt dit aanpassen.



        private const string DateTimeFormat = "dd/MM/yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString(DateTimeFormat);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && DateTime.TryParseExact(stringValue, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return dateTime;
            }
            // Signaleer een ongeldige invoer.
            return null; // Of gooi een exception, afhankelijk van uw foutafhandelingsstrategie.
        }
    }

    }

