using Moto_Phone.ViewModels;
using Moto_Phone.Models;
using System.Collections.ObjectModel;

namespace Moto_Phone.Views;

public partial class AdDetailsChangePage : ContentPage
{
    private readonly AdDetailsChangeViewModel viewModel;
    public ObservableCollection<Category> CategoriesCollection;
    public ObservableCollection<AdType> AdTypesCollection;

    public AdDetailsChangePage(AdDetailsChangeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
        CategoriesCollection = new ObservableCollection<Category>();
        AdTypesCollection = new ObservableCollection<AdType>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.GetAdDetails();
        await viewModel.GetVehicleCategories();
        await viewModel.GetAdTypes();
        GetCategories();
        GetAdTypes();
    }

    private void GetCategories()
    {
        var categories = viewModel.Categories;
        CategoriesCollection.Clear();
        foreach (var category in categories)
        {
            //CategoriesCollection.Clear();
            CategoriesCollection.Add(category);
        }

        PickerCategory.ItemsSource = CategoriesCollection;
    }

    private void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedCategory = (Category)picker.SelectedItem;
        viewModel.CategoryId = selectedCategory.Id;
    }

    private void GetAdTypes()
    {
        var adTypes = viewModel.AdTypes;
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
        viewModel.AdTypeId = selectedCategory.Id;
    }
}