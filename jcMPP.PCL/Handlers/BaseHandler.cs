using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using jcMPP.PCL.DataLayer.Models;
using Newtonsoft.Json;

namespace jcMPP.PCL.Handlers {
    public class BaseHandler {
        private string _baseURL { get; set; }

        internal string _baseArgURL { get; set; }

        internal string _token { get; set; }

        public BaseHandler(string webAPIURL, string baseArgURL) {
            _baseURL = webAPIURL;
            _baseArgURL = baseArgURL;
        }

        private HttpClient HC {
            get {
                var handler = new HttpClientHandler();

                var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };

                if (!string.IsNullOrEmpty(_token)) {
                    client.DefaultRequestHeaders.Add("AUTH_TOKEN", _token);
                }

                return client;
            }
        }

        private string BASEURL => _baseURL + _baseArgURL;

        private static HttpContent convertObj<T>(T objValue) {
            return new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
        }

        public async Task<TK> PUT<T, TK>(T obj) where T : BaseModel { return await PUT<T, TK>(_baseArgURL, obj); }

        public async Task<TK> PUT<T, TK>(string urlArgs, T obj) where T : BaseModel {
            var result = await HC.PutAsync(BASEURL, convertObj(obj));
            
            var data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<TK>(data);
        }
        
        public async Task<TK> POST<T, TK>(string urlArgs, T obj) {
            var result = await HC.PostAsync(BASEURL, convertObj(obj));

            var data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<TK>(data);
        }

        public async Task<K> GET<T, K>(string urlArguments, T obj) {
            var str = await HC.GetStringAsync($"{BASEURL}{convertObj(obj)}");

            return JsonConvert.DeserializeObject<K>(str);
        }

        private string parseUrlArguments(string urlArguments) {
            return string.IsNullOrEmpty(urlArguments) ? "" : $"?{urlArguments}";
        }

        public async Task<T> GET<T>(string urlArguments) {
            var str = await HC.GetStringAsync($"{BASEURL}{parseUrlArguments(urlArguments)}");

            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}