using System.Collections.Generic;
using System.Net.Http;
using Aedes.Aplicativo.Helpers;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int FrequencyId { get; set; }
        public virtual Frequency Frequency { get; set; }

        public static async System.Threading.Tasks.Task<List<Task>> ToList()
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"tasks?key={Preferences.CurrentUser.Key}");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Task>>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}