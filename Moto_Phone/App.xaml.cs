using Moto_Phone.Models;

namespace Moto_Phone
{
    public partial class App : Application
    {
        public static UserInfo UserInfo;
        public static UserDTO User;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
