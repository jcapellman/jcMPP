using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class BitsToMegabitsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var val = System.Convert.ToInt64(value);

            return $"{val/ 1000000} mb/sec";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}