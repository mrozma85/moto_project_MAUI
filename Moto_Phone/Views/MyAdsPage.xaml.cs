using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class MyAdsPage : ContentPage
{
    private readonly MyAdsViewModel _viewModel;

    public MyAdsPage(MyAdsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetMyAdsList();
    }
}