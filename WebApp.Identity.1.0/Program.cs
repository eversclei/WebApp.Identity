using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using WebApp.Identity._1._0.ConfigIdentity;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = @"Server=LAPTOP-QI62QRQ6;Database=IdentityCurso;Integrated Security=False;User ID=clei;Password=ceof1401;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
var migrationAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddDbContext<MyUserDbContext>(
    opt => opt.UseSqlServer(connectionString, sql=> sql.MigrationsAssembly(migrationAssembly))
    );
builder.Services.AddIdentityCore<MyUser>(options => {});
builder.Services.AddScoped<IUserStore<MyUser>, UserOnlyStore<MyUser, MyUserDbContext>>();
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
