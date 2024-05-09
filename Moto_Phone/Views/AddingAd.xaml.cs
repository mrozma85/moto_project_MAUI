using Moto_Phone.Models;
using Moto_Phone.ViewModels;
using System.Collections.ObjectModel;

namespace Moto_Phone.Views;

public partial class AddingAd : ContentPage
{
    private readonly AddingAdViewModel _viewModel;
    public ObservableCollection<Category> CategoriesCollection;
    public ObservableCollection<AdType> AdTypesCollection;

    public AddingAd(AddingAdViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        CategoriesCollection = new ObservableCollection<Category>();
        AdTypesCollection = new ObservableCollection<AdType>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetVehicleCategories();
        await _viewModel.GetAdTypes();
        GetCategories();
        GetAdTypes();
    }

    private void GetCategories()
    {
        var categories = _viewModel.Categories;
        foreach (var category in categories)
        {
            CategoriesCollection.Add(category);
        }

        PickerCategory.ItemsSource = CategoriesCollection;
    }

    private void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedCategory = (Category)picker.SelectedItem;
        _viewModel.CategoryId = selectedCategory.Id;
    }

    private void GetAdTypes()
    {
        var adTypes = _viewModel.AdTypes;
        foreach (var types in adTypes)
        {
            AdTypesCollection.Add(types);
        }

        PickerAdTypes.ItemsSource = AdTypesCollection;
    }

    private void PickerAdTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedCategory = (AdType)picker.SelectedItem;
        _viewModel.AdTypeId = selectedCategory.Id;
    }

    //private void GetCompanies()
    //{
    //    var companies = _viewModel.Vehicles;
    //    foreach (var category in companies)
    //    {
    //        CompaniesCollection.Add(category);
    //    }

    //    PickerCompanies.ItemsSource = CompaniesCollection;
    //}

    //private void Pickercompanies_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var picker = sender as Picker;
    //    var selectedCategory = (Category)picker.SelectedItem;
    //    _viewModel.CategoryId = selectedCategory.Id;
    //}
}