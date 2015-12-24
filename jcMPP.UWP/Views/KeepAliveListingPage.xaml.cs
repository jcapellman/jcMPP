using Windows.UI.Xaml;
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

        private void BtnCancelAdd_OnClick(object sender, RoutedEventArgs e) {
            pNewSite.IsOpen = false;
        }

        private void btnAddSite_Click(object sender, RoutedEventArgs e) {
            if (pNewSite.IsOpen) {
                return;
            }

            pNewSite.IsOpen = true;
        }
    }
}