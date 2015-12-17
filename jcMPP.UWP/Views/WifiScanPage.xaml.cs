using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {

    public sealed partial class WifiScanPage {
        public WifiScanPage() : base(typeof(WifiScanModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel<WifiScanModel>().LoadData();
        }
    }
}