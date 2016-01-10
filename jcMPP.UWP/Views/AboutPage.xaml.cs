using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.Library.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class AboutPage : BasePage {
        public AboutPage() : base(typeof(AboutModel), new UWPFileIO(App.AppSetting)) {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            viewModel<AboutModel>().LoadData();
        }
    }
}