using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class TimeStampToString : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            var timeStamp = System.Convert.ToDateTime(value);

            return timeStamp == DateTime.MinValue ? "Never" : timeStamp.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return null;
        }
    }
}