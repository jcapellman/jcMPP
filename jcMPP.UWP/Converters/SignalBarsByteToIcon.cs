using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class SignalBarsByteToIcon : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var numBars = System.Convert.ToInt32(value);

            return $"/Assets/Icons/wifi_signal_{numBars}_bar.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}