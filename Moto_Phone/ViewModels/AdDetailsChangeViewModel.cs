using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using MvvmHelpers;

namespace Moto_Phone.ViewModels
{
    [QueryProperty(nameof(AdIdmove), nameof(AdIdmove))]
    public partial class AdDetailsChangeViewModel : BaseViewModel
    {
        private readonly MotoApiService _motoApiService;
        string message = string.Empty;

        public AdDetailsChangeViewModel(MotoApiService motoApiService)
        {
            _motoApiService = motoApiService;
            //IsModelEnabled = false;
            LoadCompanies();
        }

        public ObservableCollection<Ad> Ads { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<AdType> AdTypes { get; set; } = new();
        public ObservableCollection<VehicleImages> VehicleImagess { get; set; } = new();
        public ObservableRangeCollection<Company> Companies { get; set; } = new();
        public ObservableRangeCollection<Model> Models { get; set; } = new();


        [ObservableProperty]
        List<Ad> ad;
        [ObservableProperty]
        VehicleImages vehicleImages;

        [ObservableProperty]
        AdUpdate adUpdate;

        [ObservableProperty]
        int adIdmove;

        [ObservableProperty]
        int adTypeId;
        [ObservableProperty]
        int categoryId;
        [ObservableProperty]
        int vehicleId;
        [ObservableProperty]
        string vehicleTitle;
        [ObservableProperty]
        int vehicleCompanyId;
        [ObservableProperty]
        int vehicleModelId;
        [ObservableProperty]
        string vehicleDescription;
        [ObservableProperty]
        double vehiclePrice;
        [ObservableProperty]
        int vehicleYear;
        [ObservableProperty]
        int vehicleEngine;
        [ObservableProperty]
        string vehicleColor;
        [ObservableProperty]
        string vehicleLocation;

        [ObservableProperty]
        private Model _selectedModel;

        [ObservableProperty]
        bool _isModelEnabled;

        [RelayCommand]
        public async Task GetAdDetails()
        {
            Ad = await _motoApiService.GetAd(AdIdmove);            

            AdTypeId = ad[0].AdTypeId;
            CategoryId = ad[0].CategoryId;
            VehicleId = ad[0].Vehicle.Id;
            Ad[0].VehicleId = ad[0].Vehicle.Id;
            VehicleTitle = ad[0].Vehicle.Title;
            VehicleCompanyId = ad[0].Vehicle.CompanyId;
            VehicleModelId = ad[0].Vehicle.ModelId;
            VehicleDescription = ad[0].Vehicle.Description;
            VehiclePrice = ad[0].Vehicle.Price;
            VehicleYear = ad[0].Vehicle.Year;
            VehicleEngine = ad[0].Vehicle.Engine;
            VehicleColor = ad[0].Vehicle.Color;
            VehicleLocation = ad[0].Vehicle.Location;
        }

        [RelayCommand]
        public async Task UpdateAdDetails()
        {
            AdUpdate adUpdate = new AdUpdate
            {
                Id = AdIdmove,
                AdTypeId = AdTypeId,
                CategoryId = CategoryId,
                VehicleId = VehicleId,
                Vehicle = new()
                {
                    Id = ad[0].VehicleId,
                    Title = VehicleTitle,
                    CompanyId = VehicleCompanyId,
                    ModelId = VehicleModelId,
                    Description = VehicleDescription,
                    Price = VehiclePrice,
                    Year = VehicleYear,
                    Engine = VehicleEngine,
                    Color = VehicleColor,
                    Location = VehicleLocation,
               },
                ApplicationUserId = Ad[0].ApplicationUserId,
            };

            await _motoApiService.UpdateAd(AdIdmove, adUpdate);
            await GoToMyAdsPage();
            await ShowAlertOk(message);
        }

        [RelayCommand]
        public async Task UpdateAdDetailsImages()
        {
            var images = new List<VehicleImages>();
            images = await _motoApiService.GetImagesAdId(AdIdmove);
            foreach (var vehicleImages in images)
            {
                VehicleImagess.Add(vehicleImages);
                await _motoApiService.DeleteImagesAdId(adIdmove);
            }

            var result = await FilePicker.PickMultipleAsync();

            foreach (var imgs in result)
            {
                using var fileStream = File.OpenRead(imgs.FullPath);
                byte[] bytes;

                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                using var fileContent = new ByteArrayContent(bytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                using var form = new MultipartFormDataContent
                {
                    { fileContent, "fileContent", Path.GetFileName(imgs.FullPath) }
                };

                var img = new VehicleImages
                {
                    Id =0,
                    FileName = imgs.FileName,
                    FileData = bytes,
                    AdId = AdIdmove,
                };
                await _motoApiService.AddImages(img);
            }
            await GoToMyAdsPage();
            await ShowAlertOk(message);
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompany = value;
                    IsModelEnabled = true;
                    LoadModels();
                }
            }
        }

        private async void LoadCompanies()
        {
            var company = await _motoApiService.GetCompanies();

            if (company is null)
                return;

            if (Companies.Count > 0)
                Companies.Clear();
            Companies.AddRange(company);
        }
        public async void LoadModels()
        {
            var model = await _motoApiService.GetModels();

            if (model is null)
                return;

            if (Models.Count > 0)
                Models.Clear();

            var list = model.Where(i => i.CompanyId == SelectedCompany.Id).ToList();
            Models.AddRange(list);
        }

        private async Task GoToMyAdsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MyAdsPage)}");
        }

        private async Task ShowAlertOk(string message)
        {
            await Shell.Current.DisplayAlert("Zmiany wprowadzono", message, "Ok");
        }

        [RelayCommand]
        public async Task GetVehicleCategories()
        {
            var cat = await _motoApiService.GetCategory();
            foreach (var category in cat)
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        public async Task GetAdTypes()
        {
            var results = await _motoApiService.GetAdTypes();
            foreach (var adTypes in results)
            {
                AdTypes.Add(adTypes);
            }
        }
    }
}
