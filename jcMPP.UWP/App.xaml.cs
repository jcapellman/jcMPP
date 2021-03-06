﻿using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

using jcMPP.UWP.Library.PlatformImplementations;

namespace jcMPP.UWP {
    sealed partial class App : Common.BootStrapper {
        public static UWPSetting AppSetting = new UWPSetting();

        public App() {
            this.InitializeComponent();
        }

        public override Task OnInitializeAsync() {
            Window.Current.Content = new Views.Shell(this.RootFrame);
            return base.OnInitializeAsync();
        }

        public override Task OnLaunchedAsync(ILaunchActivatedEventArgs e) {
            this.NavigationService.Navigate(typeof(Views.LandingPage));
            return Task.FromResult<object>(null);
        }
    }
}