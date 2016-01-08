using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class WifiDirectConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return System.Convert.ToBoolean(value) ? $"/Assets/wifidirect.png" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}