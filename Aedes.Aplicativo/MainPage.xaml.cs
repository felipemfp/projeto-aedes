using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Aedes.Aplicativo.Helpers;
using Aedes.Aplicativo.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Aedes.Aplicativo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<UserTask> userTasks;
        private List<Occurrence> occurrences;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ResetItemLists()
        {
            List<UserTask> 
                lstToday = new List<UserTask>(), 
                lstTomorrow = new List<UserTask>(), 
                lstSoon = new List<UserTask>(), 
                lstAlreadyDone = new List<UserTask>();

            userTasks = await UserTask.ToList();
            occurrences = await Occurrence.ToList();

            foreach (var userTask in userTasks)
            {
                List<Occurrence> userTaskOccurrences = occurrences.Where(o => o.UserTaskId == userTask.Id).ToList();
                if (userTaskOccurrences.Count == 0)
                {
                    lstToday.Add(userTask);
                    continue;
                }

                Occurrence lastOccurrence = userTaskOccurrences.Last();
                double diffTotalDays = (DateTime.Now - lastOccurrence.DateOccurrence).TotalDays;
                double diffDays = userTask.Task.Frequency.Days - diffTotalDays;

                if (diffTotalDays < 1)
                {
                    lstAlreadyDone.Add(userTask);
                    continue;
                }
                else
                {
                    if (userTask.Task.Frequency.Days == 1 || diffDays < 0)
                    {
                        lstToday.Add(userTask);
                        continue;
                    }
                    else if (userTask.Task.Frequency.Days == 2 && diffTotalDays > 1)
                    {
                        lstTomorrow.Add(userTask);
                        continue;
                    }
                    else if (diffDays <= 30)
                    {
                        lstSoon.Add(userTask);
                        continue;
                    }
                    else
                    {
                        lstAlreadyDone.Add(userTask);
                        continue;
                    }
                }
            }

            lbxToday.ItemsSource = lstToday;
            lbxTomorrow.ItemsSource = lstTomorrow;
            lbxSoon.ItemsSource = lstSoon;
            lbxAlreadyDone.ItemsSource = lstAlreadyDone;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Preferences.CurrentUser == null)
            {
                NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
            }
            else
            {
                ResetItemLists();
            }

            string reminder = "";
            NavigationContext.QueryString.TryGetValue("reminder", out reminder);
            if (!String.IsNullOrEmpty(reminder))
            {
                Preferences.RemoveReminder(int.Parse(reminder));
            }

            base.OnNavigatedTo(e);
        }

        private void lbx_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UserTask userTask = (sender as ListBox).SelectedItem as UserTask;
            if (userTask != null)
            {
                if (MessageBox.Show($"Deseja marcar \"{userTask.Task.Description}\" como realizada?", "Registrar realização", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Occurrence occurrence = new Occurrence();
                    occurrence.UserTaskId = userTask.Id;
                    occurrence.DateOccurrence = DateTime.Now;
                    Occurrence.Insert(occurrence);
                    ResetItemLists();
                }
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void lbx_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UserTask userTask = (sender as ListBox).SelectedItem as UserTask;
            if (userTask != null)
            {
                if (MessageBox.Show($"Deseja ser lembrado de realizar \"{userTask.Task.Description}\"?", "Adicionar lembrete", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Preferences.SetReminder(userTask, occurrences.Last(o => o.UserTaskId == userTask.Id));
                }
            }
        }
    }
}