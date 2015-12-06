using System;
using Windows.UI.Xaml.Navigation;

namespace jcMPP.UWP.Services {
    public class NavigationEventArgs : EventArgs {
        public NavigationMode NavigationMode { get; set; }

        public string Parameter { get; set; }
    }
}