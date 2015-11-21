using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class StringFormatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}