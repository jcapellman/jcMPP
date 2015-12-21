﻿using Windows.UI.Xaml.Navigation;
using jcMPP.PCL.Enums;
using jcMPP.UWP.PlatformImplementations;
using jcMPP.UWP.ViewModels;

namespace jcMPP.UWP.Views {

    public sealed partial class WifiScanPage {
        public WifiScanPage() : base(typeof(WifiScanModel), new UWPFileIO()) {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel<WifiScanModel>().LoadData();

            switch (result) {
                case WiFiScanResultTypes.NO_ACCESS_TO_WIFI_CARD:
                    ShowDialog("No Access to WiFi Card");
                    break;
                case WiFiScanResultTypes.NO_WIFI_CARD:
                    ShowDialog("No WiFi Card installed");
                    break;
            }
        }
    }
}