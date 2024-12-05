using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
