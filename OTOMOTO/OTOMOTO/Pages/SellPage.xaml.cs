using OTOMOTO.Models;
using OTOMOTO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OTOMOTO.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellPage : ContentPage
    {
        public ObservableCollection<Category> CategoriesCollection;
        private int categoryId;
        private string condition;
        public SellPage()
        {
            InitializeComponent();
            CategoriesCollection = new ObservableCollection<Category>();
            GetVehicleCategory();
        }

        private async void GetVehicleCategory()
        {
            var categories = await ApiService.GetCategories();
            foreach (var category in categories)
            {
                CategoriesCollection.Add(category);
            }
            PickerCategory.ItemsSource = CategoriesCollection;
        }

        private async void BtnSell_Clicked(object sender, EventArgs e)
        {
            var vehicle = new Vehicle();
            vehicle.Title = EntTitle.Text;
            vehicle.Price = Convert.ToInt32(EntPrice.Text);
            vehicle.Engine = EntEngine.Text;
            vehicle.Model = EntModel.Text;
            vehicle.Color = EntColor.Text;
            vehicle.Company = EntCompany.Text;
            vehicle.Location = EntLocation.Text;
            vehicle.Description = EdiDescription.Text;
            vehicle.DatePosted = DateTime.Now;
            vehicle.Userid =  Preferences.Get("userId", 0);
            vehicle.Categoryid = categoryId;
            vehicle.Condition = condition;

            var response = await ApiService.AddVehicle(vehicle);
            if (response == null) return;
            var vehicleId = response.VehicleId;
            await Navigation.PushModalAsync(new AddImagePage(vehicleId));
        }

        private void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var selectedCategory = (Category)picker.SelectedItem;
            categoryId = selectedCategory.Id;
        }

        private void TapUsed_Tapped(object sender, EventArgs e)
        {
            condition = "used";
            FrameUsed.BackgroundColor = Color.FromHex("#303F9F");
            LblUsed.TextColor = Color.White;
            FrameNew.BackgroundColor = Color.White;
            LblNew.TextColor = Color.FromHex("#303F9F");
        }

        private void TapNew_Tapped(object sender, EventArgs e)
        {
            condition = "new";
            FrameNew.BackgroundColor = Color.FromHex("#303F9F");
            LblNew.TextColor = Color.White;
            FrameUsed.BackgroundColor = Color.White;
            LblUsed.TextColor = Color.FromHex("#303F9F");
        }
    }
}