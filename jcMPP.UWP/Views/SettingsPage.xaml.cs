using Windows.UI.Xaml;

using jcMPP.UWP.Library.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class SettingsPage {

        public SettingsPage() : base(typeof(SettingsModel), new UWPFileIO(App.AppSetting)) {
            InitializeComponent();
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<SettingsModel>().ClearFiles();

            ShowDialog(!result.HasError ? "Cleared all files successfully" : "Failed to clear files");
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            await CheckForUpdatedDefinitions();
        }
    }
}