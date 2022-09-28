using ImageToArray;
using OTOMOTO.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class MyAccountPage : ContentPage
    {
        private MediaFile file;
        public MyAccountPage()
        {
            InitializeComponent();
        }

        private void TapUploadIamge_Tapped(object sender, EventArgs e)
        {
            PickImageFromGallery();
        }
        private async void PickImageFromGallery()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Przepraszam", "Twoje urzadzenie nie obsluguje tej funkcji", "OK");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                AddImageToServer();
                return stream;
            });
        }
        private async void AddImageToServer()
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            file.Dispose();
            var response = await ApiService.EditUserProfile(imageArray);
            if (response) return;
            await DisplayAlert("Cos poszlo nie tak","Prosze dodac zdjecie jeszcze raz","OK");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var profileImage = await ApiService.GetUserProfileImage();
            if(string.IsNullOrEmpty(profileImage.ImageUrl))
            {
                ImgProfile.Source = "userPlaceHolder.png";
            }
            else
            {
                ImgProfile.Source = profileImage.FullImagePath;
            }
        }

        private void TapChangePassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }

        private void TapChangePhone_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhonePage());
        }

        //private void TapLogout_Tapped(object sender, EventArgs e)
       // {
      //      Preferences.Set("accessToken", string.Empty);
      //      Preferences.Set("tokenExpirationTime", 0);
       //     Application.Current.MainPage = new NavigationPage(new SignupPage());
       // }
    }
}