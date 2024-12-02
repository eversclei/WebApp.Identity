namespace WebApp.Identity._1._0.ConfigIdentity
{
    public class MyUser
    {
        public required string Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? PasswordHash { get; set; }
    }
}
