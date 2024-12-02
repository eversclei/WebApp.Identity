using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity._1._0.Models
{
    public class RegisterModel
    {
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


    }
}
