using System;
using System.Collections.Generic;
using System.Net.Http;
using Aedes.Aplicativo.Helpers;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Models
{
    public class Occurrence
    {
        public int Id { get; set; }
        public DateTime DateOccurrence { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTask UserTask { get; set; }

        public static async void Insert(Occurrence occurrence)
        {
            using (var client = AedesClient.GetClient())
            {
                await client.PostAsync($"occurrences?key={Preferences.CurrentUser.Key}", AedesClient.JsonContent(occurrence));
            }
        }

        public static async System.Threading.Tasks.Task<List<Occurrence>> ToList()
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"occurrences?key={Preferences.CurrentUser.Key}");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Occurrence>>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}