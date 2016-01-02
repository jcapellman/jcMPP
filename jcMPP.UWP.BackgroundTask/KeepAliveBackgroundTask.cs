using System;
using System.Collections.Generic;
using System.Linq;

using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.Web.Http;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects.KeepAlive;
using jcMPP.UWP.Library.PlatformImplementations;

namespace jcMPP.UWP.BackgroundTask {
    public sealed class KeepAliveBackgroundTask : IBackgroundTask {
        private void displayToast(string content) {
            var toastXML = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var toastTextElements = toastXML.GetElementsByTagName("text");
            toastTextElements.First().AppendChild(toastXML.CreateTextNode(content));
            var toastNotification = new ToastNotification(toastXML);
            toastNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        public async void Run(IBackgroundTaskInstance taskInstance) {
            var baseIO = new UWPFileIO(new UWPSetting());

            var keepAliveResult = await baseIO.GetFile<List<KeepAliveListingItem>>(ASSET_TYPES.KEEP_ALIVE_LISTING);

            if (keepAliveResult.HasError || !keepAliveResult.Value.Any(a => a.IsEnabled)) {
                return;
            }

            var httpClient = new HttpClient();
            
            foreach (var item in keepAliveResult.Value.Where(a => a.IsEnabled)) {
                var fullItem = await baseIO.GetFile<KeepAliveItem>(ASSET_TYPES.KEEP_ALIVE_ITEM, objectGUID: item.ID);

                if (fullItem.HasError) {
                    continue;
                }

                var historyItem = new KeepAliveHistoryListingItem {
                    Timestamp = DateTime.Now,
                    Success = true
                };
                
                var httpResponse = await httpClient.GetStringAsync(new Uri(fullItem.Value.SiteAddress));

                if (fullItem.Value.History == null) {
                    fullItem.Value.History = new List<KeepAliveHistoryListingItem>();
                }
                
                if (!string.IsNullOrEmpty(httpResponse)) {
                    continue;
                }

                historyItem.Success = false;

                fullItem.Value.History.Add(historyItem);

                await baseIO.WriteFile(ASSET_TYPES.KEEP_ALIVE_ITEM, fullItem.Value, objectGUID: fullItem.Value.ID);

                item.LastReport = historyItem.Timestamp;

                var index = keepAliveResult.Value.IndexOf(item);

                keepAliveResult.Value[index] = item;

                if (fullItem.Value.AlertOnFailure) {
                    displayToast($"{fullItem.Value.SiteAddress} is down");
                }
            }

            await baseIO.WriteFile(ASSET_TYPES.KEEP_ALIVE_LISTING, keepAliveResult.Value);
        }
    }
}