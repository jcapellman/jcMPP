using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace jcMPP.UWP.Views {
    public class BasePage : Page {
        public async void ShowDialog(string content) {
            var dialog = new MessageDialog(content);
            await dialog.ShowAsync();
        }

        public async Task<bool> ShowDialogPrompt(string content) {
            var dialog = new  MessageDialog(content);

            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No"));

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            return result.Label == "Yes";
        }
    }
}