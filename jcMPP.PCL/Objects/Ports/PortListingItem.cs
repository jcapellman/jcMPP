using Newtonsoft.Json;

namespace jcMPP.PCL.Objects.Ports {
    public class PortListingItem {
        [JsonProperty("portnumber")]
        public int PortNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public  PortListingItem() { }

        public PortListingItem(string description, int portNumber) {
            PortNumber = portNumber;
            Description = description;
        }
    }
}