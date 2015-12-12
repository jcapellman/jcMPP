using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

using jcMPP.PCL.Enums;
using jcMPP.UWP.PlatformImplementations;

namespace jcMPP.UWP.Views {
    public abstract class BasePage<T> : Page where T : ViewModels.BaseModel {
        public T viewModel;

        protected BasePage(UWPFileIO uwpFileIo) {
            viewModel = (T)Activator.CreateInstance(viewModel.GetType(), uwpFileIo);

            DataContext = viewModel;
        }

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

        public async Task<bool> CheckForUpdatedDefinitions() {
            var result = await viewModel.UpdateDefinitionFiles();

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
    }
}