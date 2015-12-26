using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class KeepAliveDetailPage : BasePage {
        public KeepAliveDetailPage() : base(typeof(KeepAliveDetailModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            await viewModel<KeepAliveDetailModel>().LoadData();
        }
    }
}