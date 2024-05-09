using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.Models
{
    public class RegisterationRequestDTO
    {
        public RegisterationRequestDTO(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
