using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace jcMPP.UWP.Common {
    public abstract class BootStrapper : Application {
        public event EventHandler<Windows.UI.Core.BackRequestedEventArgs> BackRequested;

        public BootStrapper() {
            this.Resuming += (s, e) => {
                OnResuming(s, e);
            };
            this.Suspending += async (s, e) => {
                var deferral = e.SuspendingOperation.GetDeferral();
                this.NavigationService.Suspending();
                await OnSuspendingAsync(s, e);
                deferral.Complete();
            };
        }

        #region properties

        public Frame RootFrame { get; set; }
        public Services.NavigationService NavigationService { get; private set; }
        protected Func<SplashScreen, Page> SplashFactory { get; set; }

        #endregion

        #region activated

        protected override async void OnActivated(IActivatedEventArgs e) { await InternalActivatedAsync(e); }
        protected override async void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args) { await InternalActivatedAsync(args); }
        protected override async void OnFileActivated(FileActivatedEventArgs args) { await InternalActivatedAsync(args); }
        protected override async void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args) { await InternalActivatedAsync(args); }
        protected override async void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args) { await InternalActivatedAsync(args); }
        protected override async void OnSearchActivated(SearchActivatedEventArgs args) { await InternalActivatedAsync(args); }
        protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args) { await InternalActivatedAsync(args); }

        private async Task InternalActivatedAsync(IActivatedEventArgs e) {
            await this.OnActivatedAsync(e);
            Window.Current.Activate();
        }

        #endregion

        protected override void OnLaunched(LaunchActivatedEventArgs e) { InternalLaunchAsync(e as ILaunchActivatedEventArgs); }

        private async void InternalLaunchAsync(ILaunchActivatedEventArgs e) {
            UIElement splashScreen = default(UIElement);
            if (this.SplashFactory != null) {
                splashScreen = this.SplashFactory(e.SplashScreen);
                Window.Current.Content = splashScreen;
            }

            this.RootFrame = this.RootFrame ?? new Frame();
            this.RootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
            this.NavigationService = new Services.NavigationService.NavigationService(this.RootFrame);

            // the user may override to set custom content
            await OnInitializeAsync();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                try { /* TODO: restore state */ } finally { await this.OnLaunchedAsync(e); }
            } else {
                await this.OnLaunchedAsync(e);
            }

            // if the user didn't already set custom content use rootframe
            if (Window.Current.Content == splashScreen) {
                Window.Current.Content = this.RootFrame;
            }
            if (Window.Current.Content == null) {
                Window.Current.Content = this.RootFrame;
            }
            Window.Current.Activate();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private void OnBackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e) {
            BackRequested?.Invoke(this, e);
            if (!e.Handled) {
                if (this.RootFrame.CanGoBack) {
                    RootFrame.GoBack();
                    e.Handled = true;
                }
            }
        }

        #region overrides

        public virtual Task OnInitializeAsync() { return Task.FromResult<object>(null); }
        public virtual Task OnActivatedAsync(IActivatedEventArgs e) { return Task.FromResult<object>(null); }
        public abstract Task OnLaunchedAsync(ILaunchActivatedEventArgs e);
        protected virtual void OnResuming(object s, object e) { }
        protected virtual Task OnSuspendingAsync(object s, SuspendingEventArgs e) { return Task.FromResult<object>(null); }

        #endregion
    }
}