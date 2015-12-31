using System;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcMPP.PCL.Objects.Ports;
using jcMPP.UWP.Library.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {
    public sealed partial class PortScanPage {

        public PortScanPage() : base(typeof(PortScanModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel<PortScanModel>().LoadData();

            if (result) {
                return;
            }

            var dialogResult = await ShowDialogPrompt("Definitions not found, do you want to download them?");

            if (dialogResult) {
                result = await CheckForUpdatedDefinitions();

                if (result) {
                    return;
                }
            }

            ShowDialog("With no definitions found, scanning cannot occur");
        }
        
        private void lvPorts_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            viewModel<PortScanModel>().SelectedPorts = new ObservableCollection<PortListingItem>(lvPorts.SelectedItems.Select(a => (PortListingItem)a).ToList());
        }

        private async void BtnStartScan_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel<PortScanModel>().RunScan();
        }

        private void btnShareResults_Click(object sender, RoutedEventArgs e) {
            var dataTransferManager = DataTransferManager.GetForCurrentView();

            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args) {
            args.Request.Data.Properties.Title = $"Port Scan of {viewModel<PortScanModel>().HostName} at {DateTime.Now}";

            args.Request.Data.SetText(viewModel<PortScanModel>().ScanResultForShare);
        }
    }
}