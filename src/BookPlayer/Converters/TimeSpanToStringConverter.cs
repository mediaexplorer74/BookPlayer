using System;
using Xamarin.Forms;

namespace BookPlayer.Converters
{
    /// <summary>
    /// Converts <see cref="DateTime"/> to short data string of current UI culture
    /// </summary>
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                return ts.ToString(@"hh\:mm\:ss");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
