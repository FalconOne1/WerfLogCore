using System.Globalization;

namespace WerfLogApp.Converter
{
    public class DurationConverter : IValueConverter
    {
        //Omzetten TotaalUren van INT naar String en visa versa, in applicatielaag --> in samenwerking met XAML
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int minutes)
            {
                int uren = minutes / 60;
                int minuten = minutes % 60;
                return $"{uren}u {minuten}m";
            }
            return "0u 0m";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                var parts = strValue.Split(new[] { 'u', 'm' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out int uren) && int.TryParse(parts[1], out int minuten))
                {
                    return (uren * 60) + minuten;
                }
            }
            return 0;
        }
    }
}
