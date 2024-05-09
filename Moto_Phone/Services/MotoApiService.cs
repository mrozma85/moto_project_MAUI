using Moto_Phone.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Image = Moto_Phone.Models.Image;

namespace Moto_Phone.Services
{
    public class MotoApiService
    {
        HttpClient _httpClient;
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8020" : "http://localhost:8020";
        public string StatusMessage;

        public MotoApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress) };
        }

        public async Task Register(RegisterationRequestDTO registerModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseAddress + "/api/UsersAuth/register", registerModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Rejestracja powiodła się";
            }
            catch (Exception ex)
            {
                StatusMessage = "Rejestracja nie powiodła się";
            }
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseAddress + "/api/Auth/login", loginModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Zostałeś zalogowany";

                return JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd logoweania.";
                return new AuthResponseModel();
            }
        }

        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Ad>> GetAds()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMaui");
                return JsonConvert.DeserializeObject<List<Ad>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<Ad>> GetAd(int id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMaui/" + id);
                return JsonConvert.DeserializeObject<List<Ad>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<Ad>> GetAdByName(string name)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMaui?filterNazwa=" + name);
                return JsonConvert.DeserializeObject<List<Ad>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<VehicleImages>> GetImages()
        {
            try 
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMauiImages");
                return JsonConvert.DeserializeObject<List<VehicleImages>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<VehicleImages>> GetImagesAdId(int Id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMauiImages/" + Id);
                return JsonConvert.DeserializeObject<List<VehicleImages>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }
            return null;
        }

        public async Task<List<VehicleImages>> DeleteImagesAdId(int Id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.DeleteAsync(BaseAddress + "/api/AdMauiImages/" + Id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Usunięto";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }
            return null;
        }

        public async Task<AdIdResponse> AddAd(AdAdding ad)
        {
            await SetAuthToken();

            var json = JsonConvert.SerializeObject(ad);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseAddress + "/api/AdMauiPost", content);

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdIdResponse>(jsonResult);
        }

        public async Task AddImages(VehicleImages vehicleImage)
        {
            try
            {
                await SetAuthToken();

                var json = JsonConvert.SerializeObject(vehicleImage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(BaseAddress + "/api/AdMauiUploadFile", content);

                response.EnsureSuccessStatusCode();
                StatusMessage = "Dodano pomyślnie";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd dodania.";
            }
        }

        public async Task UpdateImages(int id, UpdateImg vehicleImage)
        {
            try
            {
                await SetAuthToken();

                var json = JsonConvert.SerializeObject(vehicleImage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(BaseAddress + "/api/AdMauiUploadFile/" + id, content);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Zmieniono pomyślnie";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd dodania.";
            }
        }

        public async Task<List<Category>> GetCategory()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/CategoryMaui");
                return JsonConvert.DeserializeObject<List<Category>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<AdType>> GetAdTypes()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/AdMauiAdTypes");
                return JsonConvert.DeserializeObject<List<AdType>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/CompanyMaui");
                return JsonConvert.DeserializeObject<List<Company>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task<List<Model>> GetModels()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync(BaseAddress + "/api/ModelMaui");
                return JsonConvert.DeserializeObject<List<Model>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd połączenia.";
            }

            return null;
        }

        public async Task UpdateAd(int id, AdUpdate ad)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(BaseAddress + "/api/AdMaui/" + id, ad);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Zaktualizowano pomyślnie";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd, spróbuj ponownie.";
            }
        }

        public async Task DeleteAd(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(BaseAddress + "/api/AdMaui/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Ogłoszenie zostało usunięte";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd, spróbuj ponownie.";
            }
        }
    }
}
