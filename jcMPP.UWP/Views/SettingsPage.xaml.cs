using Windows.UI.Xaml;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class SettingsPage : BasePage<SettingsModel> {

        public SettingsPage() : base(new UWPFileIO()) {
            this.InitializeComponent();
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.ClearFiles();

            ShowDialog(result ? "Cleared all files successfully" : "Failed to clear files");
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            var result = await CheckForUpdatedDefinitions();
        }
    }
}