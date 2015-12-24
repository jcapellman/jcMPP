using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class KeepAliveListingPage : BasePage {
        public KeepAliveListingPage() : base(typeof(KeepAliveListingModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel<KeepAliveListingModel>().LoadListing();
        }
    }
}