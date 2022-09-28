﻿using OTOMOTO.Models;
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
    public partial class ItemsListPage : ContentPage
    {
        public ObservableCollection<VehicleByCategory> VehicleItemsCollection;
        public ItemsListPage(int categoryId)
        {
            InitializeComponent();
            VehicleItemsCollection = new ObservableCollection<VehicleByCategory>();
            GetVehicle(categoryId);
        }

        private async void GetVehicle(int categoryId)
        {
            var vehicles = await ApiService.GetVehicleByCategory(categoryId);
            foreach (var vehicle in vehicles)
            {
                VehicleItemsCollection.Add(vehicle);
            }
            LvVehicles.ItemsSource = VehicleItemsCollection;
        }

        private void LvVehicles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as VehicleByCategory;
            Navigation.PushModalAsync(new ItemDetailPage(selectedItem.Id));
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}