using System.Collections.Generic;
using System.Threading.Tasks;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Handlers;
using jcMPP.PCL.Objects;
using jcMPP.PCL.Objects.Hashes;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.ViewModels {
    public class HashCrackerModel : BaseModel {

        private string _hashes;

        public string Hashes {
            get { return _hashes; }
            set { _hashes = value; OnPropertyChanged(); }
        }

        public async Task<CTO<bool>> SubmitHashes() {
            var hashHandler = new HashHandler();

            var requestItem = new HashCrackRequestItem();

            requestItem.HashType = HashTypes.MD5;
            requestItem.MaximumLength = 50;
            requestItem.MinimumLength = 1;
            requestItem.Hashes = new List<string> { Hashes };

            ; var result = await hashHandler.SubmitHashes(requestItem);

            if (result.HasError) {
                return new CTO<bool>(false, result.Exception);
            }

            return new CTO<bool>(true);
        }

        public HashCrackerModel(BaseFileIO baseFileIO) : base(baseFileIO)
        {
        }
    }
}