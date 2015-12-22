using System;

using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class NetworkAuthenticationTypeToString : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            var enumCast = (NetworkAuthenticationType) Enum.Parse(typeof (NetworkAuthenticationType), value.ToString());

            switch (enumCast) {
                case NetworkAuthenticationType.Open80211:
                    return "OPEN";
                case NetworkAuthenticationType.SharedKey80211:
                    return "WEP";
                case NetworkAuthenticationType.WpaNone:
                case NetworkAuthenticationType.Wpa:
                    return "WPA";
                case NetworkAuthenticationType.RsnaPsk:
                    return "WPA 2 PSK";
                case NetworkAuthenticationType.Unknown:
                    return "Unknown";
                case NetworkAuthenticationType.WpaPsk:
                    return "WPA PSK";
                case NetworkAuthenticationType.Rsna:
                    return "WPA 2";
            }

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return null;
        }
    }
}