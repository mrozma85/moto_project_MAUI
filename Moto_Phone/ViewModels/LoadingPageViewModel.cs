using Moto_Phone.Helpers;
using Moto_Phone.Models;
using Moto_Phone.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Moto_Phone.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            //SecureStorage.Remove("Token");
            // Retrieve token from internal storage
            var token = await SecureStorage.GetAsync("Token");
            if (string.IsNullOrEmpty(token))
            {
                await GoToLoginPage();
            }
            else
            {
                var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    SecureStorage.Remove("Token");
                    await GoToLoginPage();
                }
                else
                {
                    var user = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = user,
                        Role = roleClaim
                    };

                    MenuBuilder.BuildMenu();
                    await GoToHomePage();
                }
            }
        }
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }

        private async Task GoToHomePage()
        {
            await Shell.Current.GoToAsync($"{nameof(HomePage)}");
        }
    }
}
