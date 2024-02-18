using System.Globalization;

namespace WerfLogApp.Converter
{

    public class TimeStringConverter : IValueConverter
    {
        //Omzetten van DATETIME naar String en visa versa, in applicatielaag --> in samenwerking met XAML

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
            return null;
        }
    }

}

