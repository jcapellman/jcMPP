using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects {
    [DataContract]
    public class PortScanListingItem : PortListingItem {
        [DataMember]
        public bool IsOpen { get; set; }

        public PortScanListingItem() { }

        public PortScanListingItem(PortListingItem portListing, bool isOpen = true) {
            Description = portListing.Description;
            PortNumber = portListing.PortNumber;
            IsOpen = isOpen;
        }
    }
}