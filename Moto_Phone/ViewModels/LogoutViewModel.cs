using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.ViewModels
{
    public partial class LogoutViewModel : BaseViewModel
    {
        public LogoutViewModel()
        {
            Logout();
        }

        [RelayCommand]
        async void Logout()
        {
            SecureStorage.Remove("Token");
            App.UserInfo = null;
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
