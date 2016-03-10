using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aedes.Aplicativo.Helpers;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Models
{
    public class Frequency
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Days { get; set; }

        public static async Task<List<Frequency>> ToList()
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"frequencies?key={Preferences.CurrentUser.Key}");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Frequency>>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}