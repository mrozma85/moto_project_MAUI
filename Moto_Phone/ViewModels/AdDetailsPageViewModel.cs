using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using System.Collections.ObjectModel;
using System.Web;
using Image = Moto_Phone.Models.Image;

namespace Moto_Phone.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class AdDetailsPageViewModel : BaseViewModel
    {
        private readonly MotoApiService motoApiService;

        public ObservableCollection<Ad> Ads { get; set; } = new();

        public ObservableCollection<VehicleImages> Images { get; set; } = new();

        public ObservableCollection<VehicleImages> VehicleImages1 { get; set; } = new();

        public AdDetailsPageViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
        }

        [ObservableProperty]
        List<Ad> ad;

        [ObservableProperty]
        List<VehicleImages> vehicleImages;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        }

        public async Task GetAdData()
        {
            Ad = await motoApiService.GetAd(Id);
        }

        public async Task GetAdImages()
        {
            VehicleImages = await motoApiService.GetImagesAdId(Id);
        }
    }
}
