using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;

using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.Library.PlatformImplementations {
    public class UWPFileIO : BaseFileIO {
        private readonly StorageFolder _currentFolder;

        public UWPFileIO(BaseSetting appSetting) {
            _currentFolder = (appSetting.GetValue<bool>(SettingKeys.ENABLE_ROAMING_SETTINGS)
                ? ApplicationData.Current.RoamingFolder
                : ApplicationData.Current.LocalFolder);
        }

        private async Task<string> GetLocalFile(string path) {
            var folder = await StorageFolder.GetFolderFromPathAsync(path);

            var file = await folder.GetFileAsync(path);

            return await FileIO.ReadTextAsync(file);
        }

        public override async Task<CTO<bool>> DeleteFile<T>(ASSET_TYPES assetType, Guid? objectGUID = null) {
            try {
                var file = await _currentFolder.GetFileAsync(GetFileName(assetType, objectGUID));

                await file.DeleteAsync();

                return new CTO<bool>(true);
            } catch (Exception ex) {
                return new CTO<bool>(false, ex);
            }
        }

        public override async Task<CTO<bool>> WriteFile<T>(ASSET_TYPES assetType, T obj, bool encryptFile = true, Guid? objectGUID = null) {
            try {
                var str = GetJSONStringFromT(obj);

                byte[] data;

                if (encryptFile)
                {
                    data = await encryptData(str);
                }
                else
                {
                    data = GetBytesFromT(obj);
                }

                using (
                    var stream =
                        await
                            _currentFolder.OpenStreamForWriteAsync(GetFileName(assetType, objectGUID),
                                CreationCollisionOption.ReplaceExisting))
                {
                    stream.Write(data, 0, data.Length);
                }

                return new CTO<bool>(true);
            } catch (Exception ex) {
                return new CTO<bool>(false, ex);
            }
        }
        
        public override async Task<CTO<T>> GetFile<T>(ASSET_TYPES assetType, bool encrypted = true, Guid? objectGUID = null) {
            try {
                var filesinFolder = await _currentFolder.GetFilesAsync();

                var fileName = GetFileName(assetType, objectGUID);

                var file = filesinFolder.FirstOrDefault(a => a.Name == fileName);

                if (file == null) {
                    return new CTO<T>(default(T), $"{assetType} was not found");
                }

                var buffer = await FileIO.ReadBufferAsync(file);

                if (encrypted) {
                    var decrypted = await decryptData(buffer.ToArray());
                    return new CTO<T>(GetObjectFromJSONString<T>(decrypted));
                }

                return new CTO<T>(GetObjectFromBytes<T>(buffer.ToArray()));
            } catch (Exception ex) {
                return new CTO<T>(default(T), ex);
            }
        }

        public async override Task<CTO<bool>> ClearFiles() {
            try {
                await _currentFolder.DeleteAsync();

                return new CTO<bool>(true);
            } catch (Exception ex) {
                return new CTO<bool>(false, ex);
            }
        }

        private async Task<byte[]> encryptData(string unencryptedData) {
            var Provider = new DataProtectionProvider("LOCAL=user");

            var buffMsg = CryptographicBuffer.ConvertStringToBinary(unencryptedData, BinaryStringEncoding.Utf8);

            var buffProtected = await Provider.ProtectAsync(buffMsg);

            return buffProtected.ToArray();
        }

        private async Task<string> decryptData(byte[] encryptedData) {
            var Provider = new DataProtectionProvider();

            var buffUnprotected = await Provider.UnprotectAsync(CryptographicBuffer.CreateFromByteArray(encryptedData));

            return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffUnprotected);
        }
    }
}