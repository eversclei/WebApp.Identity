using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Identity._1._0.ConfigIdentity
{
    public static class Store2FA
    {

        public static ClaimsPrincipal ReturnClaims(string userId,string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
