namespace Moto_Phone.Models
{
    public class LoginModel
    {
        public LoginModel(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
