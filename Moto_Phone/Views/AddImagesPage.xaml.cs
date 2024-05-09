using CommunityToolkit.Mvvm.ComponentModel;
using Moto_Phone.ViewModels;

namespace Moto_Phone.Views;

public partial class AddImagesPage : ContentPage
{
    private readonly AddImagesViewModel addImagesViewModel;

    public AddImagesPage(AddImagesViewModel addImagesViewModel)
    {
        InitializeComponent();
        BindingContext = addImagesViewModel;
        this.addImagesViewModel = addImagesViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }
}