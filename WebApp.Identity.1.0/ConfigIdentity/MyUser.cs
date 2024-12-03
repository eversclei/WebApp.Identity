using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebApp.Identity._1._0.ConfigIdentity
{
    public class MyUser : IdentityUser
    {
        public string? NameCompleted { get; set; }
        public string OrganizationId{ get; set; }
    }

    public class Organization
    {
        public string?  Id { get; set; }
        public string? Name { get; set; }
    }
}
