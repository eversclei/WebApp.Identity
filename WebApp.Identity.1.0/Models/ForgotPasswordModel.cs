using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity._1._0.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
