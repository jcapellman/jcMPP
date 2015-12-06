using Windows.UI.Xaml;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class HashCrackerPage {
        private HashCrackerModel viewModel => (HashCrackerModel)DataContext;

        public HashCrackerPage() {
            this.InitializeComponent();
        }

        private async void btnStartHashCrack_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.SubmitHashes();

            if (result.HasError) {
                ShowDialog($"Hash Crack Failed (Exception: {result.Exception}");
                return;
            }

            ShowDialog("Hash Crack successful");
        }
    }
}