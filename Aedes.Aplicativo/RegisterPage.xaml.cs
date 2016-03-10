using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Aedes.Aplicativo.Resources;
using Aedes.Aplicativo.Models;

namespace Aedes.Aplicativo
{
    public partial class RegisterPage : PhoneApplicationPage
    {
        // Constructor
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Helpers.Preferences.CurrentUser != null)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            base.OnNavigatedTo(e);
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string password = txtPassword.Password;
            string email = txtEmail.Text;

            if (!Helpers.StringExt.IsNullOrWhiteSpace(name, password, email))
            {
                User user = new User();
                user.Username = email;
                user.Name = name;
                user.Password = password;
                user.Email = email;
                user.DateRegister = DateTime.Now;

                User registeredUser = await User.Register(user);
                if (registeredUser != null)
                {
                    Helpers.Preferences.SetCurrentUser(registeredUser);
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Algo deu errado! Tente novamente mais tarde.");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos antes de continuar!");
            }
        }
    }
}