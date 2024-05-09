using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace Moto_Phone.ViewModels
{
    public partial class AddingAdViewModel : BaseViewModel
    {
        private readonly MotoApiService motoApiService;
        string message = string.Empty;

        public ObservableCollection<Ad> Ads { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<AdType> AdTypes { get; set; } = new();
        public ObservableCollection<Vehicle> Vehicles { get; set; } = new();
        public ObservableRangeCollection<Company> Companies { get; set; } = new();
        public ObservableRangeCollection<Model> Models { get; set; } = new();

        public AddingAdViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
            IsModelEnabled = false;
            LoadCompanies();
        }

        [ObservableProperty]
        int adId;
        [ObservableProperty]
        int adTypeId;
        [ObservableProperty]
        int categoryId;

        [ObservableProperty]
        string vehicleTitle;
        [ObservableProperty]
        string vehicleDescription;
        [ObservableProperty]
        double vehiclePrice;
        [ObservableProperty]
        int vehicleModelId;
        [ObservableProperty]
        int vehicleYear;
        [ObservableProperty]
        int vehicleEngine;
        [ObservableProperty]
        string vehicleColor;
        [ObservableProperty]
        int vehicleCompanyId;
        [ObservableProperty]
        string vehicleLocation;

        [ObservableProperty]
        string applicationUserId;

        [ObservableProperty]
        int entityId;
        [ObservableProperty]
        int vehicleId;

        [ObservableProperty]
        string categoryName;

        //start cascading picker Company car => Model car

        [ObservableProperty]
        private Model _selectedModel;

        [ObservableProperty]
        bool _isModelEnabled;

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompany = value;
                    //DisplayItem();
                    IsModelEnabled = true;
                    LoadModels();
                }
            }
        }

        private async void LoadCompanies()
        {
            var company = await motoApiService.GetCompanies();

            if (company is null)
                return;

            if (Companies.Count > 0)
                Companies.Clear();
            Companies.AddRange(company);
        }

        public async void LoadModels()
        {
            var model = await motoApiService.GetModels();

            if (model is null)
                return;

            if (Models.Count > 0)
                Models.Clear();

            var list = model.Where(i => i.CompanyId == SelectedCompany.Id).ToList();
            Models.AddRange(list);
        }

        public async void DisplayItem()
        {
            await Shell.Current.DisplayAlert("Wybrany producent", SelectedCompany.Name, "Ok");
        }

        [RelayCommand]
        public async Task GetVehicleCategories()
        {
            var cat = await motoApiService.GetCategory();
            foreach (var category in cat)
            {
                Categories.Add(category);
            }            
        }

        [RelayCommand]
        public async Task GetAdTypes()
        {
            var results = await motoApiService.GetAdTypes();
            foreach (var adTypes in results)
            {
                AdTypes.Add(adTypes);
            }
        }

        [RelayCommand]
        async Task SaveAd()
        {
            //if (string.IsNullOrEmpty(vehicleTitle) || string.IsNullOrEmpty(vehicleDescription) || string.IsNullOrEmpty(vehicleModel))
            //{
            //    await ShowAlert("Please insert valid data");
            //    return;
            //}
            var user = await SecureStorage.GetAsync("UserId");

            var ad = new AdAdding
            {
                Id = AdId,
                VehicleId = 0,
                Vehicle = new Vehicle
                {
                    Id = 0,
                    Title = VehicleTitle,
                    Color = VehicleColor,
                    Price = VehiclePrice,
                    ModelId = SelectedModel.Id,
                    Year = VehicleYear,
                    Engine = VehicleEngine,
                    CompanyId = SelectedCompany.Id,
                    Description = VehicleDescription,
                    Location = VehicleLocation,
                },
                AdTypeId = AdTypeId,
                CategoryId = CategoryId,
                ApplicationUserId = user,
            };

            var response = await motoApiService.AddAd(ad);
            message = motoApiService.StatusMessage;
            if (response == null) return;
            var entityId = response.entityId;
            var vehicleId = response.vehicleId;

            if (response.status == true)
            {                 
                await Shell.Current.GoToAsync($"{nameof(AddImagesPage)}?EntityId={entityId}?VehicleId={vehicleId}");
                await ShowAlertOk(message);
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(HomePage)}");
                await ShowAlert(message);
            }            
        }

        private async Task ShowAlertOk(string message)
        {
            await Shell.Current.DisplayAlert("Dodano informacje, proszę dodać zdjęcia", message, "Ok");
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Proszę spróbować pózniej", message, "Ok");
        }
    }
}
