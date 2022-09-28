using OTOMOTO.Models;
using OTOMOTO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OTOMOTO.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAdsPage : ContentPage
    {
        public ObservableCollection<MyAd> MyAdsCollection;
        public MyAdsPage()
        {
            InitializeComponent();
            MyAdsCollection = new ObservableCollection<MyAd>();
            GetVehicle();
        }

        private async void GetVehicle()
        {
            var vehicles = await ApiService.GetMyAds();
            foreach (var vehicle in vehicles)
            {
                MyAdsCollection.Add(vehicle);
            }
            LvVehicles.ItemsSource = MyAdsCollection;
        }

        private void LvVehicles_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as MyAd;
            Navigation.PushModalAsync(new ItemDetailPage(selectedItem.Id));
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}