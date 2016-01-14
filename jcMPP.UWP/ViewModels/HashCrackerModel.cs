using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

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

        public const int HASHES_FOR_BENCHMARK = 100000;

        public CTO<double> Benchmark() {
            ShowRunning();

            var startTime = DateTime.Now;

            for (var x = 0; x < HASHES_FOR_BENCHMARK; x++) {
                var hasher = HashAlgorithmProvider.OpenAlgorithm("MD5");
                var hashed = hasher.HashData(Encoding.UTF8.GetBytes(x.ToString()).AsBuffer());

                CryptographicBuffer.EncodeToHexString(hashed);
            }

            HideRunning();

            return new CTO<double>(DateTime.Now.Subtract(startTime).TotalSeconds);
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