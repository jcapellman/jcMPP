using Windows.UI.Xaml;

using jcMPP.UWP.Library.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class HashCrackerPage {

        public HashCrackerPage() : base(typeof(HashCrackerModel), new UWPFileIO(App.AppSetting)) {
            this.InitializeComponent();
        }

        private async void btnStartHashCrack_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<HashCrackerModel>().SubmitHashes();

            if (result.HasError) {
                ShowDialog($"Hash Crack Failed (Exception: {result.Exception}");
                return;
            }

            ShowDialog("Hash Crack successful");
        }

        private async void BtnBenchmark_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<HashCrackerModel>().Benchmark();

            if (result.HasError) {
                ShowDialog(result.Exception);
                return;
            }

            ShowDialog($"{result.Value} seconds to hash {HashCrackerModel.HASHES_FOR_BENCHMARK} hashes");
        }
    }
}