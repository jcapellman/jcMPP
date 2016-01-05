using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.Library.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class SettingsPage {

        public SettingsPage() : base(typeof(SettingsModel), new UWPFileIO(App.AppSetting)) {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            viewModel<SettingsModel>().LoadData();
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<SettingsModel>().ClearFiles();

            ShowDialog(!result.HasError ? "Cleared all files successfully" : "Failed to clear files");
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            await CheckForUpdatedDefinitions();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e) {
            var result = viewModel<SettingsModel>().SaveSettings();

            ShowDialog(!result.HasError ? "Settings Saved" : result.Exception);
        }
    }
}