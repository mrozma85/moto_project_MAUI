using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class ListPage : ContentPage
{
    private readonly ListPageViewModel _viewModel;

    public ListPage(ListPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetAdsList();
    }
}