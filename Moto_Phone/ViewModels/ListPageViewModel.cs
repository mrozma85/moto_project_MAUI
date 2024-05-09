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
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.ViewModels
{
    public partial class ListPageViewModel : BaseViewModel
    {

        private readonly MotoApiService motoApiService;

        public ListPageViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
        }

        public ObservableCollection<Ad> Ads { get; set; } = new();

        [ObservableProperty]
        string search;

        [RelayCommand]
        public async Task GetAdsList()
        {
            try
            {
                if (Ads.Any()) Ads.Clear();
                var ads = new List<Ad>();
                ads = await motoApiService.GetAds();
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
            try
            {
                if (search != null)
                {
                    if (Ads.Any()) Ads.Clear();
                    var ads = new List<Ad>();
                    ads = await motoApiService.GetAdByName(search);
                    foreach (var ad in ads) Ads.Add(ad);
                }
                else
                {
                    if (Ads.Any()) Ads.Clear();
                    var ads = new List<Ad>();
                    ads = await motoApiService.GetAds();
                    foreach (var ad in ads) Ads.Add(ad);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd wyświetlenia: {ex.Message}");
                await ShowAlert("Błąd");
            }
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
