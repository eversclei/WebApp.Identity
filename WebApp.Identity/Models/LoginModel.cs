using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity.Controllers
{
    public class LoginModel
    {
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}