using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Aedes.Aplicativo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Helpers.Preferences.CurrentUser == null)
            {
                NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
            }
            base.OnNavigatedTo(e);
        }
    }
}