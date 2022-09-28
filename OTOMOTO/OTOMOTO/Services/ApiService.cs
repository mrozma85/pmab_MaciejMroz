using Newtonsoft.Json;
using OTOMOTO.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using UnixTimeStamp;

namespace OTOMOTO.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var registerModel = new RegisterModel()
            {
                Name = name,
                Email = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/accounts/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> Login(string email, string password)
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/accounts/login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.Access_token);
            Preferences.Set("userId", result.User_Id);
            Preferences.Set("tokenExpirationTime", result.Expiration_Time);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            return true;
        }
        public static async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var changePasswordModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(changePasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken",String.Empty));
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/accounts/ChangePassword", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> EditPhoneNumber(string phoneNumber)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            var content = new StringContent($"Number={phoneNumber}", Encoding.UTF8, "application/x-www-form-urlencoded");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/accounts/EditPhoneNumber", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> EditUserProfile(byte[] imageArray)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(imageArray);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/accounts/EditUserProfile", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<UserImageProfile> GetUserProfileImage()
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync("https://ccvehicle.azurewebsites.net/api/accounts/UserProfileImage");
            return JsonConvert.DeserializeObject<UserImageProfile>(response);
        }
        public static async Task<List<Category>> GetCategories()
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync("https://ccvehicle.azurewebsites.net/api/Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        public static async Task<bool> AddImage(int vehicleId, byte[] imageArray)
        {
            var vehicleImage = new VehicleImage()
            {
                VehicleId = vehicleId,
                ImageArray = imageArray
            };
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vehicleImage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/Images", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<VehicleDetail> GetVehicleDetail(int id)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync($"https://ccvehicle.azurewebsites.net/api/Vehicles/VehicleDetails?id={id}");
            return JsonConvert.DeserializeObject<VehicleDetail>(response);
        }
        public static async Task<List<VehicleByCategory>> GetVehicleByCategory(int categoryId)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync($"https://ccvehicle.azurewebsites.net/api/Vehicles?categoryId={categoryId}");
            return JsonConvert.DeserializeObject<List<VehicleByCategory>>(response);
        }
        public static async Task<List<SearchVehicle>> SearchVehicle(string search)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync($"https://ccvehicle.azurewebsites.net/api/Vehicles/SearchVehicles?search={search}");
            return JsonConvert.DeserializeObject<List<SearchVehicle>>(response);
        }
        public static async Task<VehicleResponse> AddVehicle(Vehicle vehicle)
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.PostAsync("https://ccvehicle.azurewebsites.net/api/Vehicles", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VehicleResponse>(jsonResult);
        }
        public static async Task<List<HotAndNewAd>> GetHotAndNewAds()
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync("https://ccvehicle.azurewebsites.net/api/Vehicles/HotAndNewAds");
            return JsonConvert.DeserializeObject<List<HotAndNewAd>>(response);
        }
        public static async Task<List<MyAd>> GetMyAds()
        {
            await TokenValidator.CheckTokenValidaty();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", String.Empty));
            var response = await httpClient.GetStringAsync("https://ccvehicle.azurewebsites.net/api/Vehicles/MyAds");
            return JsonConvert.DeserializeObject<List<MyAd>>(response);
        }
    }
    public static class TokenValidator
    {
        public static async Task CheckTokenValidaty()
        {
            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("currentTime", 0);
            if(currentTime > expirationTime)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiService.Login(email, password);
            }
        }
    }
}
