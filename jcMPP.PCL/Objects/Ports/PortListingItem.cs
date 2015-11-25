using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.Ports {
    [DataContract]
    public class PortListingItem {
        [DataMember]
        public int PortNumber { get; set; }

        [DataMember]
        public string Description { get; set; }

        public  PortListingItem() { }

        public PortListingItem(string description, int portNumber) {
            PortNumber = portNumber;
            Description = description;
        }
    }
}