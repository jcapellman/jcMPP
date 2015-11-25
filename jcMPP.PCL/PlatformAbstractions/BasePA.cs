using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jcMPP.PCL.PlatformAbstractions {
    public class BasePA {
        protected static byte[] GetBytesFromT<T>(T obj) {
            var jsonStr = GetJSONStringFromT(obj);

            var bytes = new byte[jsonStr.Length * sizeof(char)];

            System.Buffer.BlockCopy(jsonStr.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        protected static T GetObjectFromBytes<T>(byte[] bytes) {
            var chars = new char[bytes.Length / sizeof(char)];

            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);

            return JsonConvert.DeserializeObject<T>(new string(chars));
        }

        public static string GetJSONStringFromT<T>(T obj) { return JsonConvert.SerializeObject(obj); }

        public static T GetObjectFromJSONString<T>(string str) { return JsonConvert.DeserializeObject<T>(str); }
    }
}
