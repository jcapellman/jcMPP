using System;

using Windows.Devices.WiFi;
using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class SSIDConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            var network = (WiFiAvailableNetwork) value;

            return string.IsNullOrEmpty(network.Ssid) ? $"Hidden Network ({network.Bssid})" : network.Ssid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}