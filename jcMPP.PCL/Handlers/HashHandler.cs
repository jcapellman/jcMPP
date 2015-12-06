using System.Threading.Tasks;

using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.Hashes;

namespace jcMPP.PCL.Handlers {
    public class HashHandler : BaseHandler {
        public HashHandler() : base(jcMPP.PCL.Common.Constants.WEBAPI_ADDRESS, "Hash") { }

        public HashHandler(string webAPIURL, string baseArgURL) : base(webAPIURL, baseArgURL) { }

        public async Task<CTO<HashCrackResponseItem>> SubmitHashes(HashCrackRequestItem requestItem) {
            return await POST<HashCrackRequestItem, CTO<HashCrackResponseItem>>(string.Empty, requestItem);
        }
    }
}