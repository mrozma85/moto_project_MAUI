using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.ViewModels
{
    public partial class MyAdsViewModel : BaseViewModel
    {
        private readonly MotoApiService _motoApiService;

        public MyAdsViewModel(MotoApiService motoApiService)
        {
            _motoApiService = motoApiService;
        }

        public ObservableCollection<Ad> Ads { get; set; } = new();

        [ObservableProperty]
        int adIdmove;
        [ObservableProperty]
        string vehicleTitle;
        [ObservableProperty]
        string vehicleModel;

        [RelayCommand]
        public async Task GetMyAdsList()
        {
            try
            {
                string username = await SecureStorage.Default.GetAsync("UserId");

                if (Ads.Any()) Ads.Clear();
                var ads = new List<Ad>();
                ads = await _motoApiService.GetAds();
                var myAds = ads.Where(u => u.ApplicationUserId == username);
                
                foreach (var ad in myAds) Ads.Add(ad);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd wyświetlenia: {ex.Message}");
                await ShowAlert("Błąd wyświetlenia.");
            }
        }

        [RelayCommand]
        public async Task DeleteAd(int id)
        {
            if (id==0)
            {
                await ShowAlert("Spróbuj jeszcze raz");
                return;
            }

            await _motoApiService.DeleteAd(id);

            await GetMyAdsList();
        }

        [RelayCommand]
        public async Task ShowDetails()
        {
            await Shell.Current.GoToAsync($"{nameof(AdDetailsChangePage)}");
            //await Shell.Current.GoToAsync($"{nameof(AdDetailsChangePage)}?VehicleTitle={vehicleTitle}");
        }

        [RelayCommand]
        async Task GetAdDetails(int id)
        {
            AdIdmove = id;
            List<Ad> ad;

            ad = await _motoApiService.GetAd(AdIdmove);

            await Shell.Current.GoToAsync($"{nameof(AdDetailsChangePage)}?AdIdmove={AdIdmove}");
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}
