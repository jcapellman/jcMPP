using System;

using Windows.UI.Xaml.Data;

namespace jcMPP.UWP.Converters {
    public class TimespanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value == null || parameter == null) {
                return null;
            }

            var span = (TimeSpan) value;

            var tmpStr =
                $"{(span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty)}{(span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty)}{(span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty)}{(span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty)}";

            if (tmpStr.EndsWith(", ")) {
                tmpStr = tmpStr.Substring(0, tmpStr.Length - 2);
            }

            if (string.IsNullOrEmpty(tmpStr)) {
                tmpStr = "0 seconds";
            }

            return tmpStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}