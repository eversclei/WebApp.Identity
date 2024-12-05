using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebApp.Identity.ConfigIdentity
{
    public class MyUser : IdentityUser
    {
        public string? NameCompleted { get; set; }
        public string Member { get; set; } = "Member";
        public string OrganizationId{ get; set; }
    }

    public class Organization
    {
        public string?  Id { get; set; }
        public string? Name { get; set; }
    }
}
