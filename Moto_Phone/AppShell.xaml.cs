using Moto_Phone.Views;

namespace Moto_Phone
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(AdDetailsPage), typeof(AdDetailsPage));
            Routing.RegisterRoute(nameof(AddImagesPage), typeof(AddImagesPage));
            Routing.RegisterRoute(nameof(AdDetailsChangePage), typeof(AdDetailsChangePage));
            Routing.RegisterRoute(nameof(MyAdsPage), typeof(MyAdsPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(ListPageTemp), typeof(ListPageTemp));
        }
    }
}
