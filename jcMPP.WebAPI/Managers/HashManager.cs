using System;

using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.Hashes;

namespace jcMPP.WebAPI.Managers {
    public class HashManager : BaseManager {
        public CTO<HashCrackResponseItem> CrackHashes(HashCrackRequestItem requestItem) {
            try {
                return new CTO<HashCrackResponseItem>(null);
            } catch (Exception ex) {
                return new CTO<HashCrackResponseItem>(null, ex);
            }
        }
    }
}