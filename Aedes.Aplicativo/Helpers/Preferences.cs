using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Aedes.Aplicativo.Models;
using Microsoft.Phone.Scheduler;

namespace Aedes.Aplicativo.Helpers
{
    public class Preferences
    {
        private const string frequenciesKey = "frequencies";
        private const string currentUserKey = "currentUser";

        private static ApplicationDataContainer localSettings => ApplicationData.Current.LocalSettings;

        public static User CurrentUser
        {
            get
            {
                string json = Preferences.Get(currentUserKey) as string;
                if (String.IsNullOrEmpty(json)) return null;
                return JsonConvert.DeserializeObject<User>(json);
            }
        }

        public static async void SetCurrentUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            Add(currentUserKey, json);
            json = JsonConvert.SerializeObject(await Frequency.ToList());
            Add(frequenciesKey, json);
        }

        public static List<Frequency> Frequencies
        {
            get
            {
                string json = Get(frequenciesKey) as string;
                if (String.IsNullOrEmpty(json)) return null;
                return JsonConvert.DeserializeObject<List<Frequency>>(json);
            }
        }
        
        public static void SetReminder(UserTask userTask, Occurrence lastOccurrence)
        {
            string reminderKey = $"reminder{userTask.Id}";

            RemoveReminder(userTask.Id);

            Reminder reminder = new Reminder(Guid.NewGuid().ToString());
            reminder.Title = "Lembrete";
            reminder.Content = userTask.Task.Description;
            reminder.BeginTime = lastOccurrence?.DateOccurrence.AddDays(userTask.Task.Frequency.Days) ?? DateTime.Now.AddDays(userTask.Task.Frequency.Days);
            reminder.ExpirationTime = reminder.BeginTime.AddDays(userTask.Task.Frequency.Days);
            reminder.RecurrenceType = RecurrenceInterval.Daily;
            reminder.NavigationUri = new Uri($"/MainPage.xaml?reminder={userTask.Id}", UriKind.Relative);

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);
            Add(reminderKey, reminder.Name);
        }   
        
        public static void RemoveReminder(int id)
        {
            string reminderKey = $"reminder{id}";

            string reminder = Get(reminderKey) as string;

            if (!String.IsNullOrEmpty(reminder))
            {
                ScheduledActionService.Remove(reminder);
            }
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
