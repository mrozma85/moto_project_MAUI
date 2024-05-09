using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Helpers;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System.IdentityModel.Tokens.Jwt;

namespace Moto_Phone.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private MotoApiService motoApiService;

        public LoginViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayLoginMessage("Wpisz nazwę i hasło");
            }
            else
            {
                // Call API to attempt a login
                var loginModel = new LoginModel(username, password);

                var response = await motoApiService.Login(loginModel);

                // display message
                await DisplayLoginMessage(motoApiService.StatusMessage);

                if (!string.IsNullOrEmpty(response.Token))
                {
                    // Store token in secure storage 
                    await SecureStorage.SetAsync("Token", response.Token);
                    await SecureStorage.SetAsync("UserId", response.User.Id);
                    await SecureStorage.SetAsync("User", response.User.UserName);

                    // build a menu on the fly...based on the user role

                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                    var user = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = user,
                        Role = roleClaim
                    };

                    // navigate to app's main page
                    MenuBuilder.BuildMenu();
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}");
                }
                else
                {
                    await DisplayLoginMessage("Invalid Login Attempt");
                }
            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Komunikat", message, "OK");
            Password = string.Empty;
        }

        [RelayCommand]
        async Task RegisterMove()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");

        }


    }
}
