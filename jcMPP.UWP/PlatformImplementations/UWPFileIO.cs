﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.PCL.PlatformAbstractions;

namespace jcMPP.UWP.PlatformImplementations {
    public class UWPFileIO : BaseFileIO {
        private async Task<string> GetLocalFile(string path) {
            var folder = await StorageFolder.GetFolderFromPathAsync(path);

            var file = await folder.GetFileAsync(path);

            return await FileIO.ReadTextAsync(file);
        }

        public override async Task<CTO<bool>> WriteFile<T>(ASSET_TYPES assetType, T obj, bool encryptFile = true) {
            var storageFolder = ApplicationData.Current.LocalFolder;

            var str = GetJSONStringFromT(obj);

            byte[] data;

            if (encryptFile) {
                data = await encryptData(str);
            } else {
                data = GetBytesFromT(obj);
            }

            using (var stream = await storageFolder.OpenStreamForWriteAsync(assetType.ToString(), CreationCollisionOption.ReplaceExisting)) {
                stream.Write(data, 0, data.Length);
            }

            return new CTO<bool>(true);
        }

        public override async Task<CTO<T>> GetFile<T>(ASSET_TYPES assetType, bool encrypted = true) {
            var appFolder = ApplicationData.Current.LocalFolder;

            var filesinFolder = await appFolder.GetFilesAsync();

            var file = filesinFolder.FirstOrDefault(a => a.Name == assetType.ToString());

            if (file == null) {
                return new CTO<T>(default(T), $"{assetType} was not found");
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            if (encrypted) {
                var decrypted = await decryptData(buffer.ToArray());
                return new CTO<T>(GetObjectFromJSONString<T>(decrypted));
            }

            return new CTO<T>(GetObjectFromBytes<T>(buffer.ToArray()));
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

        public override List<Guid> GetAllClientFiles()
        {
            
        }
    }
}