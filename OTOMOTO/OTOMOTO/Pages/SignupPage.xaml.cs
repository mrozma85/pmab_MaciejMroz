using OTOMOTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OTOMOTO.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            var response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            if (response)
            {
                await DisplayAlert("Hi", "Twoje konto zostało utworzone", "OK");
                await Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("NIe dobrze", "Coś poszło nie tak", "Sprobuj ponownie");
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}