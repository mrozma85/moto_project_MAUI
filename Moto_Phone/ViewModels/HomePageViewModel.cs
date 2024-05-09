using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Moto_Phone.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private readonly MotoApiService _motoApiService;

        public HomePageViewModel(MotoApiService motoApiService)
        {
            _motoApiService = motoApiService;
        }

        [ObservableProperty]
        private string user;

        [ObservableProperty]
        string search;

        [ObservableProperty]
        string homepageSearch;

        public ObservableCollection<Ad> Ads { get; set; } = new();

        [RelayCommand]
        public async Task GetUsername()
        {
            string username1 = await SecureStorage.Default.GetAsync("User");
            if (username1 != null)
            {
                User = username1;
            }
        }

        [RelayCommand]
        public async Task GetAdsList()
        {
            try
            {
                if (Ads.Any()) Ads.Clear();
                var ads = new List<Ad>();
                ads = await _motoApiService.GetAds();
                foreach (var ad in ads) Ads.Add(ad);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd wyświetlenia: {ex.Message}");
                await ShowAlert("Failed to retrive list of cars.");
            }
        }

        [RelayCommand]
        public async Task GetAdsByName()
        {
            var homepageSearch = search;
            await Shell.Current.GoToAsync($"{nameof(ListPageTemp)}?HomepageSearch={homepageSearch}");
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }

        [RelayCommand]
        async Task GetAdDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(AdDetailsPage)}?Id={id}", true);
        }

    }
}
