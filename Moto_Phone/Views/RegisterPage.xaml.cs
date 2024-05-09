using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        BindingContext = registerViewModel;
    }
}