using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Aedes.Aplicativo.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Aedes.Aplicativo
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
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

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;
            string email = txtEmail.Text;

            if (!Helpers.StringExt.IsNullOrWhiteSpace(password, email))
            {
                User registeredUser = await User.Login(email, password);
                if (registeredUser != null)
                {
                    Helpers.Preferences.SetCurrentUser(registeredUser);
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Algo deu errado! Tente se cadastrar.");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos antes de continuar!");
            }
        }
    }
}