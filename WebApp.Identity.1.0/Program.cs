using Microsoft.AspNetCore.Identity;
using WebApp.Identity._1._0.ConfigIdentity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityCore<MyUser>(options => {});
builder.Services.AddScoped<IUserStore<MyUser>, MyUserStore>();
builder.Services.AddAuthentication("cookies").AddCookie("cookies", options=> options.LoginPath = "/Home/Login");
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
