using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.Hashes;
using jcMPP.WebAPI.Managers;

namespace jcMPP.WebAPI.Controllers {
    public class HashController : BaseController {
        public CTO<HashCrackResponseItem> POST(HashCrackRequestItem requestItem) {
            return ReturnResponse(new HashManager().CrackHashes(requestItem), WebAPIResponses.HASH_CRACK_RESPONSE);
        }
    }
}