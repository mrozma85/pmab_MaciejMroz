using OTOMOTO.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = OTOMOTO.Models.Image;

namespace OTOMOTO.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public ObservableCollection<Image> VehicleImage;
        private int TotalImages;
        private string Number;
        private string email;
        public ItemDetailPage(int id)
        {
            InitializeComponent();
            VehicleImage = new ObservableCollection<Image>();
            GetVehicleDetails(id);
        }

        private async void GetVehicleDetails(int id)
        {
            var vehicle = await ApiService.GetVehicleDetail(id);
            LblTitle.Text = vehicle.Title;
            LblLocation.Text = vehicle.Location;
            LblCondition.Text = vehicle.Condition;
            LblPrice.Text =  vehicle.Price.ToString();  
            LblCompany.Text = vehicle.Company;
            LblDescription.Text = vehicle.Description;
            LblColor.Text = vehicle.Color;
            LblModelNo.Text = vehicle.Model;
            LblEngine.Text = vehicle.Engine;
            ImgUser.Source = vehicle.FullImageUrl;
            var images = vehicle.Images;
            TotalImages = vehicle.Images.Count;
            Number = vehicle.Phone;
            email = vehicle.Email;
            foreach (var image in images)
            {
                VehicleImage.Add(image);
            }
            CrvImages.ItemsSource = VehicleImage;
        }

        private void CrvImages_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.FirstVisibleItemIndex <= TotalImages)
            {
                var count = e.FirstVisibleItemIndex +1;
                LblCount.Text = count + " z " + TotalImages;
            }

        }
        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            var emailMessage = new EmailMessage("Pytanie odnośnie pojazdu", "Chciałbym dostać więcej informacji o stanie tego pojazdu", email);
            Email.ComposeAsync(emailMessage);
        }

        public void BtnSms_Clicked(object sender, EventArgs e)
        {
            var smsMessage = new SmsMessage("Dzień dobry, Czy mógłbym dostać więcej informacji o stanie tego pojazdu", Number);
            Sms.ComposeAsync(smsMessage);
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
                PhoneDialer.Open(Number);

        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}