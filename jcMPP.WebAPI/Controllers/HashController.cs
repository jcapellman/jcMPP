using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.Hashes;
using jcMPP.WebAPI.Managers;

namespace jcMPP.WebAPI.Controllers {
    public class HashController : BaseController {
        public CTO<HashCrackResponseItem> POST(HashCrackRequestItem requestItem) {
            return new HashManager().CrackHashes(requestItem);
        }
    }
}