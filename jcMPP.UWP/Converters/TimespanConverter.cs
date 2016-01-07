using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class TimespanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            var ts = (TimeSpan) value;

            var str = string.Empty;

            if (ts.TotalDays > 0) {
                str += $"{ts.TotalDays} days";
            }

            if (ts.TotalHours > 0) {
                str += $"{ts.TotalHours} hours";
            }

            if (ts.TotalMinutes > 0) {
                str += $"{ts.TotalMinutes} minutes";
            }

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}