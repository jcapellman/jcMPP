using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcMPP.PCL.Enums;
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
                result = await CheckForUpdatedDefinitions();

                if (result) {
                    return;
                }
            }

            ShowDialog("With no definitions found, scanning cannot occur");
        }

        private async Task<bool> CheckForUpdatedDefinitions() {
            var result = await viewModel.UpdateDefinitionFiles();

            if (result == DefinitionResultTypes.UPDATE_SUCCESFULL) {
                await viewModel.LoadData();
            }

            var content = string.Empty;

            switch (result) {
                case DefinitionResultTypes.NO_INTERNET:
                    content = "No Internet Connection Found";
                    break;
                case DefinitionResultTypes.UPDATE_SUCCESFULL:
                    content = "Updated Defintions Succesfully";
                    break;
                case DefinitionResultTypes.CANT_FIND_DEFINITION_SERVER:
                    content = "Cannot connect to defintion server";
                    break;
                case DefinitionResultTypes.NO_UPDATE_NEEDED:
                    content = "No update needed";
                    break;
            }

            ShowDialog(content);

            return false;
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

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e) {
            pSettings.IsOpen = false;
        }

        private async void BtnCheckForUpdates_OnClick(object sender, RoutedEventArgs e) {
            var result = await CheckForUpdatedDefinitions();
        }

        private async void btnClearFiles_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.ClearFiles();
        }
    }
}