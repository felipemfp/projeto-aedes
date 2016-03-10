using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Helpers
{
    public class AedesClient
    {
        private const string baseAddress = "http://localhost:63289/api/";
        private const string mediaType = "application/json";

        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            return client;
        }

        public static HttpContent JsonContent(object instance)
        {
            string json = JsonConvert.SerializeObject(instance);
            return new StringContent(json, Encoding.UTF8, mediaType);
        }
    }
}
