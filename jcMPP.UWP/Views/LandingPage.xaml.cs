using System;
using System.Linq;

using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using jcMPP.UWP.BackgroundTask;

namespace jcMPP.UWP.Views {

    public sealed partial class LandingPage : Page {
        public LandingPage() {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e) {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

            var keepAliveBGName = "KeepAliveBackgroundTask";

            var taskRegistered = BackgroundTaskRegistration.AllTasks.Any(task => task.Value.Name == keepAliveBGName);

            if (!taskRegistered) {
                var builder = new BackgroundTaskBuilder {
                    Name = keepAliveBGName,
                    IsNetworkRequested = true,
                    TaskEntryPoint = typeof(KeepAliveBackgroundTask).FullName
                };

                var trigger = new TimeTrigger(20, false);

                builder.SetTrigger(trigger);

                var task = builder.Register();
            }
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

        private void BtnWifiScan_OnClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(WifiScanPage));
        }

        private void BtnKeepAlive_OnClick(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(KeepAliveListingPage));
        }
    }
}