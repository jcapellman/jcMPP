using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.KeepAlive;
using jcMPP.PCL.PlatformAbstractions;

using jcMPP.UWP.Library.PlatformImplementations;

namespace jcMPP.UWP.ViewModels {
    public class KeepAliveListingModel : BaseModel {
        #region MVVM Fields
        private bool _addSite_EnableAdd;

        public bool AddSite_EnableAdd
        {
            get { return _addSite_EnableAdd; }

            set { _addSite_EnableAdd = value; OnPropertyChanged(); }
        }

        private bool _addSite_EnableFailureAlert;

        public bool AddSite_EnableFailureAlert
        {
            get { return _addSite_EnableFailureAlert; }

            set { _addSite_EnableFailureAlert = value; OnPropertyChanged(); }
        }

        private string _addSite_AllowableFailuresBeforeAlert;

        public string AddSite_AllowableFailuresBeforeAlert
        {
            get { return _addSite_AllowableFailuresBeforeAlert; }
            set { _addSite_AllowableFailuresBeforeAlert = value; OnPropertyChanged(); CheckAddSiteForm(); }
        }

        private string _addSite_SiteAddress;

        public string AddSite_SiteAddress
        {
            get { return _addSite_SiteAddress; }
            set { _addSite_SiteAddress = value; OnPropertyChanged(); CheckAddSiteForm(); }
        }

        private string _addSite_Interval;

        public string AddSite_Interval
        {
            get { return _addSite_Interval; }
            set { _addSite_Interval = value; OnPropertyChanged(); CheckAddSiteForm(); }
        }

        private bool _addSite_Enable;

        public bool AddSite_Enable
        {
            get { return _addSite_Enable; }
            set { _addSite_Enable = value; OnPropertyChanged(); }
        }
        #endregion

        private void CheckAddSiteForm() {
            AddSite_EnableAdd = !string.IsNullOrEmpty(AddSite_SiteAddress) && !string.IsNullOrEmpty(AddSite_Interval);
        }

        public void ClearAddSiteForm() {
            AddSite_SiteAddress = string.Empty;
            AddSite_Interval = "60";
            AddSite_Enable = true;
            AddSite_EnableFailureAlert = true;
            AddSite_AllowableFailuresBeforeAlert = "2";
        }

        public async Task<bool> AddSiteFormSave() {
            ShowRunning();

            var listingItem = new KeepAliveListingItem {
                Description = AddSite_SiteAddress,
                ID = Guid.NewGuid(),
                IsEnabled = AddSite_Enable,
                LastReport = DateTime.MinValue,
            };

            KeepAliveListing.Add(listingItem);

            var mainItem = new KeepAliveItem(listingItem) {
                AlertOnFailure = AddSite_EnableFailureAlert,
                ConsectutiveFailuresAllowed = AddSite_AllowableFailuresBeforeAlert,
                Interval = AddSite_Interval
            };

            var result = await _baseFileIO.WriteFile(ASSET_TYPES.KEEP_ALIVE_LISTING, KeepAliveListing.ToList());

            await _baseFileIO.WriteFile(ASSET_TYPES.KEEP_ALIVE_ITEM, mainItem, objectGUID: listingItem.ID);

            HideRunning();

            return result.Value;
        }

        public KeepAliveListingModel() : base(new UWPFileIO(App.AppSetting)) { }

        public KeepAliveListingModel(BaseFileIO baseFileIO) : base(baseFileIO) { }

        private ObservableCollection<KeepAliveListingItem> _keepAliveListing;

        public ObservableCollection<KeepAliveListingItem> KeepAliveListing {
            get { return _keepAliveListing; }
            set { _keepAliveListing = value; OnPropertyChanged(); }
        }

        public async Task<CTO<bool>> LoadListing() {
            try {
                ShowRunning();

                var result = await _baseFileIO.GetFile<List<KeepAliveListingItem>>(ASSET_TYPES.KEEP_ALIVE_LISTING);

                KeepAliveListing = result.Value == null
                    ? new ObservableCollection<KeepAliveListingItem>()
                    : new ObservableCollection<KeepAliveListingItem>(result.Value);

                return new CTO<bool>(true);
            } catch (Exception ex) {
                return new CTO<bool>(false, ex);
            } finally {
                HideRunning();
            }
        }
    }
}