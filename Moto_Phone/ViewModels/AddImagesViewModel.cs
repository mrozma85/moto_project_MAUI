using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System.Net.Http.Headers;

namespace Moto_Phone.ViewModels
{
    [QueryProperty(nameof(EntityId), nameof(EntityId))]
    public partial class AddImagesViewModel : BaseViewModel
    {
        private readonly MotoApiService motoApiService;

        public AddImagesViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
        }

        [ObservableProperty]
        int entityId;

        [RelayCommand]
        async Task SaveImage()
        {
            var fileResult = await MediaPicker.PickPhotoAsync();

            using var fileStream = File.OpenRead(fileResult.FullPath);
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
                { fileContent, "fileContent", Path.GetFileName(fileResult.FullPath) }
            };

            var img = new VehicleImages
            {
                Id = 0,
                FileName = fileResult.FileName,
                FileData = bytes,
                AdId = entityId,
            };

            //if (AdId != 0)
            //{
            await motoApiService.AddImages(img);
            //message = motoApiService.StatusMessage;
            //}

            //await ShowAlert(message);
            //await motoApiService.GetAds();
            //await ClearForm();
            await Shell.Current.GoToAsync($"{nameof(HomePage)}");
        }

        [RelayCommand]
        async Task SaveMulti()
        {
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
                    Id = 0,
                    FileName = imgs.FileName,
                    FileData = bytes,
                    AdId = entityId,
                };

                await motoApiService.AddImages(img);
            }
            await Shell.Current.GoToAsync($"{nameof(MyAdsPage)}");
        }
    }
}
