using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class ListPageTemp : ContentPage
{
    private readonly ListPageTempViewModel _viewModel;

    public ListPageTemp(ListPageTempViewModel viewModel)
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