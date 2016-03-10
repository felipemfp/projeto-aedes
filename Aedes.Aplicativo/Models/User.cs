using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aedes.Aplicativo.Helpers;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; }

        public static async Task<User> Register(User user)
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.PostAsync("users", AedesClient.JsonContent(user));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }

        public static async Task<User> Login(string username, string password)
        {
            using (var client = AedesClient.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"users?username={username}&password={password}");
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}