using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = loadingPageViewModel;
    }
}