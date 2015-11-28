using System;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcMPP.PCL.Objects.Ports;
using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP {
    public sealed partial class MainPage : BasePage {
        private MainPageModel viewModel => (MainPageModel)DataContext;

        public MainPage() {
            this.InitializeComponent();

            DataContext = new MainPageModel(new UWPFileIO());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel.LoadData();

            if (result) {
                return;
            }

            var dialogResult = await ShowDialogPrompt("Definitions not found, do you want to download them?");

            if (dialogResult) {
                result = await viewModel.UpdateDefinitionFiles();

                if (result) {
                    await viewModel.LoadData();

                    return;
                }

                ShowDialog("Could not connect, please try again later");

                return;
            }

            ShowDialog("With no definitions found, scanning cannot occur");
        }

        private void lvPorts_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            viewModel.SelectedPorts = new ObservableCollection<PortListingItem>(lvPorts.SelectedItems.Select(a => (PortListingItem)a).ToList());
        }

        private async void BtnStartScan_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.RunScan();
        }

        private void btnShareResults_Click(object sender, RoutedEventArgs e) {
            var dataTransferManager = DataTransferManager.GetForCurrentView();

            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args) {
            args.Request.Data.Properties.Title = $"Port Scan of {viewModel.HostName} at {DateTime.Now}";

            args.Request.Data.SetText(viewModel.ScanResultForShare);
        }

        private void ApbSettings_OnClick(object sender, RoutedEventArgs e) {
            if (pSettings.IsOpen) {
                return;
            }

            pSettings.IsOpen = true;
        }
    }
}