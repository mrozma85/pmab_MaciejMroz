using OTOMOTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OTOMOTO.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            var response = await ApiService.ChangePassword(EntOldPassword.Text, EntNewPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Błąd", "Przepraszam coś poszło nie tak", "Ok");
            }
            else
            {
                await DisplayAlert("Hasło zostało zmienione", "Proszę się zalogować ponownie", "Ok");
                Preferences.Set("accessToken", string.Empty);
                Application.Current.MainPage = new NavigationPage(new SignupPage());
            }
        }
    }
}