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
    public partial class ChangePhonePage : ContentPage
    {
        public ChangePhonePage()
        {
            InitializeComponent();
        }

        private async void BtnAddPhone_Clicked(object sender, EventArgs e)
        {
            var response = await ApiService.EditPhoneNumber(EntPhone.Text);
            if(!response)
            {
                await DisplayAlert("oops", "cos poszlo nie tak", "ok");
            }
            else
            {
                await DisplayAlert("dziekuje", "numer zostal zmieniony", "ok");
                await Navigation.PopAsync();
            }
        }
    }
}