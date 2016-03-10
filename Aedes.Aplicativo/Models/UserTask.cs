using System;
using System.Collections.Generic;
using System.Net.Http;
using Aedes.Aplicativo.Helpers;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReminder { get; set; }
        public TimeSpan TimeOfReminder { get; set; }
        public string Username { get; set; }
        public virtual User User { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public static async System.Threading.Tasks.Task<List<UserTask>> ToList()
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"usertasks?key={Preferences.CurrentUser.Key}");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<UserTask>>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}