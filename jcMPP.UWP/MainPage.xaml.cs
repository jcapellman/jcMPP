using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP {
    public sealed partial class MainPage : Page {
        private MainPageModel viewModel => (MainPageModel)DataContext;

        public MainPage() {
            this.InitializeComponent();

            DataContext = new MainPageModel();
        }

        private void lvPorts_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            viewModel.SelectedPorts = new ObservableCollection<string>(lvPorts.SelectedItems.Select(a => a.ToString()).ToList());
        }

        private async void BtnStartScan_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.RunScan();
        }
    }
}