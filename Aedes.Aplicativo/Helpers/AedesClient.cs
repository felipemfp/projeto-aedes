using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Aedes.Aplicativo.Helpers
{
    public class AedesClient
    {
        private const string baseAddress = "http://localhost:63289/api/";

        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            return client;
        }
    }
}
