using OTOMOTO.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OTOMOTO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var accesstoken = Preferences.Get("accesstoken", string.Empty);
            if(string.IsNullOrEmpty(accesstoken))
            {
                MainPage = new NavigationPage(new SignupPage());
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
