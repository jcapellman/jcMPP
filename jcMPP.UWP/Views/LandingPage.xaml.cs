using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace jcMPP.UWP.Views {

    public sealed partial class LandingPage : Page {
        public LandingPage() {
            this.InitializeComponent();
        }

        private void BtnPortScan_OnClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(PortScanPage));
        }

        private void BtnHash_OnClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(HashCrackerPage));
        }

        private void BtnSettings_OnClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(SettingsPage));
        }
    }
}