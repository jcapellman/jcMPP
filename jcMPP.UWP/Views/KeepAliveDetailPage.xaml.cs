using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class KeepAliveDetailPage : BasePage {
        public KeepAliveDetailPage() : base(typeof(KeepAliveDetailModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            await viewModel<KeepAliveDetailModel>().LoadData(Guid.Parse(e.Parameter.ToString()));
        }

        private async void btnSaveData_Click(object sender, RoutedEventArgs e) {
            var result = await viewModel<KeepAliveDetailModel>().SaveData();
        }

        private  async void btnDelete_Click(object sender, RoutedEventArgs e) {
            var result = await ShowDialogPrompt("Are you sure you want to delete this?");

            if (!result) {
                return;
            }

            result = await viewModel<KeepAliveDetailModel>().Delete();

            this.Frame.GoBack();
        }
    }
}