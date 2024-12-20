using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using WebApp.Identity.ConfigIdentity;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = @"Server=LAPTOP-QI62QRQ6;Database=IdentityCurso;Integrated Security=False;User ID=clei;Password=ceof1401;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
var migrationAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddDbContext<MyUserDbContext>(
    opt => opt.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly))
    );
builder.Services.AddIdentity<MyUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<MyUserDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<MyUser>, MyUserClaimsPrincipalFactory>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/Login");

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(25));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseAuthentication();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
