using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;

namespace jcMPP.UWP.ViewModels {
    public class BaseModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility _progressBarRunning;

        public Visibility ProgressBarRunning { get { return _progressBarRunning; ; } set { _progressBarRunning = value; OnPropertyChanged(); } }

        protected void ShowRunning() {
            ProgressBarRunning = Visibility.Visible;
        }

        protected void HideRunning() {
            ProgressBarRunning = Visibility.Collapsed;
        }

        public bool HasInternetConnection {
            get {
                var result = NetworkInformation.GetInternetConnectionProfile();

                return result != null;
            }
        }
    }
}