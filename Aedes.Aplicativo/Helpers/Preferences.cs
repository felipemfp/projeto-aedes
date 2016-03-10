using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace Aedes.Aplicativo.Helpers
{
    public class Preferences
    {
        private const string currentUserKey = "currentUser";

        private static ApplicationDataContainer localSettings => ApplicationData.Current.LocalSettings;

        public static Models.User CurrentUser
        {
            get
            {
                string json = Preferences.Get(currentUserKey) as string;
                if (String.IsNullOrEmpty(json)) return null;
                return JsonConvert.DeserializeObject<Models.User>(json);
            }
        }

        public static void SetCurrentUser(Models.User user)
        {
            string json = JsonConvert.SerializeObject(user);
            Add(currentUserKey, json);
        }

        public static void Add(string key, string value)
        {
            localSettings.Values[key] = value;
        }

        public static object Get(string key)
        {
            return localSettings.Values[key];
        }

        public static void Remove(string key)
        {
            localSettings.Values.Remove(key);
        }
    }
}
