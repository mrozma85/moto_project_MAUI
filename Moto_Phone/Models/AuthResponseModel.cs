using System.Net;

namespace Moto_Phone.Models
{
    public class AuthResponseModel
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
