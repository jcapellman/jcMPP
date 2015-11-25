using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;

namespace jcMPP.PCL.PlatformAbstractions {
    public abstract class BaseFileIO : BasePA {
        public abstract List<Guid> GetAllClientFiles();

        public abstract Task<CTO<bool>> WriteFile<T>(ASSET_TYPES assetType, T obj, bool encryptFile = true);

        public abstract Task<CTO<T>> GetFile<T>(ASSET_TYPES assetType, bool encrypted = true);
    }
}