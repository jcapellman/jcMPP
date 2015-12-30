using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;

namespace jcMPP.PCL.PlatformAbstractions {
    public abstract class BaseFileIO : BasePA {
        public async  Task<List<int>> GetAllClientFiles() {
            var fileList = await GetFile<List<int>>(ASSET_TYPES.FILE_LIST);

            return fileList.HasError ? new List<int>() : fileList.Value;
        }

        public abstract Task<CTO<bool>> WriteFile<T>(ASSET_TYPES assetType, T obj, bool encryptFile = true, Guid? objectGUID = null);

        public abstract Task<CTO<T>> GetFile<T>(ASSET_TYPES assetType, bool encrypted = true, Guid? objectGUID = null);

        public abstract Task<bool> ClearFiles();

        protected string GetFileName(ASSET_TYPES assetType, Guid? objectGUID = null)
        {
            return (objectGUID.HasValue ? $"{assetType}_{objectGUID.Value}" : assetType.ToString());
        }
    }
}