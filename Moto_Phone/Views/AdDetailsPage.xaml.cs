using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class AdDetailsPage : ContentPage
{
	private readonly AdDetailsPageViewModel adDetailsPageViewModel;

    public AdDetailsPage(AdDetailsPageViewModel adDetailsPageViewModel)
	{
		InitializeComponent();
		BindingContext = adDetailsPageViewModel;
		this.adDetailsPageViewModel = adDetailsPageViewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
        await adDetailsPageViewModel.GetAdData();
        await adDetailsPageViewModel.GetAdImages();
    }
}