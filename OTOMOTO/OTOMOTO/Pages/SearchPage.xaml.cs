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
    public partial class SearchPage : ContentPage
    {
        public ObservableCollection<SearchVehicle> SearchVehicleCollection;
        public SearchPage()
        {
            InitializeComponent();
            SearchVehicleCollection = new ObservableCollection<SearchVehicle>();
        }

        private async void SearchBarVehicle_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vehicles = await ApiService.SearchVehicle(e.NewTextValue);
            foreach (var vehicle in vehicles)
            {
                SearchVehicleCollection.Add(vehicle);
            }
            LvSearch.ItemsSource = SearchVehicleCollection;
        }

        private void LvSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedVehicle = e.SelectedItem as SearchVehicle;
            Navigation.PushModalAsync(new ItemDetailPage(selectedVehicle.Id));
        }
    }
}