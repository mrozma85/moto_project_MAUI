using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moto_Phone.Models;
using Moto_Phone.Services;
using Moto_Phone.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private MotoApiService motoApiService;

        public RegisterViewModel(MotoApiService motoApiService)
        {
            this.motoApiService = motoApiService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string password1;

        [RelayCommand]
        async Task Register()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password1))
            {
                await DisplayRegisterMessage("Rejestracja nieudana");
                await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
            }
            if (password != password1)
            {
                await DisplayRegisterMessage("Wpisz poprawnie oba hasła ");
                await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
            }
            else
            {
                var registerModel = new RegisterationRequestDTO(username, password);

                var response = motoApiService.Register(registerModel);

                await DisplayRegisterMessage(motoApiService.StatusMessage);

                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }

            async Task DisplayRegisterMessage(string message)
            {
                await Shell.Current.DisplayAlert("Rejestracja się powiodła", message, "OK");
                Password = string.Empty;
            }
        }

        [RelayCommand]
        async Task LoginMove()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");

        }
    }
}
