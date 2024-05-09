using Moto_Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.Views
{
    public class LogoutPage : ContentPage
    {
        public LogoutPage(LogoutViewModel logoutViewModel)
        {
            Content = new VerticalStackLayout
            {
                Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Wyloguj"
                }
            }
            };

            BindingContext = logoutViewModel;

        }
    }
}
