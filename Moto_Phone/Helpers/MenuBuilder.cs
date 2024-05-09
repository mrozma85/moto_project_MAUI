using Moto_Phone.Controls;
using Moto_Phone.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.Helpers
{
    public static class MenuBuilder
    {
        public static void BuildMenu()
        {
            Shell.Current.Items.Clear();

            Shell.Current.FlyoutHeader = new FlyOutHeader();

            
            var role = App.UserInfo.Role;

            if (role.Equals("admin"))
            {
                var flyOutItem = new FlyoutItem()
                {
                    Title = "Management",
                    Route = nameof(HomePage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "page.svg",
                            Title = "Strona główna",
                            ContentTemplate = new DataTemplate(typeof(HomePage))
                        },
                        new ShellContent
                        {
                            Icon = "list.svg",
                            Title = "Lista ogłoszeń",
                            ContentTemplate = new DataTemplate(typeof(ListPage))
                        },
                        new ShellContent
                        {
                            Icon = "add.svg",
                            Title = "Dodaj ogłoszenie",
                            ContentTemplate = new DataTemplate(typeof(AddingAd))
                        },
                        new ShellContent
                        {
                            Icon = "userlist.svg",
                            Title = "Moje ogłoszenia",
                            ContentTemplate = new DataTemplate(typeof(MyAdsPage))
                        }
                    }
                };

                if (!Shell.Current.Items.Contains(flyOutItem))
                {
                    Shell.Current.Items.Add(flyOutItem);
                }
            }

            if (role.Equals("customer"))
            {
                var flyOutItem = new FlyoutItem()
                {
                    Title = "Management",
                    Route = nameof(HomePage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Strona główna",
                            ContentTemplate = new DataTemplate(typeof(HomePage))
                        },
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Lista ogłoszeń",
                            ContentTemplate = new DataTemplate(typeof(ListPage))
                        },
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Dodaj ogłoszenie",
                            ContentTemplate = new DataTemplate(typeof(AddingAd))
                        },
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Moje ogłoszenia",
                            ContentTemplate = new DataTemplate(typeof(MyAdsPage))
                        }
                    }
                };

                if (!Shell.Current.Items.Contains(flyOutItem))
                {
                    Shell.Current.Items.Add(flyOutItem);
                }
            }

            var logoutFyloutItem = new FlyoutItem()
            {
                Title = "Wyloguj",
                Route = nameof(LogoutPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Items =
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "WylogujMENUBUILDER",
                        ContentTemplate = new DataTemplate(typeof(LoginPage))
                    }
                }
            };

            if (!Shell.Current.Items.Contains(logoutFyloutItem))
            {
                Shell.Current.Items.Add(logoutFyloutItem);
            }
        }
    }
}
